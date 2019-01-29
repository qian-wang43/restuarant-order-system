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
    public partial class Main : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        BindingSource bs = new BindingSource();

        string name;
        string price;
        int qty = 0;
        public Main()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            if (CheckNull())
            {
                MessageBox.Show("You did not anything yet.");
            }
            else
            {
                this.Hide();
                Orders ordersForm = new Orders();
                ordersForm.ShowDialog();
                this.Close();
            }
        }
        private void showData(string type)
        {
            using (cmd = new SqlCommand("Select * from menu where type = (@type)", con))
            {
                cmd.Parameters.AddWithValue("type", type);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    bs.DataSource = rdr;
                    con.Close();
                    dataGridView1.DataSource = bs;
                }
            }
        }

        private void btnDrink_Click(object sender, EventArgs e)
        {
            showData("Drink");
        }

        private void btnNoodle_Click(object sender, EventArgs e)
        {
            showData("Noodle");
        }

        private void btnSoup_Click(object sender, EventArgs e)
        {
            showData("Soup");
        }

        private void btnRice_Click(object sender, EventArgs e)
        {
            showData("Rice");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckRepeat(name))
                {
                    using (cmd = new SqlCommand("Select Qty from totalOrder Where Name = @Name and Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("Name", name);
                        cmd.Parameters.AddWithValue("Id", Staff.id);
                        con.Open();
                        qty = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    using (cmd = new SqlCommand("Update totalOrder Set Qty=@Qty WHERE Name=@Name and Id = @Id", con))
                    {
                        qty++;
                        cmd.Parameters.AddWithValue("Name", name);
                        cmd.Parameters.AddWithValue("Id", Staff.id);
                        cmd.Parameters.AddWithValue("Qty", qty);
                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show(name + " X " + qty);
                    }
                }
                else
                {
                    using (cmd = new SqlCommand("Insert into totalOrder values (@Id,@Name,@Price,@Qty)", con))
                    {
                        cmd.Parameters.AddWithValue("Id", Staff.id);
                        cmd.Parameters.AddWithValue("Name", name);
                        cmd.Parameters.AddWithValue("Price", Convert.ToDouble(price));
                        cmd.Parameters.AddWithValue("Qty", 1);
                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show(name + " X 1");
                    }
                }
            }
            catch (Exception obj)
            {
                con.Close();
                MessageBox.Show("Pick a item First!!!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                name = row.Cells["name"].Value.ToString();
                price = row.Cells["price"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Do not click tag name!!!");
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private bool CheckRepeat(string name)
        {
            int count;

            using (cmd = new SqlCommand("Select COUNT(1) from totalOrder Where Name = @Name and Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("Id", Staff.id);
                con.Open();
                count = (int)cmd.ExecuteScalar();
                con.Close();
            }

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckNull()
        {
            int count;

            using (cmd = new SqlCommand("Select COUNT(*) from totalOrder Where Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("Id", Staff.id);
                con.Open();
                count = (int)cmd.ExecuteScalar();
                con.Close();
            }

            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Error ");
        }
    }
}
