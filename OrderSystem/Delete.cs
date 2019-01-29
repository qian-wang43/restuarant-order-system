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
    public partial class Delete : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        BindingSource bs;
        SqlDataReader rdr;
        //set connection string
        public Delete()
        {
            InitializeComponent();
            con = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //form load to connect to the database and set gridview
        private void Delete_Load(object sender, EventArgs e)
        {
            label1.Select();
            toolTip1.SetToolTip(txtDishName, "Enter the dish name to delete, it cannot be empty.");
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

        //close the form
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDishName_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtDishName, "");
        }

        //founction to check dish name whether is empty or not
        public bool ValidDishName(string dishName, out string errorMessage)
        {
            //check if the name is empty
            if (dishName.Trim() == String.Empty)
            {
                errorMessage = "Dish's name is required.";
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
            if (!ValidDishName(txtDishName.Text, out errorMsg))
            {
                //cancel the event and select the text to be corrected by the user
                e.Cancel = true;
                txtDishName.Select(0, txtDishName.Text.Length);
                //Set the ErrorProvider error with the text to display
                this.errorProvider1.SetError(txtDishName, errorMsg);
            }
            if (e.Cancel == false)
            {
                this.errorProvider1.Clear();
            }
        }

        //delete btn to delete the dish in the menu
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //check if the id is empty
            if (txtDishName.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Dish name cannot be empty!");
            }
            else
            {
                MessageBoxButtons message = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show("Are you sure to delete the dish?", "Delete", message);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        using (cmd = new SqlCommand("Delete from menu WHERE name=@name", con))
                        {
                            cmd.Parameters.AddWithValue("name", txtDishName.Text);
                            con.Open();
                            int x = cmd.ExecuteNonQuery();
                            con.Close();
                            if (x == 1)
                            {
                                MessageBox.Show("Delete dish sucessfully!");
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong, the dish maybe not exist!!!");
                            }
                            con.Open();
                            cmd = new SqlCommand("Select * from menu", con);

                            using (rdr = cmd.ExecuteReader())
                            {
                                bs.DataSource = rdr;
                                con.Close();
                                txtDishName.Text = "";
                                dataGridView1.DataSource = bs;
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
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtDishName.Text = row.Cells["name"].Value.ToString();
            }
            catch (Exception obj)
            {
                MessageBox.Show("No dish in the menu!");
                this.Close();
            }
        }
    }
}
