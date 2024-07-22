using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Speech.Synthesis;
using System.Media;
using System.Threading;
using System.Text;
namespace FYP
{
    public partial class Scan : Form
    {
        SpeechSynthesizer voice = new SpeechSynthesizer();
        public SqlConnection cnn;
        public SqlCommand command;
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
        public List<string> list = new List<string>();
        private string p;
        public Scan()
        {
             InitializeComponent();
             cnn = new SqlConnection("Data Source=KIMI\\SQLEXPRESS;Initial Catalog=VIRUS SIGNATURES;Integrated Security=True");
             try
             {
                 cnn.Open();
                 SqlDataAdapter a = new SqlDataAdapter("SELECT * FROM virus_sigvalue", cnn);
                 DataTable t = new DataTable();
                 a.Fill(t);
                 dataGridView1.DataSource = t; 
             }
             catch
             {
                 MessageBox.Show("Database Connectivity Error ! ");
             }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (list.Count != 0)
            {
                foreach (string s in list)
                {
                    try
                    {

                        File.Delete(s);

                    }
                    catch (IOException)
                    {
                        MessageBox.Show("System File cant be deleted");
                    }

                }
                MessageBox.Show("Deletion Sucessfull");
            }

            else
            {
                MessageBox.Show("There is no Virus in QUEUE");
            }
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                pictureBox3.Visible = true;
                string[] drives = System.Environment.GetLogicalDrives();

                foreach (string dr in drives)
                {
                    System.IO.DriveInfo di = new System.IO.DriveInfo(dr);

                    // Here we skip the drive if it is not ready to be read. This
                    // is not necessarily the appropriate action in all scenarios.
                    if (!di.IsReady)
                    {
                        //  Console.WriteLine("The drive {0} could not be read", di.Name);
                        continue;
                    }

                    System.IO.DirectoryInfo rootDir = di.RootDirectory;

                    if (rootDir.ToString() == "C:\\")
                    {
                        label1.Text = rootDir.ToString();
                        WalkDirectoryTree(rootDir);
                    }
                }
              
            }

            if (radioButton2.Checked == true)
            {
                pictureBox3.Visible = true;
                string[] drives = System.Environment.GetLogicalDrives();

                foreach (string dr in drives)
                {
                    System.IO.DriveInfo di = new System.IO.DriveInfo(dr);

                    // Here we skip the drive if it is not ready to be read. This
                    // is not necessarily the appropriate action in all scenarios.
                    if (!di.IsReady)
                    {
                        //  Console.WriteLine("The drive {0} could not be read", di.Name);
                        continue;
                    }

                    System.IO.DirectoryInfo rootDir = di.RootDirectory;

                     label1.Text = rootDir.ToString();
                     WalkDirectoryTree(rootDir);
                    
                }
               
            }

            if (radioButton3.Checked == true)
            {
                pictureBox3.Visible = true;
               // folderBrowserDialog1.ShowDialog();
               // string filePath = folderBrowserDialog1.SelectedPath;
                string[] drives = System.Environment.GetLogicalDrives();

                foreach (string dr in drives)
                {
                    System.IO.DriveInfo di = new System.IO.DriveInfo(dr);

                    // Here we skip the drive if it is not ready to be read. This
                    // is not necessarily the appropriate action in all scenarios.
                    if (!di.IsReady)
                    {
                        //  Console.WriteLine("The drive {0} could not be read", di.Name);
                        continue;
                    }

                    System.IO.DirectoryInfo rootDir = di.RootDirectory;

                    if (rootDir.ToString() == "I:\\")
                    {
                        label1.Text = rootDir.ToString();
                        WalkDirectoryTree(rootDir);
                        cnn.Close();
                    }
                }
              
            }
        }

            public void WalkDirectoryTree(System.IO.DirectoryInfo root)
            {
                System.IO.FileInfo[] files = null;
                System.IO.DirectoryInfo[] subDirs = null;

                // First, process all the files directly under this folder
                try
                {
                    files = root.GetFiles("*.*");
                }
                // This is thrown if even one of the files requires permissions greater
                // than the application provides.
                catch (UnauthorizedAccessException e)
                {
                    // This code just writes out the message and continues to recurse.
                    // You may decide to do something different here. For example, you
                    // can try to elevate your privileges and access the file again.
                    log.Add(e.Message);
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    // Console.WriteLine(e.Message);

                }

                if (files != null)
                {
                    foreach (System.IO.FileInfo fi in files)
                    {
                        // In this example, we only access the existing FileInfo object. If we
                        // want to open, delete or modify the file, then
                        // a try-catch block is required here to handle the case
                        // where the file has been deleted since the call to TraverseTree().
                        // MessageBox.Show(fi.FullName.ToString());
                      //  listBox1.Items.Add(fi.FullName.ToString());
                        label4.Visible = true;
                        label4.Text = fi.FullName.ToString();
                        this.Refresh();

                        try
                        {
                            int rowCount = dataGridView1.RowCount;
                            int colCount = dataGridView1.ColumnCount;
                            string val1 = Security.DTHasher.GetMD5Hash(fi.FullName).ToLower();
                            for (int k = 0; k < rowCount - 1; k++)
                            {
                                for (int i = 0; i < colCount; i++)
                                {
                                    if (val1 == Convert.ToString(dataGridView1.Rows[k].Cells[i].Value) || val1.ToUpper() == Convert.ToString(dataGridView1.Rows[k].Cells[i].Value))
                                    {
                                        SoundPlayer My_JukeBox = new SoundPlayer(@"C:\Users\Umar\Downloads\virus-siren-583542.wav");
                                        My_JukeBox.Play();
                                        //  Thread.Sleep(1000);
                                        voice.SelectVoiceByHints(VoiceGender.Male);
                                        for (int speak = 0; speak < 2; speak++)
                                        {
                                            voice.SpeakAsync(" Virus has been detected");
                                        }
                                        MessageBox.Show("Virus Found Protect your PC" + fi.FullName, "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        label7.Text = (list.Count + 1).ToString();
                                        list.Add(fi.FullName);
                                        dataGridView2.Rows.Add(new object[] { fi.FullName, "Virus Found : May harm your pc", "High risk" });
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Can not open connection ! ");

                        }

                    }

                    // Now find all the subdirectories under this directory.
                    subDirs = root.GetDirectories();

                    foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                    {
                        // Resursive call for each subdirectory.
                        WalkDirectoryTree(dirInfo);
                    }
                }
            }
        private void label3_Click(object sender, EventArgs e)
        {
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Scan_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Options op = new Options();
            op.ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        }
    }