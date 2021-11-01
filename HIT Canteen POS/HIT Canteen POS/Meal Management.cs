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
    public partial class Meal_Management : Form
    {

        MySqlConnection connection = new MySqlConnection("server=localhost; database=canteenPOS; username=root; password=;"); //Connection String
        public Meal_Management()
        {
            InitializeComponent();
        }

        public void gridfill()
        {
            connection.Open();
            //importign database into datagrid view
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM meal_management", connection);
            DataTable dtb1 = new DataTable();
            sqlDa.Fill(dtb1);

            mealDVG.DataSource = dtb1;
            connection.Close();

        }
        private void button3_Click(object sender, EventArgs e) //Reset Clear
        {
            idTxt.Clear();
            mealTxt.Clear();
            priceTxt.Clear();
        }

        private void button5_Click(object sender, EventArgs e) //Insert Button
        {
            connection.Open();
            string insertQuery = "INSERT INTO `meal_management`(`id`, `Meal`, `Price`) VALUES ('" + idTxt.Text + "','" + mealTxt.Text + "','" + priceTxt.Text + "')";

            if (mealTxt.Text == "")
            {
                MessageBox.Show("Meal not defined", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mealTxt.Focus();
            }
            else if (priceTxt.Text == "")
            {
                MessageBox.Show("PRICE not defined", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                priceTxt.Focus();
            }
            else
            {
                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Insert Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    gridfill();
                    idTxt.Clear();
                    mealTxt.Clear();
                    priceTxt.Clear();

                }
                else
                {
                    MessageBox.Show("Insert Not Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   
                }
                connection.Open();
            }
            connection.Close();
        }

        private void Meal_Management_Load(object sender, EventArgs e)
        {
            gridfill();
            
        }

        private void mealDVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //importing selected records to text boxes
            if (e.RowIndex >= 0)
            {
                //Gets a collection that gets all the rows
                DataGridViewRow row = this.mealDVG.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                idTxt.Text = row.Cells[0].Value.ToString();
                mealTxt.Text = row.Cells[1].Value.ToString();
                priceTxt.Text = row.Cells[2].Value.ToString();
            }
        }

        private void updateBtn_Click(object sender, EventArgs e) //Update Button
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update meal_management set meal = '"+mealTxt.Text+"', price ='"+priceTxt.Text+ "' where id = '" + idTxt.Text + "' ";
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Update Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gridfill();
        }

        private void button4_Click(object sender, EventArgs e) //Delete Button
        {
            DialogResult delete_confirm =  MessageBox.Show("Are you sure you want to delete this item", "HIT Canteen POS System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
           
            if (delete_confirm == DialogResult.Yes)
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from meal_management where id =' " + idTxt.Text + "' ";
                cmd.ExecuteNonQuery();
                connection.Close();
                gridfill();

                MessageBox.Show("Deletion Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }

        private void pictureBox2_Click(object sender, EventArgs e) //minimize icon
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard In = new Dashboard();
            this.Hide();
            In.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e) // Exit Button
        {
            DialogResult appClose = MessageBox.Show("Confirm Exit", "HIT CANTEEN POS SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }
    }
}
