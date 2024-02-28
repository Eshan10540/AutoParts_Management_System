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

namespace POS_System_Spare_Parts_Shop
{
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
            CustomersGV.RowTemplate.Height = 50;
            CustomersGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            CustomersGV.ColumnHeadersHeight = 70;
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\SparePartsShopDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ManageUsers mu = new ManageUsers();
            mu.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ManageCategories cat = new ManageCategories();
            cat.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ManageSpareParts sp = new ManageSpareParts();
            sp.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ManageCustomers cus = new ManageCustomers();
            cus.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ManageOrders pb = new ManageOrders();
            pb.Show();
            this.Hide();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(161, 154);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(135, 133);
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(161, 154);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(135, 133);
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(161, 154);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(135, 133);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(161, 154);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(135, 133);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(161, 154);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(135, 133);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CnameTb_MouseClick(object sender, MouseEventArgs e)
        {
            CnameTb.Text = "";
        }

        private void CphoneTb_MouseClick(object sender, MouseEventArgs e)
        {
            CphoneTb.Text = "";
        }

        private void CidTb_MouseClick(object sender, MouseEventArgs e)
        {
            CidTb.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (CidTb.Text == "")
                {
                    MessageBox.Show("Please Enter the Customer ID");
                }
                else if (CnameTb.Text == "")
                {
                    MessageBox.Show("Please Enter the Customer Name");
                }
                else if (CphoneTb.Text == "")
                {
                    MessageBox.Show("Please Enter the Customer Phone Number");
                }
                                
                else if (CphoneTb.Text.Length != 10)
                {
                    MessageBox.Show("Phone Number Must have a 10 Numbers");
                }
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into CustomerTable values('" + CidTb.Text + "','" + CnameTb.Text + "','" + CphoneTb.Text + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Successfully Added");
                    Con.Close();
                    FillCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); 
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();                        
                SqlCommand cmd = new SqlCommand("update CustomerTable set CustomerName='" + CnameTb.Text + "',CustomerPhoneNo='" + CphoneTb.Text + "' where CustomerID='" + CidTb.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated");
                Con.Close();
                FillCustomer();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CidTb.Text == "") 
            {
                MessageBox.Show("Enter the Customer ID");
            }
            else
            {
                Con.Open();               
                string myquery = "delete from CustomerTable where CustomerID = '" + CidTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Deleted"); 
                Con.Close();
                FillCustomer();
            }
        }

       private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {  
           /* CidTb.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CnameTb.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
            CphoneTb.Text = CustomersGV.SelectedRows[0].Cells[2].Value.ToString();

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from OrderTable where CustomerID = " + CidTb.Text + "", Con);
            DataTable dt = new DataTable();    
            sda.Fill(dt); 
            OrderLabel.Text = dt.Rows[0][0].ToString(); 
       
            SqlDataAdapter sda1 = new SqlDataAdapter("Select Sum(TotalAmount) from OrderTable where CustomerID = " + CidTb.Text + "", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            AmountLabel.Text = dt1.Rows[0][0].ToString();
            
            SqlDataAdapter sda2 = new SqlDataAdapter("Select Max(OrderDate) from OrderTable where CustomerID = " + CidTb.Text + "", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            DateLabel.Text = dt2.Rows[0][0].ToString();
            Con.Close();*/
        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            FillCustomer();
        }
        public void FillCustomer()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CustomerTable"; 
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CustomersGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CidTb.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CnameTb.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
            CphoneTb.Text = CustomersGV.SelectedRows[0].Cells[2].Value.ToString();

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from OrderTable where CustomerID = " + CidTb.Text + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            OrderLabel.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("Select Sum(TotalAmount) from OrderTable where CustomerID = " + CidTb.Text + "", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            AmountLabel.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("Select Max(OrderDate) from OrderTable where CustomerID = " + CidTb.Text + "", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            DateLabel.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }

        private void CphoneTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void CidTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // Suppress the key press
            }
        }
    }
}
