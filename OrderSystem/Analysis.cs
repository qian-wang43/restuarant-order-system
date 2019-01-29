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
    public partial class Analysis : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        bool hasTitle = false;
        public Analysis()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            chart1.Visible = false;
            string start = dateTimePickerStart.Value.ToString("yyyy-MM-dd");
            string end = dateTimePickerEnd.Value.ToString("yyyy-MM-dd");
            string name;
            int qty = 0;
            List<string> names=new List<string>();
            List<int> qtys = new List<int>();
            bool check = false;
            
            using (cmd = new SqlCommand("Select Name,Sum(Qty) from record where Date between '" +
                start+"' and '"+end+"' group by Name", con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr.IsDBNull(1))
                    {
                       
                        check = false;
                    }
                    else
                    {
                        name = rdr.GetString(0);
                        names.Add(name);
                        qty = Convert.ToInt32(rdr.GetValue(1));
                        qtys.Add(qty);
                        check = true;
                       
                    }
                   

                }
                con.Close();
            }
            if (check == true)
            {
                chart1.Series[0].Points.DataBindXY(names, qtys);
                if (hasTitle == true)
                    chart1.Titles.RemoveAt(0);
                chart1.Titles.Add("Sales Volumn between " + start + " and " + end);
                hasTitle = true;
                chart1.Visible = true;
            }
            else
                MessageBox.Show("no data.", "ERROR");


        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Management managementForm = new Management();
            managementForm.ShowDialog();
            this.Close();
        }

        private void Analysis_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePickerStart_Validating(object sender, CancelEventArgs e)
        {
            if (dateTimePickerEnd.Value < dateTimePickerStart.Value)
            {
                e.Cancel = true;
                this.errorProvider1.SetError(dateTimePickerStart, "The end date should be equal or greater than the start date");

            }
        }

        private void dateTimePickerEnd_Validating(object sender, CancelEventArgs e)
        {
            if (dateTimePickerEnd.Value < dateTimePickerStart.Value)
            {
                e.Cancel = true;
                this.errorProvider1.SetError(dateTimePickerEnd,"The end date should be equal or greater than the start date");

            }

        }

        private void dateTimePickerEnd_Validated(object sender, EventArgs e)
        {
            this.errorProvider1.Clear();
        }

        private void dateTimePickerStart_Validated(object sender, EventArgs e)
        {
            this.errorProvider1.Clear();
        }
    }
}
