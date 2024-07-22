using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FYP
{
    public partial class Network_Analysis : Form
    {
        public Network_Analysis()
        {
            InitializeComponent();
           
        }

        private void btnListAll_Click(object sender, EventArgs e)
        {
            // clear the textbox of any content
            txtAllProcesses.Text = string.Empty;

            // populate it with a list of all running processes
            // with name and process ID shown
            txtAllProcesses.Text = ProcessValidation.ListAllProcesses();
        }

        private void btnListImages_Click(object sender, EventArgs e)
        {
            txtAllProcesses.Text = ProcessValidation.ListAllByImageName();
        }

        private void btnListAllApps_Click(object sender, EventArgs e)
        {
            // clear the textbox of any content
            txtAllProcesses.Text = string.Empty;

            // populate it with a list of all applications along with
            // some information about those application
            txtAllProcesses.Text = ProcessValidation.ListAllApplications();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool bTest = ProcessValidation.CheckForProcessByName(txtSearchProcess.Text.ToString());

            switch (bTest)
            {
                case true:
                    MessageBox.Show(txtSearchProcess.Text + " process name found.");
                    break;
                case false:
                    MessageBox.Show(txtSearchProcess.Text + " process name not found.");
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool bTest = ProcessValidation.CheckForProcessByImageName(txtImageName.Text.ToString());

            switch (bTest)
            {
                case true:
                    MessageBox.Show(txtImageName.Text + " image name found.");
                    break;
                case false:
                    MessageBox.Show(txtImageName.Text + " image name not found.");
                    break;
                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            bool bTest = ProcessValidation.CheckForApplicationByName(txtApplicationName.Text.ToString());

            switch (bTest)
            {
                case true:
                    MessageBox.Show(txtApplicationName.Text + " application name found.");
                    break;
                case false:
                    MessageBox.Show(txtApplicationName.Text + " application name not found.");
                    break;
                default:
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Options OP = new Options();
            OP.ShowDialog();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
