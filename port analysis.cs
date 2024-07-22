using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace FYP
{
    public partial class Process_checker : Form
    {
        public IPAddress ipaddr;
        public AppDomain dom = AppDomain.CurrentDomain;

        public int CurPort;
        public int[] ClosedPorts;
        public int[] OpenPorts;
        public int cpIndex = 0;
        public int opIndex = 0;
        public bool WantToExit = false;
        public Process_checker()
        {
            InitializeComponent();
        }

        private void Process_checker_Load(object sender, EventArgs e)
        {

        }

        public bool IsValidIP(string addr)
        {
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            Regex check = new Regex(pattern);
            bool valid = false;
            if (addr == "")
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(addr, 0);
            }
            return valid;
        }

        static void expHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            MessageBox.Show(e.Message);
        }

        public void AddLog(string log)
        {
            textBox2.Text += log + Environment.NewLine;
        }

        public void FireScan()
        {
            //int LoopNum = 0;
         
                while (CurPort < portTo.Value)
                {
                    if (WantToExit == true) { break; }
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress remoteIPAddress = IPAddress.Parse(ipaddr.ToString());
                    int alPort = CurPort;
                    CurPort++;
                    toolStripStatusLabel1.Text = "Scanning port " + alPort.ToString();
                    IPEndPoint remoteEndPoint = new IPEndPoint(remoteIPAddress, alPort);
                    try
                    {
                        //AddLog("Trying port " + alPort.ToString() + "...");
                        sock.Connect(remoteEndPoint);
                        //this.OpenPorts[this.opIndex] = alPort;
                        //this.opIndex++;
                        AddLog("Port " + alPort.ToString() + " is OPEN");
                    }
                    catch (SocketException sockerr)
                    {
                        //this.ClosedPorts[this.cpIndex] = alPort;
                        //this.cpIndex++;
                        //AddLog("Port " + alPort.ToString() + " is CLOSED");
                        //return;
                    }
                    sock.Close();
                    toolStripProgressBar1.PerformStep();
                    decimal perc = 0.00M;
                    perc = (Convert.ToDecimal(alPort) / portTo.Value) * 100;
                    perc = Math.Round(perc, 2);
                    percLabel.Text = perc.ToString() + "%";
                    //LoopNum++;
                    Thread.Sleep(1);
                    //AddLog("This is loop #" + LoopNum.ToString());
                }
                AddLog("Thread exiting...");

          
        }


        private void button1_Click(object sender, EventArgs e)
        {
            dom.UnhandledException += new UnhandledExceptionEventHandler(expHandler);
            Thread[] threads;
            threads = new Thread[Convert.ToInt16(threadCount.Value)];
            if (button1.Text == "")
            {
                WantToExit = false;
                if (ipHost.Text == "" )
                {
                    MessageBox.Show("You must input an IP address or hostname to scan .");
                    ipHost.Focus();
                }

                else
                {
                    if(portTo.Value<501  && portTo.Value>1)
                    { 
                    button1.Text = ".";
                    textBox2.Clear();
                    AddLog("Beginning scan...");
                    AddLog("");
                    toolStripProgressBar1.Maximum = Convert.ToInt16(portTo.Value - portFrom.Value);
                    toolStripProgressBar1.Minimum = 0;
                    toolStripProgressBar1.Value = 0;

                    if (IsValidIP(ipHost.Text) == false)
                    {
                        AddLog("Resolving " + ipHost.Text + "...");
                        IPHostEntry ihe = Dns.GetHostByName(ipHost.Text);
                        ipaddr = ihe.AddressList[0];
                        AddLog("Resolved to " + ipaddr.ToString());
                    }
                    else
                    {
                        ipaddr = IPAddress.Parse(ipHost.Text);
                    }
                    AddLog("Scanning " + ipaddr.ToString() + "...");
                    int i = 0;
                    CheckForIllegalCrossThreadCalls = false;

                    CurPort = Convert.ToInt16(portFrom.Value);
                    for (i = 0; i < threadCount.Value; i++)
                    {
                        threads[i] = new Thread(new ThreadStart(FireScan));
                        threads[i].IsBackground = true;
                        threads[i].Start();
                    }
                }
                else
                {
                    MessageBox.Show("MAx range shouid be less then 500");
                }
                }
            }
            else
            {
                WantToExit = true;
                button1.Text = ".";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            this.Refresh();
            List<Port> s = class_SerialPort.GetNetStatPorts();
            foreach (var x in s)
            {

                dataGridView1.Rows.Add(new object[] { x.port_number.ToString(), x.process_name.ToString(), x.protocol.ToString(), x.name.ToString() });
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Options op = new Options();
            op.ShowDialog();
            this.Close();
        }

        private void portTo_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
