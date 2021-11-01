using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace HIT_Canteen_POS
{
    public partial class storage : Form
    {
        public storage()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection("server=localhost; database=water; username=root; password=;");
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (t1_volume.Text == "")
            {
                MessageBox.Show("Tank 1 Volume not defined", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                t1_volume.Focus();
            }
            else if (t2_volume.Text == "")
            {
                MessageBox.Show("Tank 2 Volume not defined", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                t2_volume.Focus();
            }
            else if (t3_volume.Text == "")
            {
                MessageBox.Show("Tank 3 Volume not defined", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                t3_volume.Focus();
            }
            else if (t4_volume.Text == "")
            {
                MessageBox.Show("Tank 4 Volume not defined", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                t4_volume.Focus();
            }
            else if (t5_volume.Text == "")
            {
                MessageBox.Show("Tank 5 Volume not defined", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                t5_volume.Focus();
            }
            else if (t6_volume.Text == "")
            {
                MessageBox.Show("Tank 6 Volume not defined", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                t6_volume.Focus();
            }
            else
            {
                string t1vol = t1_volume.Text;
                connection.Open();
                string insertQuery = "INSERT INTO `readings`(`Name`, `Volume`, `Date`) VALUES ('Tank 1','" + t1_volume.Text + "','" + dateLabel.Text + "'),('Tank 2','" + t2_volume.Text + "','" + dateLabel.Text + "'),('Tank 3','" + t3_volume.Text + "','" + dateLabel.Text + "'), ('Tank 4','" + t4_volume.Text + "','" + dateLabel.Text + "'), ('Tank 5','" + t5_volume.Text + "','" + dateLabel.Text + "'), ('Tank 6','" + t6_volume.Text + "','" + dateLabel.Text + "')";
                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                if (command.ExecuteNonQuery() == 6)
                {
                    MessageBox.Show("Water Level Recorded", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    t1_volume.Clear();
                    t2_volume.Clear();
                    t3_volume.Clear();
                    t4_volume.Clear();
                    t5_volume.Clear();
                    t6_volume.Clear();
                }
                else
                {
                    MessageBox.Show("Error Encountered", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                connection.Close();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Today.ToString("yyyy-MM-dd");
            timer1.Start();
        }

        private void storage_Load(object sender, EventArgs e)
        {
            dateLabel.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dash In = new dash();
            this.Hide();
            In.Show();
        }
    }
}
