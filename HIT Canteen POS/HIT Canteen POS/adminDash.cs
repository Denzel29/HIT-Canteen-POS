using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIT_Canteen_POS
{
    public partial class adminDash : Form
    {
        public adminDash()
        {
            InitializeComponent();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login In = new Login();
            this.Hide();
            In.Show();

        }
    }
}
