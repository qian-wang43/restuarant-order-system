using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantOrderSystem
{
    public partial class Login : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public Login()
        {
            InitializeComponent();
            con = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        private void login_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == String.Empty || txtPass.Text.Trim() == String.Empty)
            {
                MessageBox.Show("User name or password cannot be empty!");
            }
            else
            {
                try
                {
                    using (cmd = new SqlCommand("SELECT * FROM staff WHERE userName=@userName AND password=@password", con))
                    {
                        cmd.Parameters.AddWithValue("userName", txtUserName.Text);
                        cmd.Parameters.AddWithValue("password", txtPass.Text);
                        con.Open();
                        //int x = Convert.ToInt32(cmd.ExecuteScalar());


                        if (cmd.ExecuteScalar() == null)
                        {
                            MessageBox.Show("Please make sure your user name and password are both valid!");
                            txtPass.Text = "";
                            txtPass.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Welcome to 3W Restaurant order system!");
                            this.Hide();
                            Management manageForm = new Management();
                            manageForm.ShowDialog();
                            this.Close();

                        }
                        con.Close();
                    }

                }catch(SqlException obj)
                {
                    con.Close();
                    MessageBox.Show(obj.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main mainForm = new Main();
            mainForm.ShowDialog();
            this.Close();
        }
    }
}
