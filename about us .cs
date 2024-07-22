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
    public partial class Scanner : Form
    {
        public Scanner()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.URL = "C:\\Users\\KAMAL\\Desktop\\face.mp4";
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox1.Text = "In the present world of computer technology so much evolution has come that sometimes it feels like technology is interfering with our daily life routine. With the passage of time it could be seen that different countries could provide harm to each other with the use of information technology and by stealing or accessing the sensitive data of the desired country. So for the advance protection in case of such type of scenarios, we decided to check possible vulnerability or loop holes in computer systems, so by focusing on them we can make our sensitive data more secure and private in future. Our Vulnerability analysis toolkit project will help any organization that perform security related services for computer users by analyzing the possible loop holes and then they can build a strategy to overcome on such system vulnerabilities in order to protect sensitive data. Our vision is to check system vulnerabilities and make them secure. Our project first check vulnerabilities of system then we made antivirus or scanner which secure our system by blocking external malwares.";
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Scanner_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Options op = new Options();
            op.ShowDialog();
            this.Close();
        }
    }
}
