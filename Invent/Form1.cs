using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace Invent
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
       // public string conString = "Data Source=MAS-5CD4241CYM\\SQLEXPRESS;Initial Catalog=Actiondb;Integrated Security=True;";
        public string conString = "Data Source=MTX-SRV-APP1;Initial Catalog=Actiondb;Integrated Security=True;Trust Server Certificate=True";
        public static string PublicUsername;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtusername.Text;
                string password = txtpassword.Text;

                if (ValidateUser(username, password))
                {
                    PublicUsername = txtusername.Text;
                    Portal newportal = new Portal(PublicUsername);                    
                    newportal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                    txtusername.Text = "";
                    txtpassword.Text = "";
                    txtusername.Focus();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }         
             
        }
      
        private bool ValidateUser(string username, string password)
        {

            // DB connectivity and Update Weight data in to the DB
            SqlConnection Cons = new SqlConnection(conString);            

            using (SqlConnection connection = new SqlConnection(conString))
            {
                string query = "SELECT COUNT(1) FROM tblUsers WHERE Username=@Username AND Password=@Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count == 1; // Returns true if user exists
                
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

           

        }
    }
}
