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
    public partial class PrintBill : Form
    {
        public PrintBill()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\SparePartsShopDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void btnPB_Click(object sender, EventArgs e)
        {
            if (OrderGv.Rows == null || OrderGv.Rows.Count == 0)
            {
                MessageBox.Show("No Bills Found");
            }
            else
            {
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                try
                {

                    Con.Open();

                    string myQuery = "UPDATE OrderTable SET Status = '1' WHERE OrderID = @OrderID";

                    using (SqlCommand cmd = new SqlCommand(myQuery, Con))
                    {
                        cmd.Parameters.Add("@OrderID", SqlDbType.NVarChar).Value = OrderGv.SelectedRows[0].Cells[0].Value.ToString();

                        cmd.ExecuteNonQuery();

                    }
                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
           
            
        }
        void populateorders()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from OrderTable where Status = '0' ";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds); 
                OrderGv.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void PrintBill_Load(object sender, EventArgs e)
        {
            populateorders();
           
           

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Order Summary", new Font("Century", 30, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("01.Order Id: " + OrderGv.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 100));
            e.Graphics.DrawString("02.Customer Id: " + OrderGv.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 133));
            e.Graphics.DrawString("03.Customer Name: " + OrderGv.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 166));
            e.Graphics.DrawString("04.Prodcut Id: " + OrderGv.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 199));
            e.Graphics.DrawString("05.Product Name: " + OrderGv.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 232));
            e.Graphics.DrawString("06.Product Price: " + OrderGv.SelectedRows[0].Cells[5].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 265));
            e.Graphics.DrawString("07.Quantity: " + OrderGv.SelectedRows[0].Cells[6].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 298));
            e.Graphics.DrawString("08.Order Date: " + OrderGv.SelectedRows[0].Cells[7].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 331));
            e.Graphics.DrawString("09.Order Amount: " + OrderGv.SelectedRows[0].Cells[8].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(80, 364));
            e.Graphics.DrawString("#Suneth-Auto Entreprises.", new Font("Century", 15, FontStyle.Bold), Brushes.DarkGreen, new Point(200, 400));

           /* e.Graphics.DrawString("Order Summary", new Font("Segoe UI", 40, FontStyle.Bold), Brushes.DarkBlue, new Point(230));
            e.Graphics.DrawString("01. Order Id: " + OrderGv.SelectedRows[0].Cells[0].Value.ToString(), new Font("Calibri", 26, FontStyle.Italic), Brushes.DarkGreen, new Point(80, 100));
            e.Graphics.DrawString("02. Customer Id: " + OrderGv.SelectedRows[0].Cells[1].Value.ToString(), new Font("Verdana", 26, FontStyle.Underline), Brushes.Purple, new Point(80, 133));
            e.Graphics.DrawString("03. Customer Name: " + OrderGv.SelectedRows[0].Cells[2].Value.ToString(), new Font("Tahoma", 26, FontStyle.Bold), Brushes.DarkOrange, new Point(80, 166));
            e.Graphics.DrawString("04. Product Id: " + OrderGv.SelectedRows[0].Cells[3].Value.ToString(), new Font("Arial", 26, FontStyle.Strikeout), Brushes.DeepPink, new Point(80, 199));
            e.Graphics.DrawString("05. Product Name: " + OrderGv.SelectedRows[0].Cells[4].Value.ToString(), new Font("Comic Sans MS", 26, FontStyle.Italic), Brushes.Teal, new Point(80, 232));
            e.Graphics.DrawString("06. Product Price: " + OrderGv.SelectedRows[0].Cells[5].Value.ToString(), new Font("Georgia", 26, FontStyle.Bold), Brushes.IndianRed, new Point(80, 265));
            e.Graphics.DrawString("07. Quantity: " + OrderGv.SelectedRows[0].Cells[6].Value.ToString(), new Font("Rockwell", 26, FontStyle.Regular), Brushes.DarkCyan, new Point(80, 298));
            e.Graphics.DrawString("08. Order Date: " + OrderGv.SelectedRows[0].Cells[7].Value.ToString(), new Font("Book Antiqua", 26, FontStyle.Underline), Brushes.SlateGray, new Point(80, 331));
            e.Graphics.DrawString("09. Order Amount: " + OrderGv.SelectedRows[0].Cells[8].Value.ToString(), new Font("Courier New", 26, FontStyle.Bold), Brushes.DarkRed, new Point(80, 364));
            e.Graphics.DrawString("#Suneth-Auto Entreprises.", new Font("Lucida Console", 20, FontStyle.Italic | FontStyle.Bold), Brushes.DarkGreen, new Point(200, 400));*/



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ManageUsers ms = new ManageUsers();
            this.Hide();
            ms.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ManageCategories ms = new ManageCategories();
            this.Hide();
            ms.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ManageSpareParts ms = new ManageSpareParts();
            this.Hide();
            ms.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ManageCustomers ms = new ManageCustomers();
            this.Hide();
            ms.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ManageOrders ms = new ManageOrders();
            this.Hide();
            ms.Show();
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
    }
}
