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
    public partial class payment : Form
    {
        public payment()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection("server=localhost; database=canteenPOS; username=root; password=;");
        int totalPrice = 0;

        private void payment_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            total_unitsLbl.Visible = false;
            total_salesLbl.Visible = false;
            idTxt.Focus ();
            gridfill();
            label4.Text = "0";
            receipt.Visible = false;

            //importign database into datagrid view

            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM sales_products", connection);
            DataTable dtb3 = new DataTable();
            sqlDa.Fill(dtb3);
            dataGridView2.DataSource = dtb3;
        }

        void receiptHeader() //Purchase receipt's opening text
        {
            timer1.Start();
            dateLabel.Text = DateTime.Today.ToString("yyyy-MM-dd");
            timeLabel.Text = DateTime.Now.ToString("hh:mm:ss");
            receipt.Items.Add("Harare Institute of Technology");
            receipt.Items.Add("Lawley Ave, Harare");
            receipt.Items.Add("P.O.Box BE 277");
            receipt.Items.Add("Tel: (024) 2741422");
            receipt.Items.Add("     "+dateLabel.Text );
            receipt.Items.Add("     "+timeLabel.Text);
        }

        void gridfill()
        {
            //importign database into datagrid view
            connection.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM meal_management", connection);
            DataTable dtb1 = new DataTable();
            sqlDa.Fill(dtb1);

            price_listDGV.DataSource = dtb1;
            connection.Close();

        }

        private void idTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                qtyTxt.Enabled = true;
                qtyTxt.Focus();
            }
        }

        private void qtyTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    string txt = "select * from meal_management where id = '" + idTxt.Text + "' ";
                    MySqlCommand cmd = new MySqlCommand(txt, connection);
                    connection.Open();
                    MySqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        int price = int.Parse(qtyTxt.Text.Trim()) * int.Parse(r[2].ToString());
                        totalPrice = totalPrice + price;
                        dataGridView1.Rows.Add(r[0], r[1], qtyTxt.Text.Trim(), r[2], price);


                    }
                    label4.Text = " " + totalPrice + " ";
                    
                    connection.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Error From Database");
                }

                idTxt.Focus();
                idTxt.Clear();

                qtyTxt.Enabled = false;
                qtyTxt.Clear();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Today.ToString("yyyy-MM-dd");
            timer1.Start();
            timeLabel.Text = DateTime.Now.ToShortTimeString();
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e) // Purchase Button
        {
            if (label4.Text == "0") //Checking if an item was input
            {
                MessageBox.Show("Please Input Items", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dataGridView1.Visible = false;
                //Adding Items to the Receipt
                receiptHeader();
                receipt.Items.Add("------------------------------------------------");
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    receipt.Items.Add("" + dataGridView1.Rows[i].Cells[1].Value + " x " + dataGridView1.Rows[i].Cells[2].Value + "       " + dataGridView1.Rows[i].Cells[4].Value + "");

                }
                receipt.Items.Add("Total Cost: " + label4.Text + " ");
                receipt.Items.Add("=====   End  Of Receipt   ======");
                receipt.Visible = true;

                try
                {
                    //Adding Items purchased to the sales_products table
                    for (int a = 0; a < dataGridView1.Rows.Count - 1; a++)
                    {
                        MySqlCommand purchase = new MySqlCommand();
                        purchase.Connection = connection;

                        purchase.CommandText = "insert into sales_products (id, Meal, quantity, price, total, date)VALUES (@id, @Meal, @quantity, @price, @total, @date) ";
                        purchase.Parameters.AddWithValue("@id", dataGridView1.Rows[a].Cells[0].Value);
                        purchase.Parameters.AddWithValue("@Meal", dataGridView1.Rows[a].Cells[1].Value);
                        purchase.Parameters.AddWithValue("@quantity", dataGridView1.Rows[a].Cells[2].Value);
                        purchase.Parameters.AddWithValue("@price", dataGridView1.Rows[a].Cells[3].Value);
                        purchase.Parameters.AddWithValue("@total", dataGridView1.Rows[a].Cells[4].Value);
                        purchase.Parameters.AddWithValue("@date", dateLabel.Text);

                        connection.Open();
                        purchase.ExecuteNonQuery();

                        connection.Close();


                    }
                    MessageBox.Show("Purchase Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Visible = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                    receipt.Visible = false;
                }

                //Clearing List Box and datagridview and textboxes after a successful purchase
                dataGridView1.Rows.Clear();
                idTxt.Clear();
                qtyTxt.Clear();
                label4.Text = "0";
                totalPrice = 0;
                receipt.Items.Clear();
                idTxt.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e) // End of day Event
        {


            /*
                GENERAL ALGORYRHM FOR END OF DAY EVENT

             * Take meal id from the pricelist datagridview
             * Create a loop that loops through all records in the meal manegement table
             *      select current meal id using above loop and filter the sales_products table
             *      Create loop for adding total sales.
             *      create loop for adding total quantiity sold
             *  Insert the Corresponding total quantity sold and sales into the sales report table
             *  truncate sales products table
             
             */


            try
            {
                for (int i = 0; i < price_listDGV.Rows.Count - 1; i++)
                {
                    //Filtering selected meal in the above look in dataGridview2
                    var search_meal_id = this.price_listDGV.Rows[i].Cells[0].Value;

                    connection.Open();
                    MySqlDataAdapter dataSearch = new MySqlDataAdapter("Select * from sales_products where id = '" + search_meal_id + "' ", connection);
                    DataTable sd = new DataTable();
                    dataSearch.Fill(sd);
                    dataGridView2.DataSource = sd;
                    connection.Close();
                    total_salesLbl.Text = "0"; //storing total sales
                    total_unitsLbl.Text = "0"; //storing total quantity sold

                    //Calculating  total quantity
                    for (int x = 0; x < dataGridView2.Rows.Count - 1; x++)
                    {
                        total_unitsLbl.Text = Convert.ToString(int.Parse(total_unitsLbl.Text) + int.Parse(dataGridView2.Rows[x].Cells[2].Value.ToString()));
                    }


                    //Calculating total sales 
                    for (int y = 0; y < dataGridView2.Rows.Count - 1; y++)
                    {
                        total_salesLbl.Text = Convert.ToString(int.Parse(total_salesLbl.Text) + int.Parse(dataGridView2.Rows[y].Cells[4].Value.ToString()));
                    }


                    //Query for inserting meal date and total items and total sales 
                    MySqlCommand day_end = new MySqlCommand();
                    day_end.Connection = connection;

                    day_end.CommandText = "INSERT INTO `sales_report`(id, Meal, quantity_sold, total_sales, date) Values (@id, @Meal, @quantity_sold, @total_sales, @date)";
                    day_end.Parameters.AddWithValue("@id", price_listDGV.Rows[i].Cells[0].Value);
                    day_end.Parameters.AddWithValue("@Meal", price_listDGV.Rows[i].Cells[1].Value);
                    day_end.Parameters.AddWithValue("@quantity_sold", total_unitsLbl.Text);
                    day_end.Parameters.AddWithValue("@total_sales", total_salesLbl.Text);
                    day_end.Parameters.AddWithValue("@date", dateLabel.Text);

                    connection.Open();
                    day_end.ExecuteNonQuery();
                    connection.Close();

                }

                //Truncating the sales_product table
                MySqlCommand trun = new MySqlCommand();
                trun.Connection = connection;

                trun.CommandText = "TRUNCATE TABLE sales_products";
                connection.Open();
                trun.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("End Of Day Event Successful", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e) //Clear BUTTON
        {
            dataGridView1.Rows.Clear();
            idTxt.Clear();
            qtyTxt.Clear();
            label4.Text = "0";
            totalPrice = 0;
            receipt.Items.Clear();
            
        }

        private void total_unitsLbl_Click(object sender, EventArgs e)
        {

        }

        private void qtyTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e) //Back Icon
        {
            Dashboard In = new Dashboard();
            this.Hide();
            In.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e) //Minimise Icon
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e) // Close Icon
        {
            DialogResult appClose = MessageBox.Show("Confirm Exit", "HIT CANTEEN POS SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }
    }
}
