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
    public partial class CheckOrders : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        BindingSource bs = new BindingSource();
        public CheckOrders()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private void CheckOrders_Load(object sender, EventArgs e)
        {
            showData();
        }
        private void showData()
        {
            cmd = new SqlCommand("Select * from totalOrder Order by Id", con);
            con.Open();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                bs.DataSource = rdr;
                con.Close();
                dataGridView1.DataSource = bs;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
