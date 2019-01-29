using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantOrderSystem
{
    public partial class SignOut : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        BindingSource bs;
        SqlDataReader rdr;

        //set connect string
        public SignOut()
        {
            InitializeComponent();
            con = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //function to go back to home page
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //form load to connect to the database and set the gridview
        private void SignOut_Load(object sender, EventArgs e)
        {
            label1.Select();
            toolTip1.SetToolTip(txtUserName, "Enter the staff name to sign out, it cannot be empty.");
            try
            {
                con.Open();
                using (cmd = new SqlCommand("Select userName from staff", con))
                {
                    using (rdr = cmd.ExecuteReader())
                    {
                        bs = new BindingSource();
                        bs.DataSource = rdr;
                        dataGridViewUser.DataSource = bs;
                        con.Close();
                    }
                }
            }
            catch (SqlException obj)
            {
                con.Close();
                MessageBox.Show(obj.Message);
            }
        }

        private void txtUserName_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtUserName, "");
        }

        //check the staff's user name
        public bool ValidUserName(string userName, out string errorMessage)
        {
            //check if the name is empty or not 
            if (userName.Trim() == String.Empty)
            {
                errorMessage = "User name is required.";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidUserName(txtUserName.Text, out errorMsg))
            {
                //cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                txtUserName.Select(0, txtUserName.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(txtUserName, errorMsg);
            }
            if (e.Cancel == false)
            {
                this.errorProvider1.Clear();
            }
        }

        //function for signout btn to delete the staff access permission
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == String.Empty)
            {
                MessageBox.Show("User name cannot be empty!");
            }
            else
            {
                MessageBoxButtons message = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show("Are you sure to sign out?", "Sign out?", message);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        using (cmd = new SqlCommand("Delete from staff WHERE userName=@name", con))
                        {
                            cmd.Parameters.AddWithValue("name", txtUserName.Text);
                            con.Open();
                            int x = cmd.ExecuteNonQuery();
                            con.Close();
                            if (x == 1)
                            {
                                MessageBox.Show("Sign out sucessfully!");
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong, the staff may be not exist!!!");
                            }
                            con.Open();
                            cmd = new SqlCommand("Select userName from staff", con);
                            using (rdr = cmd.ExecuteReader())
                            {
                                bs.DataSource = rdr;
                                con.Close();
                                txtUserName.Text = "";
                                dataGridViewUser.DataSource = bs;
                            }
                        }
                    }
                    catch (SqlException obj)
                    {
                        con.Close();
                        MessageBox.Show(obj.Message);
                    }
                }
            }
        }

        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridViewUser.Rows[e.RowIndex];
            txtUserName.Text = row.Cells["userName"].Value.ToString();
        }
    }
}
