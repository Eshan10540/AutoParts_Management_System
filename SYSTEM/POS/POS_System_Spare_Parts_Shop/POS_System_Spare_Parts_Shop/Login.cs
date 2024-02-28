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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\SparePartsShopDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtPassword.Clear();
        }

        private void label6_ForeColorChanged(object sender, EventArgs e)
        {
            //--------
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
           // ForeColor = Color.Green;
            //label6.ForeColor = Color.Green;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
           // ForeColor = Color.Red;
            //label6.ForeColor = Color.Red;
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked == false)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
                               
        }

        private void btnLOGIN_Click(object sender, EventArgs e)
        {
            Con.Open();
            
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from UserTable where UserName = '" + txtUserName.Text + "' and Password = '" + txtPassword.Text + "'", Con);
            DataTable dt = new DataTable();   // Create a data table to hold the result
            sda.Fill(dt);// Fill the data table with the query result

            
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("Login Succeed", "Login Message", MessageBoxButtons.OK); 
                ManageOrders home = new ManageOrders();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorret UserName or Password Try Again");
            }
            Con.Close();
        }
    }
}
