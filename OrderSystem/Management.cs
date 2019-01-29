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
    public partial class Management : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection con1 = new SqlConnection();
        SqlCommand cmd1 = new SqlCommand();

        public Management()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con1.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Restuarant;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private void Management_Load(object sender, EventArgs e)
        {

        }

        private void employeeManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignUp signupForm = new SignUp();
            signupForm.ShowDialog();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignOut signoutForm = new SignOut();
            signoutForm.ShowDialog();
        }

        private void editMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update updateForm = new Update();
            updateForm.ShowDialog();
        }

        private void deleteMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete deleteForm = new Delete();
            deleteForm.ShowDialog();
        }

        private void addMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add addForm = new Add();
            addForm.ShowDialog();
        }

        private void checkOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckOrders checkOrdersFrom = new CheckOrders();
            checkOrdersFrom.ShowDialog();
        }

        private void dailyClosingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm daily closing?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                addToRecord();
                printDaily();
                clearDaily();
  
            }
        }


        private void addToRecord()
        {
            using (cmd = new SqlCommand("Select Name,Price,Qty from totalOrder", con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                  
                    string name = rdr.GetString(0);
                    double price = Convert.ToDouble(rdr.GetValue(1));
                    int qty = Convert.ToInt32(rdr.GetValue(2));
                    string dateStr = DateTime.Now.ToString("yyyy-MM-dd");

                    using (cmd1 = new SqlCommand("Insert into record values (@Date,@Name,@Price,@Qty)", con1))
                    {
                        con1.Open();
                        cmd1.Parameters.AddWithValue("Date", dateStr);
                        cmd1.Parameters.AddWithValue("Name", name);
                        cmd1.Parameters.AddWithValue("Price", price);
                        cmd1.Parameters.AddWithValue("Qty", qty);
                        

                        cmd1.ExecuteNonQuery();
                        con1.Close();
                    }

                }

                con.Close();

            }



        }
        private void printDaily()
        {
            int id=0;
            double price=0;
            double tax=0;
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd");
            bool closed=false;
            string name = "";
            int qty = 0;
            string sale = "The sales volumn of each item is:\r\n";
           
            using (cmd = new SqlCommand("Select Max(Id),Sum(Price*Qty) from totalOrder", con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (rdr.IsDBNull(1)) {
                          
                            closed = true;
                        }
                        else{
                            id = Convert.ToInt32(rdr.GetValue(0));
                            price = Convert.ToDouble(rdr.GetValue(1));
                            tax = Math.Round(price * 0.13, 2);
                            closed = false;

                    }
                           

                    }

                con.Close();
            }
            using (cmd = new SqlCommand("Select Name,Sum(Qty) from totalOrder group by name", con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.IsDBNull(1))
                    {

                        closed = true;
                    }
                    else
                    {
                        name=rdr.GetString(0);
                        qty = Convert.ToInt32(rdr.GetValue(1));
                        closed = false;
                        sale += name + ": " + qty + "\r\n";

                    }


                }

                con.Close();
            }

            if (closed == false) {

                string content = "W3 Restaurant " + dateStr + " Closing\r\n----------------------------\r\nPerson-time: "
                + id + "\r\nSubtotal: " + price + "\r\nHST: " + tax + "\r\nTotal: " + (price + tax)
                + "\r\n----------------------------\r\n"+sale+ "----------------------------\r\nClosing Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                try
                {
                    string activeDir = @"C:\Users\Qian Wang\Desktop";
                    string folderPath = System.IO.Path.Combine(activeDir, dateStr.ToString());
                    if (!Directory.Exists(folderPath))
                        System.IO.Directory.CreateDirectory(folderPath);
                    string str = folderPath + "\\daily.txt";
                    FileStream fs = new FileStream(str, FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(content);
                    sw.Close();
                    System.Diagnostics.Process.Start(@str);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There are something wrong!", "ERROR");
                }


            }
            else
                MessageBox.Show("Already closed today or revenue yet.", "ERROR");


        }
        private void clearDaily()
        {

            using (cmd = new SqlCommand("TRUNCATE TABLE totalOrder", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        private void backToMainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main mainForm = new Main();
            mainForm.ShowDialog();
            this.Close();
        }

        private void analyseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analysis analysisForm = new Analysis();
            analysisForm.ShowDialog();
            this.Close();
        }
    }
}
