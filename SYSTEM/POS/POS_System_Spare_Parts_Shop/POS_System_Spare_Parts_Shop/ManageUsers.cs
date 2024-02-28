using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace POS_System_Spare_Parts_Shop
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
           InitializeComponent();
           // UsersGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
           // UsersGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

           // UsersGV.SelectionChanged += UsersGV_SelectionChanged;
            UsersGV.RowTemplate.Height = 50;
            UsersGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            UsersGV.ColumnHeadersHeight = 70;
       
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\SparePartsShopDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            UserFill();
        }
        public void UserFill()
        {
            try
            {
                Con.Open();
                String MyQuery = "SELECT * FROM UserTable";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                //ds.Tables[0]
                da.Fill(ds);
                UsersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
           try {
                if (UnameTb.Text == "")
                {
                    MessageBox.Show("Please Enter the User Name");
                }
                else if (FnameTb.Text == "")
                {
                    MessageBox.Show("Please Enter the Full Name");
                }
                else if (PasswordTb.Text == "")
                {
                    MessageBox.Show("Please Enter Password");
                }
                else if (PhoneTb.Text == "")
                {
                    MessageBox.Show("Please Enter the Phone Number");
                }else if(PhoneTb.Text.Length != 10)
                {
                    MessageBox.Show("Phone Number Must have a 10 Numbers");
                }
                else
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO UserTable VALUES('" + UnameTb.Text + "','" + FnameTb.Text + "','" + PasswordTb.Text + "','" + PhoneTb.Text + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Details Added Sucessfully");
                    Con.Close();
                    UserFill();
                }

            }
            catch (Exception ex){ 
            
              MessageBox.Show(ex.Message);  
            
            }
              
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {          
                Con.Open();           
                SqlCommand cmd = new SqlCommand("UPDATE UserTable SET UserName='" + UnameTb.Text + "',FullName='" + FnameTb.Text + "',Password='" + PasswordTb.Text + "' where TelephoneNo='" + PhoneTb.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Details Updated Successfully");
                Con.Close();
                UserFill();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (PhoneTb.Text == "") 
            {
                MessageBox.Show("Enter The Users Phone Number"); 
            }
            else
            {
                Con.Open();
                String myquery = "DELETE FROM UserTable WHERE TelephoneNo = '" + PhoneTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Details Deleted Successfully");
                Con.Close();
                UserFill();
            }
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
            FnameTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            PasswordTb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = UsersGV.SelectedRows[0].Cells[3].Value.ToString();

        }
        private void UnameTb_MouseClick(object sender, MouseEventArgs e)
        {
            UnameTb.Text = "";
        }

        private void PasswordTb_MouseClick(object sender, MouseEventArgs e)
        {
            PasswordTb.Text = "";
        }

        private void FnameTb_MouseClick(object sender, MouseEventArgs e)
        {
            FnameTb.Text = "";
        }

        private void PhoneTb_MouseClick(object sender, MouseEventArgs e)
        {
            PhoneTb.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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

        private void UsersGV_SelectionChanged(object sender, EventArgs e)
        {

            //-----------------------------------------
            // Check if any cell in the header row is selected
            //foreach (DataGridViewCell cell in UsersGV.SelectedCells)
            //{
              //  if (cell.RowIndex == -1)
              //  {
                    // Deselect the header row
               //     UsersGV.Rows[cell.RowIndex].Selected = false;
               //     break;
               // }

          // }
       }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Use \D to match any non-digit character
            string nonDigitPattern = @"\D";
            string onlyDigits = Regex.Replace(phoneNumber, nonDigitPattern, "");

            // Check if the resulting string has exactly 10 digits
            return onlyDigits.Length == 10;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void UnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void PhoneTb_TextChanged(object sender, EventArgs e)
        {
            //PhoneTb.Text
        }

        private void PhoneTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits (0-9) and the backspace key
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
