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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Mobile mobile = new Mobile();
            mobile.Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Accessories accessories = new Accessories();
            accessories.Show();
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Selling selling = new Selling();
            selling.Show();
            this.Close();
        }
    }
}
