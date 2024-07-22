using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FYP
{
    public partial class login : Form
    {
         private int xPos=0;
        public login()
        {
            InitializeComponent();
            timer1.Start();
        }

              

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Width == xPos)
            {
                //repeat marquee
                this.marquee.Location = new System.Drawing.Point(0, 120);
                xPos = 0;
            }
            else
            {
                this.marquee.Location = new System.Drawing.Point(xPos, 120);
                xPos++;
            }
        }

       
        private void marquee_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Username.Text =="Umar@CyberSecure" && Password.Text == "CyberSecure@123")
            {
                MessageBox.Show("Sucessful Login ");
                this.Hide();
               Options op = new Options();
               op.ShowDialog();
               this.Close();
               
                
                
                   
            }
            else
            {
                status_check.Text = "Ivalid ID or Password";
                status_check.Visible = true;
              
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
