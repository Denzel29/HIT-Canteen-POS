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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        MySqlConnection connection = new MySqlConnection("server=localhost; database=water; username=root; password=;");
    
    
       
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void ExitBtn_Click_1(object sender, EventArgs e)
        {
            DialogResult appClose = MessageBox.Show("Are You Sure You Want to Exit", "WATER RATIONING  SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (appClose == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.adminRadBtn.Checked = false;
            usernameTxt.Clear();
            passTxt.Clear();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {

            string username = usernameTxt.Text;
            string password = passTxt.Text;

            if (this.adminRadBtn.Checked)
            {
                if (username == "admin" && password == "management")
                {
                    DialogResult appClose = MessageBox.Show("Admin Login Successful", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    adminDashboard In = new adminDashboard();
                    this.Hide();
                    In.Show();
                }
                else
                {
                    DialogResult appClose = MessageBox.Show("INVALID ADMIN LOGIN", "WATER RATIONING  SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.adminRadBtn.Checked = false;
                    usernameTxt.Text = "";
                    passTxt.Text = "";
                }
            }

            else
            {

                DB db = new DB();

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();               
                MySqlCommand command = new MySqlCommand("SELECT* FROM `users` WHERE `username` = @user AND `password` = @pass", db.GetConnection());

                command.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;

                adapter.SelectCommand = command;

                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    DialogResult appClose = MessageBox.Show("Login Successful", "WATER RATIONING SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dash In = new dash();
                    this.Hide();
                    In.Show();

                }
                else
                {
                    DialogResult result = MessageBox.Show("Login Failed", "WATER RATIONING SYSTEM", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.Retry)
                    {
                        this.adminRadBtn.Checked = false;
                        usernameTxt.Clear();
                        passTxt.Clear();
                    }
                    else
                    {
                        System.Windows.Forms.Application.ExitThread();
                    }
                }
            }

        }
    }
}
