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
    public partial class Add : Form
    {
        SqlConnection con;
        SqlCommand cmd;

        //add connection string to form
        public Add()
        {
            InitializeComponent();
            con = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //form load to set tips for input
        private void Add_Load(object sender, EventArgs e)
        {
            label1.Select();
            toolTip1.SetToolTip(txtName, "The dish name cannot be blank and should be between 0-20 characters.");
            toolTip1.SetToolTip(txtPrice, "The Price cannot be 0.");
            toolTip1.SetToolTip(cbbType, "Must choose one of the types.");
            toolTip1.SetToolTip(rtbDesc, "The description cannot be empty.");
        }

        //founction for add button to insert new dish
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == String.Empty || txtPrice.Text.Trim() == String.Empty ||cbbType.Text.Trim() == String.Empty || rtbDesc.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please fill all required fields!");
            }
            else
            {
                try
                {
                    using (cmd = new SqlCommand("Insert into menu values(@name,@price,@type,@description)", con))
                    {
                        cmd.Parameters.AddWithValue("name", txtName.Text);
                        cmd.Parameters.AddWithValue("price", Convert.ToDouble(txtPrice.Text));
                        cmd.Parameters.AddWithValue("type", cbbType.SelectedItem);
                        cmd.Parameters.AddWithValue("description", rtbDesc.Text);
                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        if (x == 1)
                        {
                            MessageBox.Show("New Dish Add Successfully!");
                            cbbType.SelectedIndex = -1;
                            txtPrice.Text = "";
                            txtName.Text = "";
                            rtbDesc.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong, the dish may be existed!");
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

        //button to go bak to home page
        private void btnancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtName, "");
        }
        //function to check dish name
        public bool ValidDishName(string dishName, out string errorMessage)
        {
            //check if name is empty
            if (dishName.Trim() == String.Empty)
            {
                errorMessage = "Dish name is required.";
                return false;
            }
            //name's length should between 0 and 28
            else if (dishName.Length < 0 || dishName.Length > 20)
            {
                errorMessage = "Dish name should be between 0 to 20 characters.";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidDishName(txtName.Text, out errorMsg))
            {
                //cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                txtName.Select(0, txtName.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(txtName, errorMsg);
            }
            if (e.Cancel == false)
            {
                this.errorProvider1.Clear();
            }
        }

        private void txtPrice_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtPrice, "");
        }

        //function to check dish price
        public bool ValidPrice(string dishPrice, out string errorMessage)
        {
            //check if the price is empty
            if (dishPrice.Length == 0)
            {
                errorMessage = "Price is required";
                return false;
            }
            //check if the input is 0
            if (Regex.IsMatch(dishPrice, @"^[0]\d*$"))
            {
                errorMessage = "Dish price cannot be free!!!";
                return false;
            }

            //check if the user's input is integer
            if (Regex.IsMatch(dishPrice, @"^(0|[1-9][0-9]{0,9})(\.[0-9]{1,2})?$"))
            {
                errorMessage = "";
                return true;
            }
            errorMessage = ("Dish's price must be a number and greater than 0!!!");
            return false;

        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidPrice(txtPrice.Text, out errorMsg))
            {
                //Cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                txtPrice.Select(0, txtPrice.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(txtPrice, errorMsg);
            }
            if (e.Cancel == false)
            {
                this.errorProvider1.Clear();
            }
        }

        private void cbbType_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(cbbType, "");
        }

        //function to check dish type
        public bool ValidType(string dishType, out string errorMessage)
        {
            //check if the user do not select the genre
            if (dishType.Length == 0)
            {
                errorMessage = "Dish type must be selected!";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private void cbbType_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidType(cbbType.Text, out errorMsg))
            {
                //Cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                cbbType.Select(0, cbbType.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(cbbType, errorMsg);
            }
            if (e.Cancel == false)
                this.errorProvider1.Clear();
        }

        private void rtbDesc_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(rtbDesc, "");
        }

        //function to check dish description
        public bool ValidDescription(string description, out string errorMessage)
        {
            //check if the input is empty
            if (description.Length == 0)
            {
                errorMessage = "Dish description is required";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private void rtbDesc_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidDescription(rtbDesc.Text, out errorMsg))
            {
                //cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                rtbDesc.Select(0, rtbDesc.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(rtbDesc, errorMsg);
            }
            if (e.Cancel == false)

                this.errorProvider1.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
