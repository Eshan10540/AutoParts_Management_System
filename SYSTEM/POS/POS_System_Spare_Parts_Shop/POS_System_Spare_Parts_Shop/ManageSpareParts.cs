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
    public partial class ManageSpareParts : Form
    {
        public ManageSpareParts()
        {
            InitializeComponent();
            ProductsGV.RowTemplate.Height = 50;
            ProductsGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            ProductsGV.ColumnHeadersHeight = 70;
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

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(161, 154);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(135, 133);
        }

        private void PidTb_MouseClick(object sender, MouseEventArgs e)
        {
            PidTb.Text = "";
        }

        private void PnameTb_MouseClick(object sender, MouseEventArgs e)
        {
            PnameTb.Text = "";
        }

        private void PqTb_MouseClick(object sender, MouseEventArgs e)
        {
            PqTb.Text = "";
        }

        private void PpTb_MouseClick(object sender, MouseEventArgs e)
        {
            PpTb.Text = "";
        }

        private void PdTb_MouseClick(object sender, MouseEventArgs e)
        {
            PdTb.Text = "";
        }

        private void ManageSpareParts_Load(object sender, EventArgs e)
        {
            fillcategory();
            FillProducts();
        }
        public void FillProducts()
        {
            try
            {
                Con.Open();
                string Myquery = "SELECT * FROM ProductTable";
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
            /* PidTb.Text = ProductsGV.SelectedRows[0].Cells[0].Value.ToString();
             PnameTb.Text = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
             PqTb.Text = ProductsGV.SelectedRows[0].Cells[2].Value.ToString();
             PpTb.Text = ProductsGV.SelectedRows[0].Cells[3].Value.ToString();
             PdTb.Text = ProductsGV.SelectedRows[0].Cells[4].Value.ToString();
             CatCombo.SelectedValue = ProductsGV.SelectedRows[0].Cells[5].Value.ToString();*/
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (PidTb.Text == "")
                {
                    MessageBox.Show("Please Enter the Product ID");
                }
                else if (PnameTb.Text == "")
                {
                    MessageBox.Show("Please Enter the Product Name");
                }
                else if (PpTb.Text == "" || PpTb.Text.Contains(","))
                {
                    MessageBox.Show("Please Enter the Valid Product Price");
                }
                else if (PqTb.Text == "")
                {
                    MessageBox.Show("Please Enter the Product Quantity");
                }
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO ProductTable VALUES('" + PidTb.Text + "','" + PnameTb.Text + "','" + PqTb.Text + "','" + PpTb.Text + "','" + PdTb.Text + "','" + CatCombo.SelectedValue.ToString() + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Added");
                    Con.Close();
                    FillProducts();
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
                SqlCommand cmd = new SqlCommand("UPDATE ProductTable SET ProductName='" + PnameTb.Text + "',ProductQuantity='" + PqTb.Text + "',ProductPrice='" + PpTb.Text + "',ProductDescription='" + PdTb.Text + "',ProductCategory='" + CatCombo.SelectedValue.ToString() + "' WHERE ProductID='" + PidTb.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Updated");
                Con.Close();
                FillProducts();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (PidTb.Text == "")
            {
                MessageBox.Show("Enter the Product ID");
            }
            else
            {
                Con.Open();
                string myquery = "DELETE FROM ProductTable WHERE ProductID = '" + PidTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Deleted");
                Con.Close();
                FillProducts();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillProducts();
        }
        void filterbycategory()
        {
            try
            {
                if (SearchCombo.SelectedValue == null )
                {
                    MessageBox.Show("No Products Found");
                }
                else
                {
                    Con.Open();
                    string Myquery = "SELECT * FROM ProductTable WHERE ProductCategory='" + SearchCombo.SelectedValue.ToString() + "'";
                    SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    var ds = new DataSet();
                    da.Fill(ds);
                    ProductsGV.DataSource = ds.Tables[0];
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        

        }
    
        void fillcategory()
        {
            string query = "SELECT * FROM CategoryTable";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CategoryName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CategoryName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CategoryName";
                SearchCombo.DataSource = dt;

                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ProductsGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PidTb.Text = ProductsGV.SelectedRows[0].Cells[0].Value.ToString();
            PnameTb.Text = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            PqTb.Text = ProductsGV.SelectedRows[0].Cells[2].Value.ToString();
            PpTb.Text = ProductsGV.SelectedRows[0].Cells[3].Value.ToString();
            PdTb.Text = ProductsGV.SelectedRows[0].Cells[4].Value.ToString();
            CatCombo.SelectedValue = ProductsGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void PpTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void PidTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // Suppress the key press
            }
        }
    }
}
