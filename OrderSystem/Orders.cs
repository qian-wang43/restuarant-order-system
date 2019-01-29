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
    public partial class Orders : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        BindingSource bs = new BindingSource();
        string name;
        int qty;
        double amount;
        public Orders()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            showData();
            showAmount();
        }
        private void showData()
        {
            cmd = new SqlCommand("Select Name, Price, Qty from totalOrder Where id = @Id", con);
            cmd.Parameters.AddWithValue("Id", Staff.id);
            con.Open();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {

                    bs.DataSource = rdr;

                    dataGridView1.DataSource = bs;
                

                
                con.Close();
                
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main mainForm = new Main();
            mainForm.ShowDialog();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (cmd = new SqlCommand("Select Qty from totalOrder Where Name = @Name", con))
                {
                    cmd.Parameters.AddWithValue("Name", name);
                    con.Open();
                    qty = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                if (qty > 1)
                {
                    using (cmd = new SqlCommand("Update totalOrder Set Qty=@Qty WHERE Name=@Name", con))
                    {
                        qty--;
                        cmd.Parameters.AddWithValue("Name", name);
                        cmd.Parameters.AddWithValue("Qty", qty);
                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show(name + " X " + qty);
                    }
                    showData();
                    showAmount();
                }
                else
                {
                    using (cmd = new SqlCommand("Delete from totalOrder where Id = (@Id) and Name = (@Name)", con))
                    {
                        cmd.Parameters.AddWithValue("Id", Staff.id);
                        cmd.Parameters.AddWithValue("Name", name);
                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();
                        if (x == 1)
                            MessageBox.Show("Item Delete");
                        else
                            MessageBox.Show("Pick a item first!!!");
                        showData();
                    }
                    showAmount();
                }
            }
            catch (SqlException obj)
            {
                con.Close();
                MessageBox.Show("Pick a item first!!!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                name = row.Cells["Name"].Value.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show("Do not click tag name!!!");
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show("Please call waiters");
            this.Hide();
            OrderLogin orderLoginForm = new OrderLogin();
            orderLoginForm.ShowDialog();
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void showAmount()
        {
            using (cmd = new SqlCommand("SELECT SUM(Price*Qty) FROM totalOrder Where Id = @Id", con))
            {
                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("Id", Staff.id);
                    amount = Convert.ToDouble(cmd.ExecuteScalar());
                    con.Close();
                }catch(Exception e)
                {
                    amount = 0;
                }
            }
            lblAmount.Text = amount.ToString();
        }
    }
}
