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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }
        MySqlConnection connection = new MySqlConnection("server=localhost; database=canteenPOS; username=root; password=;");

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
            Dashboard In = new Dashboard();
            this.Hide();
            In.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Report_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            label7.Visible = false;

            connection.Open();
            //importign database into datagrid view

            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT * FROM sales_report", connection);
            DataTable dtb1 = new DataTable();
            sqlDa.Fill(dtb1);
            dataGridView1.DataSource = dtb1;
            connection.Close();


            //Filling Combo Box with Meals from Database
            combo_boxFill();
                        

        }
        void combo_boxFill()
        {
            //Linking data items in database and combo box
            comboBox1.Items.Clear();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT Meal FROM meal_management", connection);
            DataTable dtb2 = new DataTable();
            sqlDa.Fill(dtb2);

            foreach (DataRow dr in dtb2.Rows)
            {
                comboBox1.Items.Add(dr["Meal"].ToString());

            }
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            label7.Visible = true;
            var mealComboBox = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);

            MySqlDataAdapter sdf = new MySqlDataAdapter("Select * from 	sales_report where Meal like '" + mealComboBox + "' AND date between'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ", connection);
            DataTable sd = new DataTable();
            sdf.Fill(sd);
            dataGridView1.DataSource = sd;

            label6.Text = "0";
            label7.Text = "0";

            //Calculating total sales for the selected meal within the selected period
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                label6.Text = Convert.ToString(int.Parse(label6.Text) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
            }


            //Calculating total units sold for the selected meal within the selected period
            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                label7.Text = Convert.ToString(int.Parse(label7.Text) + int.Parse(dataGridView1.Rows[j].Cells[2].Value.ToString()));
            }

            if (label6.Text == "0")
            {
                MessageBox.Show("No Results from the Selected Range", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click_1(object sender, EventArgs e) //Minimize Icon
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click_1(object sender, EventArgs e) //Close Icon
        {
            DialogResult appClose = MessageBox.Show("Confirm Exit", "HIT CANTEEN POS SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e) // Back icon
        {
            Dashboard In = new Dashboard();
            this.Hide();
            In.Show();
        }
    }
}

