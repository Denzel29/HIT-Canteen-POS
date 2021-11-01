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
    public partial class dash : Form
    {
        public dash()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e) //minimize icon
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e) //Exit Icon
        {
            DialogResult appClose = MessageBox.Show("Are You Sure You Want to Exit", "WATER RATIONING  SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void dash_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            storage In = new storage();
            this.Hide();
            In.Show();
        }
    }
}
