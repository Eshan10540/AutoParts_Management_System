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
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
            OrderGv.RowTemplate.Height = 50;
            OrderGv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            OrderGv.ColumnHeadersHeight = 70;

            CustomersGV.RowTemplate.Height = 50;
            CustomersGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            CustomersGV.ColumnHeadersHeight = 70;

            ProductsGV.RowTemplate.Height = 50;
            ProductsGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            ProductsGV.ColumnHeadersHeight = 70;
        }
        DataTable table = new DataTable();
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\SparePartsShopDB.mdf;Integrated Security=True;Connect Timeout=30");

        //Declare the variables to Use The Calculate Part
        int num = 0;
        double uprice, totprice;
        int qty;
        string product;


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

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void ManageOrders_Load(object sender, EventArgs e)
        {
            FillOrders(); 
            populateproducts();
            fillcategory();

            table.Columns.Add("OrderID", typeof(string));
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UnitPrice", typeof(double));
            table.Columns.Add("TotalPrice", typeof(double));

            OrderGv.DataSource = table; 
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(161, 154);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(135, 133);
        }
        public void FillOrders()
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
        void populateproducts()
        {
            try
            {
                Con.Open(); 
                string Myquery = "select * from ProductTable"; 
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con); 
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); 
            }
        }
        void fillcategory()
        {
            string query = "select * from CategoryTable"; 
            SqlCommand cmd = new SqlCommand(query, Con); 
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable(); 
                dt.Columns.Add("CategoryName", typeof(string)); 
                rdr = cmd.ExecuteReader();
                dt.Load(rdr); 
                SearchCombo.ValueMember = "CategoryName"; 
                SearchCombo.DataSource = dt; 
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); 
            }
        }
        void updateproduct()
        {

            int id = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[0].Value.ToString());
            
            if (QtyTb.Text == "") {
                MessageBox.Show("Please Insert the Quantity Value");
            }
            else {

                int newQty = stock - int.Parse(QtyTb.Text);


                if (newQty < 0)
                    MessageBox.Show("Operation Failed");

                else
                {
                    try
                    {
                        Con.Open();
                        string query = "update ProductTable set ProductQuantity = " + newQty + " where ProductID= " + id + ";";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        Con.Close();
                        populateproducts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            } 
        }
        int flag = 0;
        int stock;

        private void insertOrderBtn_Click(object sender, EventArgs e)
        {
            Random randomid = new Random();
            int randomNumber = randomid.Next(0, 100000);
            string formattedNumber = randomNumber.ToString("D5");
            OrderIdTb.Text = formattedNumber;

            if (OrderIdTb.Text == "" || TotAmount.Text == "")
            {
                MessageBox.Show("Fill the data Correctly");
            }
            else
            {

                try
                {
                    int id = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[0].Value.ToString());
                    int QT = int.Parse(QtyTb.Text);

                    Con.Open();
                    if (CnameTb.Text == "" && CidTb.Text == "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO OrderTable(OrderID, ProductID, ProductName, ProductPrice, Quantity, OrderDate, TotalAmount) VALUES (" + OrderIdTb.Text + ", " + id + ", '" + product + "', '" + uprice + "', " + QT + ", '" + orderdate.Value.ToString("yyyy-MM-dd") + "', " + TotAmount.Text + ")", Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order Added Successfully");
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO OrderTable(OrderID, CustomerID, CustomerName,ProductID, ProductName, ProductPrice, Quantity, OrderDate, TotalAmount) VALUES (" + OrderIdTb.Text + ", " + CidTb.Text + ", '" + CnameTb.Text + "', " + id + ", '" + product + "', '" + uprice + "', " + QT + ", '" + orderdate.Value.ToString("yyyy-MM-dd") + "', " + TotAmount.Text + ")", Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order Added Successfully");
                    }
                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }


            }
        }

        double sum = 0;

        private void viewOrderBtn_Click(object sender, EventArgs e)
        {
            PrintBill view = new PrintBill();
            view.Show(); 
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
            {
                MessageBox.Show("Enter the Quantity of Product");
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select the Product");
            }
            else if (Convert.ToInt32(QtyTb.Text) > stock)
            {
                MessageBox.Show("No Enough Stocks Available");
            }
            else
            {
                num = num + 1;
                qty = Convert.ToInt32(QtyTb.Text);
                totprice = qty * uprice;
                table.Rows.Add(num, product, qty, uprice, totprice);
                OrderGv.DataSource = table;
                flag = 0;
            }

            sum = sum + totprice;
            TotAmount.Text = sum.ToString();

            updateproduct();
        }

        private void SearchCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Con.Open();

                string Myquery = "select * from ProductTable where ProductCategory='" + SearchCombo.SelectedValue.ToString() + "'";

                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {     
                  //--------------    
        }
        

        private void CustomersGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CidTb.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CnameTb.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void ProductsGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            product = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            stock = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[2].Value.ToString());
            uprice = Convert.ToDouble(ProductsGV.SelectedRows[0].Cells[3].Value.ToString());

            flag = 1;
        }

        private void OrderGv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //--------
        }

        private void SearchBTN_Click(object sender, EventArgs e)
        {
          
            try
            {
                Con.Open();
                String MyQuery = "SELECT * FROM ProductTable WHERE ProductID='" + SearchTEXT.Text + "'";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void QtyTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void QtyTb_MouseClick(object sender, MouseEventArgs e)
        {
            QtyTb.Text = "";
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {          
           //-------
        }

        
    }
}
