using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FYP
{
    public partial class RAM_Analysis : Form

    {
        public int xPos=0;
        const string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        int subkey_count1 = 0;
        int subkey_count2 = 0;
        public RAM_Analysis()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void RAM_Analysis_Load(object sender, EventArgs e)
        {
            using (RegistryKey startupKey = Registry.LocalMachine.OpenSubKey(runKey))
            {
                var valueNames = startupKey.GetValueNames();
                subkey_count1 = startupKey.SubKeyCount;

                // Name => File path
                Dictionary<string, string> appInfos = valueNames
                    .Where(valueName => startupKey.GetValueKind(valueName) == RegistryValueKind.String)
                    .ToDictionary(valueName => valueName, valueName => startupKey.GetValue(valueName).ToString());
                Display_RegisteryItems(appInfos);
            }
            using (RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(runKey))
            {
                var valueNames = startupKey.GetValueNames();

                // Name => File path
                Dictionary<string, string> appInfos = valueNames
                    .Where(valueName => startupKey.GetValueKind(valueName) == RegistryValueKind.String)
                    .ToDictionary(valueName => valueName, valueName => startupKey.GetValue(valueName).ToString());
                subkey_count2 = startupKey.SubKeyCount;
                Display_RegisteryItems(appInfos);
            }

        }

        void Display_RegisteryItems(Dictionary<string, string> arr)
        {
            foreach (var s in arr)
            {

                int x = dataGridView1.Rows.Add();
                dataGridView1.Rows[x].Cells[0].Value = x + 1;
                dataGridView1.Rows[x].Cells[1].Value = s.Key;
                dataGridView1.Rows[x].Cells[2].Value = s.Value;

                if (" utorrent skype chrome avast avg norton kaspersky ".Contains(s.Key.ToLower())

                     ||
                (
                   s.Key.ToLower().Contains("utorrent") ||
                   s.Key.ToLower().Contains("skype") ||
                   s.Key.ToLower().Contains("torrent") ||
                   s.Key.ToLower().Contains("chrome") ||
                   s.Key.ToLower().Contains("avast") ||
                   s.Key.ToLower().Contains("avg") ||
                   s.Key.ToLower().Contains("norton") ||
                   s.Key.ToLower().Contains("kaspersky") ||
                   s.Key.ToLower().Contains("adobe") ||
                   s.Key.ToLower().Contains("avira")

                   
                )
                    )
                {

                }
                else
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.PeachPuff;
                }

                dataGridView1.AllowUserToAddRows = false;
            }
        }

             private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Width == xPos)
            {
                //repeat marquee
                this.marquee_registry.Location = new System.Drawing.Point(0, 160);
                xPos = 0;
            }
            else
            {
                this.marquee_registry.Location = new System.Drawing.Point(xPos, 160);
                xPos++;
            }
        }

             private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
             {

             }

             private void button1_Click(object sender, EventArgs e)
             {

                 int x = Convert.ToInt32(textBox1.Text);

                 if (x <= subkey_count1)
                 {
                     using (RegistryKey startupKey = Registry.LocalMachine.OpenSubKey(runKey, true))
                     {
                         startupKey.DeleteValue(dataGridView1.Rows[x - 1].Cells[1].Value.ToString());

                         startupKey.Close();

                     }
                 }
                 else
                 {
                     using (RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(runKey, true))
                     {



                         startupKey.DeleteValue(dataGridView1.Rows[x - 1].Cells[1].Value.ToString());

                         startupKey.Close();

                     }
                 }
                 dataGridView1.Rows.Clear();
                 RAM_Analysis_Load(sender, e);
             }

             private void button6_Click(object sender, EventArgs e)
             {
                 this.Hide();
                 Options op = new Options();
                 op.ShowDialog();
                 this.Close();
             }

             private void marquee_registry_Click(object sender, EventArgs e)
             {

             }

        }
    }

