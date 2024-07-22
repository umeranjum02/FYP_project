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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            this.Hide();
            Scan op = new Scan();
            op.ShowDialog();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            this.Hide();
            Network_Analysis op = new Network_Analysis();
            op.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            this.Hide();
            Process_checker op = new Process_checker();
            op.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            RAM_Analysis op = new RAM_Analysis();
            op.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
         
            this.Hide();
            Scanner op = new Scanner();
            op.ShowDialog();
            this.Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            login log = new login();
            log.ShowDialog();
            this.Close();
        }

     
    }
}
