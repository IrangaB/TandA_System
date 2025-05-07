using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace Invent
{
    public partial class NewModels : Form
    {
        public NewModels()
        {
            InitializeComponent();

        }
        //public string conString = "Data Source=MAS-5CD4241CYM\\SQLEXPRESS;Initial Catalog=Actiondb;Integrated Security=True;";
        public string conString = "Data Source=MTX-SRV-APP1;Initial Catalog=Actiondb;Integrated Security=True;Trust Server Certificate=True";
        string user = Login.PublicUsername;
        string randNumber, approvalName, approvalMobile;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "Select Image (*.jpg;*.png*.gif)|*.jpg;*.png*.gif";
            openFileDialog1.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                Image img = pictureBox1.Image;

                if (txtStyleNo.Text == "")
                {
                    MessageBox.Show("Style number cannot be blank");
                }
                else if (txtDescription.Text == "")
                {
                    MessageBox.Show("Description cannot be blank");
                }
                else if (txtSeason.Text == "")
                {
                    MessageBox.Show("Season cannot be blank");
                }                
                else if (txtFob.Text == "")
                {
                    MessageBox.Show("FOB cannot be blank");
                }
                else if (txtOrderqty.Text == "")
                {
                    MessageBox.Show("Order Qty cannot be blank");
                }
                else if (txtRmc.Text == "")
                {
                    MessageBox.Show("RMC cannot be blank");
                }
                else if (txtknit.Text == "")
                {
                    MessageBox.Show("Knit cannot be blank");
                }
                else if (txtpk.Text == "")
                {
                    MessageBox.Show("SMV cannot be blank");
                }
                else if (txtCustomerPo.Text == "")
                {
                    MessageBox.Show("Customer PO cannot br blank");
                }
                else if (txtWeight.Text == "")
                {
                    MessageBox.Show("Weight cannot be blank");
                }
                

                else if (img != null)
                {
                    SqlConnection Cons = new SqlConnection(conString);
                    Cons.Open();
                    DateTime selectdate = dtDate.Value;
                    MessageBox.Show(selectdate.ToString());
                    int day = selectdate.Day;
                    int month = selectdate.Month;
                    int year = selectdate.Year;

                    //MessageBox.Show(selectdate.ToString());
                    SqlCommand cmd = new SqlCommand("insert into tblmodel (style_no,description,season,fob,delivery_date,rmcost,knitmin,pksmv,customer_po,order_qty,weight,custodian) values ('" + txtStyleNo.Text + "','" + txtDescription.Text + "','" + txtSeason.Text + "','" + txtFob.Text + "','" + selectdate + "','" + txtRmc.Text + "','" + txtknit.Text + "','" + txtpk.Text + "','" + txtCustomerPo.Text + "','" + txtOrderqty.Text + "','" + txtWeight.Text + "','" + user + "')", Cons);
                    SqlDataReader InsertModelDR = cmd.ExecuteReader();
                    while (InsertModelDR.Read())
                    {

                    }
                    InsertModelDR.Close();
                    saveImage();
                    MessageBox.Show("New Model added to the system successfully");
                    dtDate.Enabled = false;
                }
                else
                {
                    MessageBox.Show(" No Image in PictureBox");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveImage()
        {

            try
            {
                // Assuming pictureBox1 is your PictureBox control
                if (pictureBox1.Image != null)
                {
                    string styleNumber = txtStyleNo.Text;
                    string imageName = "Image_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png"; // Generate a unique name
                    byte[] imageData;

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Save the image from PictureBox to MemoryStream
                        pictureBox1.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        imageData = memoryStream.ToArray();
                    }

                    using (SqlConnection Cons = new SqlConnection(conString))
                    {
                        // Insert image data into the database
                        using (SqlCommand command = new SqlCommand("INSERT INTO tblimages (StyleNumber, ImageName, ImageData) VALUES (@StyleNumber, @ImageName, @ImageData)", Cons))
                        {
                            command.Parameters.Add("@StyleNumber", SqlDbType.VarChar, 255).Value = styleNumber;
                            command.Parameters.Add("@ImageName", SqlDbType.VarChar, 255).Value = imageName;
                            command.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1).Value = imageData;

                            Cons.Open(); // Open the connection
                            command.ExecuteNonQuery();
                            MessageBox.Show("Image uploaded successfully!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No image found in PictureBox.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error uploading image: " + ex.ToString());
            }

        }

        private void Retriveimage()
        {
            SqlConnection Cons = new SqlConnection(conString);

            try
            {
                string styleNumber = txtStyleNo.Text;

                // Retrieve image data from the database
                using (SqlCommand command = new SqlCommand("SELECT imageData FROM tblimages WHERE StyleNumber = @StyleNumber", Cons))
                {
                    command.Parameters.Add("@StyleNumber", SqlDbType.VarChar, 255).Value = styleNumber;

                    Cons.Open(); // Open the connection
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] imageData = (byte[])reader["ImageData"];

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image image = Image.FromStream(ms);
                                pictureBox1.Image = image; // Display the retrieved image
                            }
                        }
                        else
                        {
                            MessageBox.Show("Image not found for Style Number: " + styleNumber);
                            pictureBox1.Image = null; // Clear the PictureBox if no image found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving image: " + ex.ToString());
            }
            finally
            {
                Cons.Close(); // Ensure the connection is closed
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
        }

        private void NewModels_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'actiondbDataSet6.tblEmpmaster' table. You can move, or remove it, as needed.
            //this.tblEmpmasterTableAdapter.Fill(this.actiondbDataSet6.tblEmpmaster);
                                  
            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from tblEmpmaster", Cons);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    cmdApproval.Items.Add(reader["firstname"].ToString());
                 
                    approvalName = cmdApproval.Text;
                    
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();
            
            SqlCommand cmd = new SqlCommand("select * from tblempmaster where firstname = '" + cmdApproval.Text + "'", Cons);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                approvalMobile = reader["mobile"].ToString();               

            }

            Random random = new Random();
            int randomNumber = random.Next(1001, 77777);
            randNumber = randomNumber.ToString();

            // Replace with actual values or retrieve from input fields
            //string mobileNumber = txtAppPw.Text; // Mobile number to send the SMS
            
            string message = " Hello! You have delivery date amend request by '" + Login.PublicUsername + "' and approval verification code is '" + randNumber + "'"; // Message to be sent
           
            string esmsqk = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MTQ2MDksImN1c3RvbWVyX3JvbGUiOjAsImlhdCI6MTc0MDAyOTUxOSwiZXhwIjo0ODY0MjMxOTE5fQ.D6FMPID6GBuy_P8noSlIAFUexjkTbufFGn_nN4TdfIo";

            // Define the URLs using the provided parameters
            string url1 = $"https://e-sms.dialog.lk/api/v1/message-via-url/create/url-campaign?esmsqk={esmsqk}&list={approvalMobile}&source_address=MATRIX%20INFO&message={message} Please submit it for the verify";
            // string url = $"https://e-sms.dialog.lk/api/v1/message-via-url/create/url-campaign?esmsqk={esmsqk}&list={mobileNumber}&message={message}";

            try
            {
                using (var client = new HttpClient())
                {
                    //var response = await client.GetAsync(url);
                    var responsee = await client.GetAsync(url1);
                    // response.EnsureSuccessStatusCode();
                    responsee.EnsureSuccessStatusCode();
                    MessageBox.Show("Approval SMS sent successfully. Please enter approval code and click Unblock to change the Delivery Date");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {


            using (WebClient client = new WebClient())
            {
                try
                {
                    // Replace with your actual API credentials and endpoint
                    string username = "MASMatrix"; // Your SMS gateway username
                    string password = "Abcd@123"; // Your SMS gateway password
                    string url = $"https://e-sms.dialog.lk/api/v2/sms={username}&dst={0774847694}&msg={WebUtility.UrlEncode("This is test message")}&username={username}&password={password}";

                    // Call web API to send SMS
                    string result = client.DownloadString(url);

                    if (result.Contains("OK"))
                    {
                        MessageBox.Show("Your message has been successfully sent.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Message send failure.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(randNumber==txtAppPw.Text)
            {
                dtDate.Enabled = true;
            }
            else
            {
                dtDate.Enabled = false;
                MessageBox.Show("Approval code is Invalid.. Please check the code and try again");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
