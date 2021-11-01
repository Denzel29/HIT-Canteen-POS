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
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection("server=localhost; database=canteenPOS; username=root; password=;");
        private void Inventory_Load(object sender, EventArgs e)
        {
            inventory_gridfill();
            dataGridView1.Visible = false;
           

        }

        private void Reset_Btn_Click(object sender, EventArgs e)
        {
            idTxt.Text = "";
            productTxt.Text = "";
            descTxt.Text = "";
            quantityTxt.Text = "";
        }

        private void use_btn_Click(object sender, EventArgs e) //Use Button
        {
            /*
            General Algorithm for Use Button
                1.Search for item id to verify if its in existence
                2.Insert Data into used items temp n final
                3.Update table and truncate items temp   
            */

            DataTable check_id = new DataTable();
            MySqlDataAdapter check_id_adapter = new MySqlDataAdapter();
            MySqlCommand run_check_id = new MySqlCommand("SELECT * FROM `inventory` WHERE `Product ID` = '" + idTxt.Text + "' ", connection);
            check_id_adapter.SelectCommand = run_check_id;
            check_id_adapter.Fill(check_id);

            dataGridView1.DataSource = check_id;

            if (check_id.Rows.Count > 0)
            {
                int available_quantity = 0; // Variable to store available item quantity
                int used_quantity = 0;
                int remaining_quantity = 0;

                used_quantity = int.Parse(quantityTxt.Text.Trim());

                available_quantity = Convert.ToInt32(dataGridView1.Rows[0].Cells[3].Value);

                if(available_quantity < used_quantity)
                {
                    MessageBox.Show("Proposed Quantity Exceeds Available Amount", "HIT Cateen POS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    quantityTxt.Clear();
                }
                else
                {
                    //Insertion into used inventory table
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        MySqlCommand use_item = new MySqlCommand();
                        use_item.Connection = connection;
                        use_item.CommandText = " INSERT INTO `used_inventory` (`id`, `Product Name`, `Description`, `Quantity`, `Date`) VALUES(@id, @name, @desc, @quantity,  @date)";
                        use_item.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value);
                        use_item.Parameters.AddWithValue("@name", dataGridView1.Rows[i].Cells[1].Value);
                        use_item.Parameters.AddWithValue("@desc", dataGridView1.Rows[i].Cells[2].Value);
                        use_item.Parameters.AddWithValue("@quantity", quantityTxt.Text);
                        use_item.Parameters.AddWithValue("@date", dateLabel.Text);

                        connection.Open();
                        use_item.ExecuteNonQuery();
                        connection.Close();
                    }

                    //Updating Table
                    remaining_quantity = available_quantity - used_quantity;
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE `inventory` SET `Quantity`= '" + remaining_quantity+ "'  WHERE `Product ID` = '" + idTxt.Text + "'";
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    inventory_gridfill();
                }

            }
            else
            {
                //Show error and clear id text box
                MessageBox.Show("Please Enter a Valid Meal ID", "HIT Canteen POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                idTxt.Clear();
                idTxt.Focus();
            }

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult appClose = MessageBox.Show("Confirm Exit", "HIT CANTEEN POS SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void button5_Click(object sender, EventArgs e) //Add Button
        {
            idTxt.Clear();
            if (productTxt.Text == "")
            {
                MessageBox.Show("Product Name must be SPECIFIED", "HIT Canteen POS SYstem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                productTxt.Focus();
            }
            else if (descTxt.Text == "")
            {
                MessageBox.Show("Product Description must be SPECIFIED", "HIT Canteen POS SYstem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                descTxt.Focus();
            }
            else if (quantityTxt.Text == "")
            {
                MessageBox.Show("Quantuty must be SPECIFIED", "HIT Canteen POS SYstem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                quantityTxt.Focus();
            }
            else
            {
                string insertQuery = "INSERT INTO `inventory`(`Product ID`, `Product Name`, `Description`, `Quantity`,`Date added`) VALUES('" + idTxt.Text + "', '" + productTxt.Text + "', '" + descTxt.Text + "', '" + quantityTxt.Text + "', '" + dateLabel.Text + "')";

                connection.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Insert Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    inventory_gridfill();
                    idTxt.Clear();
                    productTxt.Clear();
                    descTxt.Clear();
                    quantityTxt.Clear();
                    productTxt.Focus();
                }
                else
                {
                    MessageBox.Show("Insert Not Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                }
            }
        }

        void inventory_gridfill()
        {
            //importign database into Inventory_datagrid view
            connection.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM inventory", connection);
            DataTable dtb1 = new DataTable();
            sqlDa.Fill(dtb1);

            inventoryDVG.DataSource = dtb1;
            connection.Close();

        }
      
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            adminDashboard In = new adminDashboard();
            this.Hide();
            In.Show();
        }

        

        private void records_Btn_Click(object sender, EventArgs e) //INVENTORY USAGE BUTTON
        {
            Inventory_records In = new Inventory_records();
            this.Hide();
            In.Show();
        }

      
      
        private void idTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void idTxt_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Today.ToString("yyyy-MM-dd");
            timer1.Start();
        }

        private void used_items_DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}