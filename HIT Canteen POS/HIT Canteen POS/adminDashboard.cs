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
    public partial class adminDashboard : Form
    {
        public adminDashboard()
        {
            InitializeComponent();
        }

        private void mealBtn_Click(object sender, EventArgs e)
        {
            Meal_Management In = new Meal_Management();
            this.Hide();
            In.ShowDialog();
        }

        private void salesBtn_Click(object sender, EventArgs e)
        {
            payment In = new payment();
            this.Hide();
            In.Show();
        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            Report In = new Report();
            this.Hide();
            In.Show();
        }

        private void inventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory In = new Inventory();
            this.Hide();
            In.Show();
        }

        private void userBtn_Click(object sender, EventArgs e)
        {
            User_Management In = new User_Management();
            this.Hide();
            In.ShowDialog();

        }

        private void pictureBox2_Click(object sender, EventArgs e) //Minimize Icon
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)//Logout Button
        {
            Login In = new Login();
            this.Hide();
            In.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e) //Close Icon
        {
            DialogResult appClose = MessageBox.Show("Confirm Exit", "HIT CANTEEN POS SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }
    }
}
