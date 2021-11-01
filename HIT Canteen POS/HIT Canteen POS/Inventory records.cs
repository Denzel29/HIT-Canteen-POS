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
    public partial class Inventory_records : Form
    {
        public Inventory_records()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection("server=localhost; database=canteenPOS; username=root; password=;");

        private void Inventory_records_Load(object sender, EventArgs e)
        {
            used_inventory_gridfill();
            product_list_gridfill();
            combo_boxFill();

            label7.Visible = false;
        }

        void combo_boxFill()
        {
            //Linking data items in database and combo box
            comboBox1.Items.Clear();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT `Product ID` FROM `inventory`", connection);
            DataTable dtb2 = new DataTable();
            sqlDa.Fill(dtb2);

            foreach (DataRow dr in dtb2.Rows)
            {
                comboBox1.Items.Add(dr["Product ID"].ToString());
            }
        }

            void product_list_gridfill()
        {
            //importign database into Inventory_datagrid view
            connection.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT `Product ID`, `Product Name`, `Description` FROM `inventory`", connection);
            DataTable dtb1 = new DataTable();
            sqlDa.Fill(dtb1);

            product_list_DGV.DataSource = dtb1;
            connection.Close();

        }

        void used_inventory_gridfill()
        {
            //importign database into Used_Items_DGV
            connection.Open();
            MySqlDataAdapter sqlDa1 = new MySqlDataAdapter("SELECT * FROM used_inventory", connection);
            DataTable dtb2 = new DataTable();
            sqlDa1.Fill(dtb2);

            used_items_DGV.DataSource = dtb2;
            connection.Close();

        }

        

        private void ID_combo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            label7.Visible = true;

            var mealComboBox = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            connection.Open();
            MySqlDataAdapter sdf = new MySqlDataAdapter("Select * from 	used_inventory where id like '" + mealComboBox + "' AND Date between'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ", connection);
            DataTable sd = new DataTable();
            sdf.Fill(sd);
            used_items_DGV.DataSource = sd;
            connection.Close();

            
            label7.Text = "0";

            
            //Calculating total units sold for the selected meal within the selected period
            for (int j = 0; j < used_items_DGV.Rows.Count ; j++)
            {
                label7.Text = Convert.ToString(int.Parse(label7.Text) + int.Parse(used_items_DGV.Rows[j].Cells[3].Value.ToString()));
            }

            if (label7.Text == "0")
            {
                MessageBox.Show("No Results from the Selected Range", "HIT Canteen POS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pictureBox4_Click(object sender, EventArgs e) //Back Icon
        {
            Inventory In = new Inventory();
            this.Hide();
            In.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e) //minimize icon
        {
            this.WindowState = FormWindowState.Minimized;
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
