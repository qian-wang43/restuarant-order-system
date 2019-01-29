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
    public partial class Update : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        BindingSource bs;
        SqlDataReader rdr;

        // set connet string
        public Update()
        {
            InitializeComponent();
            con = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //founction to close the form
        private void Update_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        //founction to click cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //form load founction to connetion to the database and set gridview
        private void Update_Load(object sender, EventArgs e)
        {
            label1.Select();
            toolTip1.SetToolTip(TxtName, "The dish name cannot be blank and should be between 0-20 characters.");
            toolTip1.SetToolTip(TxtPrice, "The Price cannot be 0.");
            toolTip1.SetToolTip(CbbType, "Must choose one of the types.");
            toolTip1.SetToolTip(RtbDesc, "The description cannot be empty.");
            try
            {
                con.Open();
                using (cmd = new SqlCommand("Select * from menu", con))
                {

                    using (rdr = cmd.ExecuteReader())
                    {
                        bs = new BindingSource();
                        bs.DataSource = rdr;
                        dataGridView1.DataSource = bs;
                        con.Close();
                    }

                }
            }
            catch (SqlException obj)
            {
                con.Close();
                MessageBox.Show(obj.Message);
            }

            catch (Exception obj)
            {
                MessageBox.Show("No dish in the menu!");
                this.Close();
            }
        }

        private void txtDishName_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(TxtName, "");
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

        private void txtDishName_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidDishName(TxtName.Text, out errorMsg))
            {
                //cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                TxtName.Select(0, TxtName.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(TxtName, errorMsg);
            }
            if (e.Cancel == false)
            {
                this.errorProvider1.Clear();
            }
        }



        private void txtPrice_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(TxtPrice, "");
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

            errorMessage = ("Dish's price must be a number and greater than 0(Format:1.23)!!!");
            return false;

        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidPrice(TxtPrice.Text, out errorMsg))
            {
                //Cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                TxtPrice.Select(0, TxtPrice.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(TxtPrice, errorMsg);
            }
            if (e.Cancel == false)
            {
                this.errorProvider1.Clear();
            }
        }

        private void cbbType_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(CbbType, "");
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
            if (!ValidType(CbbType.Text, out errorMsg))
            {
                //Cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                CbbType.Select(0, CbbType.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(CbbType, errorMsg);
            }
            if (e.Cancel == false)
                this.errorProvider1.Clear();
        }

        private void rtbDesc_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(RtbDesc, "");
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
            if (!ValidDescription(RtbDesc.Text, out errorMsg))
            {
                //cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                RtbDesc.Select(0, RtbDesc.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(RtbDesc, errorMsg);
            }
            if (e.Cancel == false)

                this.errorProvider1.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            TxtName.Text = row.Cells["name"].Value.ToString();
            TxtPrice.Text = row.Cells["price"].Value.ToString();
            CbbType.Text = row.Cells["type"].Value.ToString();
            RtbDesc.Text = row.Cells["description"].Value.ToString();

        }

        //function for undate button to update the information
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (TxtName.Text.Trim() == String.Empty || TxtPrice.Text.Trim() == String.Empty || CbbType.Text.Trim() == String.Empty || RtbDesc.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please fill all required fields!");
            }
            else
            {
                try
                {

                    using (cmd = new SqlCommand("SELECT COUNT(1) FROM menu where name=@name", con))
                    {
                        cmd.Parameters.AddWithValue("name", TxtName.Text);
                        con.Open();
                        int qty = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        if (qty == 0)
                        {
                            MessageBox.Show("Dish does not exist!");
                        }
                        else
                        {
                            //Insert into movie values(@ISBN,@movieName,@releaseDate,@location,@genre,@rating,@duration,@price)
                            using (cmd = new SqlCommand("Update menu Set price=@price,type=@type,description=@description WHERE name=@name", con))
                            {
                                cmd.Parameters.AddWithValue("name", TxtName.Text);
                                cmd.Parameters.AddWithValue("price", Convert.ToDouble(TxtPrice.Text));
                                cmd.Parameters.AddWithValue("type", CbbType.SelectedItem);
                                cmd.Parameters.AddWithValue("description", RtbDesc.Text);
                                con.Open();
                                int x = cmd.ExecuteNonQuery();
                                con.Close();
                                if (x == 1)
                                {
                                    MessageBox.Show("Dish's information Updated");
                                }
                                else
                                {
                                    MessageBox.Show("Dish's name cannot be updated!!!");
                                }
                                con.Open();
                                cmd = new SqlCommand("Select * from menu", con);
                                using (rdr = cmd.ExecuteReader())
                                {
                                    bs.DataSource = rdr;
                                    con.Close();
                                    dataGridView1.DataSource = bs;
                                }
                            }
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
}

