using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantOrderSystem
{
    public partial class Staff : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection con1 = new SqlConnection();
        SqlCommand cmd1 = new SqlCommand();
        public static int id = 1;
        string info;
        public Staff()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con1.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Orders ordersForm = new Orders();
            ordersForm.ShowDialog();
            this.Close();
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            getInfo();

            //   string filepath = @"C:\Users\Weijie Zheng\Desktop\orders\order.txt";
            //string filepath = @"C:\Users\Qian Wang\Desktop\orders\order.txt";
            //StreamWriter sw = new StreamWriter(filepath, false, Encoding.Default);
            //sw.WriteLine("ID: " + id + "\r\n" + info);
            //sw.Close();
            //MessageBox.Show("Send order Successfully");
            try
            {
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+ "\\orders";
                if (!Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);
                string str = dir + "\\order.txt";
                FileStream fs = new FileStream(str, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("ID: " + id + "\r\n" + info);
                sw.Close();
                //               System.Diagnostics.Process.Start(@str);
                MessageBox.Show("Send order Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("There are something wrong!", "ERROR");
            }

        }

        private void btnPay_Click(object sender, EventArgs e)
        {

            double amount;
            using (cmd = new SqlCommand("SELECT SUM(Price*Qty) FROM totalOrder", con))
            {
                con.Open();
                amount = Convert.ToDouble(cmd.ExecuteScalar());
                con.Close();
            }

            double HST = Math.Round((amount * 0.13), 2);
            double Total = Math.Round((amount * 1.13), 2);
            getPayment();

            string date = DateTime.Now.ToString("yyyy-MM-dd");
         //   string activeDir = @"C:\Users\Weijie Zheng\Desktop";
            string activeDir = @"C:\Users\Qian Wang\Desktop"; 
            string folderPath = System.IO.Path.Combine(activeDir, date.ToString());
            System.IO.Directory.CreateDirectory(folderPath);

        //    string filepath = @"C:\Users\Weijie Zheng\Desktop\"+ date.ToString()+"\\"+Staff.id+".txt";
            string filepath = @"C:\Users\Qian Wang\Desktop\" + date.ToString() + "\\" + Staff.id + ".txt";
            StreamWriter sw = new StreamWriter(filepath, false, Encoding.Default);
            sw.WriteLine("Welcome To W3 Restaurant\r\n-------------------------------\r\n"
                + "# " + Staff.id
                + "\r\nQty     Item        Amount\r\n"
                + info
                + "-------------------------------"
                + "\r\n   Subtotal         " + amount
                + "\r\n   HST              " + HST
                + "\r\n-------------------------------"
                + "\r\n   Total            " + Total
                + "\r\nTime: "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); 
            sw.Close();
            MessageBox.Show("Payment Applied");
            id++;
            this.Hide();
            Main mainForm = new Main();
            mainForm.ShowDialog();
            this.Close();
        }

        private void getInfo()
        {
            using (cmd = new SqlCommand("Select Name, Qty from totalOrder Where Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                info = "";
                while (rdr.Read())
                {
                    info += rdr.GetString(0);
                    info += "     ";
                    info += rdr.GetValue(1);
                    info += "\r\n";
                }
                con.Close();
            }
        }

        private void getPayment()
        {
            using (cmd = new SqlCommand("Select Name, Qty, Price from totalOrder Where Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                info = "";
                while (rdr.Read())
                {
                    info += rdr.GetValue(1);
                    info += "     ";
                    info += rdr.GetString(0);
                    info += "     ";
                    info += rdr.GetValue(2);
                    info += "\r\n";
                }
                con.Close();
            }
        }






        private void Staff_Load(object sender, EventArgs e)
        {

        }
    }
}
