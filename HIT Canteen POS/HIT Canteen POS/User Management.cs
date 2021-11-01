using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HIT_Canteen_POS
{
    public partial class User_Management : Form
    {
        public User_Management()
        {
            InitializeComponent();
        }
        MySqlConnection connection = new MySqlConnection("server=localhost; database=water; username=root; password=;");

        private void User_Management_Load(object sender, EventArgs e)
        {
            gridfill();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult appClose = MessageBox.Show("Confirm Exit", "HIT CANTEEN POS SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            adminDashboard In = new adminDashboard();
            this.Hide();
            In.Show();
        }


        private void userDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //importing selected records to text boxes
            if (e.RowIndex >= 0)
            {
                //Gets a collection that gets all the rows
                DataGridViewRow row = this.userDGV.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                IDtxt.Text = row.Cells[0].Value.ToString();
                fullnameTxt.Text = row.Cells[1].Value.ToString();
                emailTxt.Text = row.Cells[2].Value.ToString();
                usernameTxt.Text = row.Cells[3].Value.ToString();
                passwordTxt.Text = row.Cells[4].Value.ToString();
            }
        }

        public void gridfill()
        {
            connection.Open();
            //importign database into datagrid view
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM users", connection);
            DataTable dtb1 = new DataTable();
            sqlDa.Fill(dtb1);

            userDGV.DataSource = dtb1;
            connection.Close();
        }
        
        private void button5_Click_1(object sender, EventArgs e)
        {
            
        }

        private void updateBtn_Click_1(object sender, EventArgs e)
        {
            connection.Open();
            string updateQuery = "UPDATE `users` SET `Fullname`='" + fullnameTxt.Text + "',`Email`='" + emailTxt.Text + "',`Username`='" + usernameTxt.Text + "',`Password`='" + passwordTxt.Text + "' WHERE `Employee ID`='" + IDtxt.Text + "' ";

            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Update Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                gridfill();

            }
            else
            {
                  connection.Close();
                MessageBox.Show("Update Not Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)//Delete Button
        {
            DialogResult delete_confirm = MessageBox.Show("Are you sure you want to delete this user", "HIT Canteen POS System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (delete_confirm == DialogResult.Yes)
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                
                cmd.CommandText = "delete from users where Employee ID =' " + IDtxt.Text + "' ";
                cmd.ExecuteNonQuery();
                connection.Close();
                gridfill();

                MessageBox.Show("Deletion Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            gridfill();
        }

        private void button3_Click_1(object sender, EventArgs e) //Clear Button
        {

            IDtxt.Clear();
            fullnameTxt.Clear();
            emailTxt.Clear();
            usernameTxt.Clear();
            passwordTxt.Clear();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)// Minimize icon
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click_1(object sender, EventArgs e) // Exit Icon
        {
            DialogResult appClose = MessageBox.Show("Confirm Exit", "HIT CANTEEN POS SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e) //Back Icon
        {
            adminDashboard In = new adminDashboard();
            this.Hide();
            In.Show();
        }
    }
}
