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
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
            CategoriesGV.RowTemplate.Height = 50;
            CategoriesGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            CategoriesGV.ColumnHeadersHeight = 70;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(CataIdTb.Text == "")
                {
                    MessageBox.Show("Please input category ID");
                }else if(CatanameTb.Text == "")
                {
                    MessageBox.Show("Please input category name");
                }
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO CategoryTable VALUES('" + CataIdTb.Text + "','" + CatanameTb.Text + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Added Sucessfully");
                    Con.Close();
                    FillCategory();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE CategoryTable SET CategoryName='" + CatanameTb.Text + "' where CategoryID='" + CataIdTb.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Updated Successfully");
                Con.Close();
                FillCategory();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CataIdTb.Text == "") 
            {
                MessageBox.Show("Enter the Category ID");
            }
            else
            {
                Con.Open();
                String myquery = "DELETE FROM CategoryTable WHERE CategoryID = '" + CataIdTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Deleted Successfully");  
                Con.Close();
                FillCategory();
            }
        }

        
        public void FillCategory()
        {
            try
            {
                Con.Open();
                String MyQuery = "SELECT * FROM CategoryTable";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategoriesGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        private void CataIdTb_MouseClick(object sender, MouseEventArgs e)
        {
            CataIdTb.Text = "";
        }

        private void CatanameTb_MouseClick(object sender, MouseEventArgs e)
        {
            CatanameTb.Text = "";
        }

        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          //  CataIdTb.Text = CategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
          //  CatanameTb.Text = CategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            FillCategory();
        }

        private void CategoriesGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CataIdTb.Text = CategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
            CatanameTb.Text = CategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
        }
    }
}
