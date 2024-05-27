using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobisoftCsharp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            UIDtb.Text = "";
            PwdDtb.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(UIDtb.Text == "" ||  UIDtb.Text == "")
            {
                MessageBox.Show("enter User-Name or Password");
            }else if (UIDtb.Text == "Admin1" && PwdDtb.Text == "1stadmin" || UIDtb.Text == "Admin2" && PwdDtb.Text == "2ndadmin")
            {
                Home home = new Home();
                home.Show();
                this.Close();
            } 
            else
            {
                MessageBox.Show("Wrong User-Name or Password");
            }
        }
    }
}
