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
    public partial class SignUp : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        //set connection string
        public SignUp()
        {
            InitializeComponent();
            con = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //form load to set input tips
        private void SignUp_Load(object sender, EventArgs e)
        {
            label2.Select();
            toolTip1.SetToolTip(txtUserName, "The User name cannot be blank and should be between 1-8 characters.");
            toolTip1.SetToolTip(txtPass, "The password cannot be empty.");
            toolTip1.SetToolTip(txtRePass, "The repeat password cannot be empty and should be same as password.");
        }

        //function to close the form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //function to sign up a new staff
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            //check whether all the fields is filled
            if (txtUserName.Text.Trim() == String.Empty || txtPass.Text.Trim() == String.Empty || txtRePass.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please fill all required fields!");
            }
            else
            {
                try
                {
                    using (cmd = new SqlCommand("Insert into staff values(@name,@password)", con))
                    {
                        cmd.Parameters.AddWithValue("name", txtUserName.Text);
                        cmd.Parameters.AddWithValue("password", txtPass.Text);
                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        if (x == 1)
                        {
                            MessageBox.Show("Register successfully.");
                            txtPass.Text = "";
                            txtUserName.Text = "";
                            txtRePass.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Something is wrong,please try again later!");
                        }
                    }

                }
                catch (SqlException obj)
                {
                    MessageBox.Show("Something is wrong, the user name may be exist");
                    txtUserName.Select();
                    txtPass.Text = "";
                    txtRePass.Text = "";
                }
            }
        }

        private void txtUserName_Validated(object sender, EventArgs e)
        {
            txtPass.Focus();
            errorProvider1.SetError(txtUserName, "");

        }

        //chek the user name
        public bool ValidUserName(string userName, out string errorMessage)
        {
            ////chek the user name is empty or not
            if (userName.Trim() == String.Empty)
            {
                errorMessage = "User name is required.";
                return false;
            }
            else if (userName.Length < 1 || userName.Length > 8)
            {
                errorMessage = "User name should be between 1 to 8 characters.";
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

        private void txtPass_Validated(object sender, EventArgs e)
        {

            txtRePass.Focus();
            errorProvider1.SetError(txtPass, "");
            txtRePass.Enabled = true;
        }

        //chek the user password
        public bool ValidPassword(string password, out string errorMessage)
        {
            //password cannot be empty ,check the password
            if (password.Trim() == String.Empty)
            {
                errorMessage = "Password is required.";
                txtRePass.Enabled = false;
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private void txtPass_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidPassword(txtPass.Text, out errorMsg))
            {
                //cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                txtPass.Select(0, txtPass.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(txtPass, errorMsg);
            }
            if (e.Cancel == false)
            {
                this.errorProvider1.Clear();
            }
        }

        private void txtRePass_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtRePass, "");
        }

        //check the repeat password
        public bool ValidRepeatPassword(string repeatPassword, string password, out string errorMessage)
        {
            //repeat password cannot be empty
            if (repeatPassword.Trim() == String.Empty)
            {
                errorMessage = "Repeat password is required.";
                return false;

            }
            //repeat password must be same as password
            else if ((repeatPassword.Trim()) != (password.Trim()))
            {
                errorMessage = "Password should be same!";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private void txtRePass_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidRepeatPassword(txtRePass.Text, txtPass.Text, out errorMsg))
            {
                //cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                txtRePass.Select(0, txtRePass.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(txtRePass, errorMsg);
            }
            if (e.Cancel == false)
            {
                this.errorProvider1.Clear();
            }
        }
    }
}
