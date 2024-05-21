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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        int Startpoint = 15;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Startpoint += 1;
            ProgressBar1.Value = Startpoint;
            ProgressBar2.Value = Startpoint;
            if (ProgressBar1.Value == 100)
            {
                ProgressBar1.Value = 0;
                ProgressBar2.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        
    }
}
