using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Invent
{
    public partial class SettingsForm : Form
    {
        private Size formOriginalSize;
        private Rectangle recBut1;
        private Rectangle recRdo1;
        private Rectangle recTxt1;
        private Rectangle recDgrd1;
        public SettingsForm()
        {
            InitializeComponent();
            this.Resize += Activityform_Resize;
            formOriginalSize = this.Size;
            //recBut1 = new Rectangle(button1.Location,button1.Size);
            //recDgrd1 = new Rectangle(dataGridView1.Location,dataGridView1.Size);
            //recRdo1 = new Rectangle(checkBox1.Location, checkBox1.Size);
            //recTxt1 = new Rectangle(textBox1.Location,textBox1.Size);
        }
        //public string conString = "Data Source=MAS-5CD4241CYM\\SQLEXPRESS;Initial Catalog=Actiondb;Integrated Security=True;";
        public string conString = "Data Source=MTX-SRV-APP1;Initial Catalog=Actiondb;Integrated Security=True;Trust Server Certificate=True";
        string randNumber;
        string modelForm, templateForm, activityForm, viewForm, settingsForm, reportsForm, approvalForm, lockoutStatus, userAdmin;
        private void resize_Control(Control c, Rectangle r)
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);

        }

        private void Activityform_Resize(object sender, EventArgs e)
        {
            //resize_Control(button1, recBut1);
            //resize_Control(textBox1, recTxt1);
            //resize_Control(checkBox1, recRdo1);
            //resize_Control(dataGridView1, recDgrd1);

            //  throw new NotImplementedException();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Activityform_Load(object sender, EventArgs e)
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            cmbDepartment.Enabled = false;
            cmbDesignation.Enabled = false;
            txtPasswordConfirmation.Enabled = false;
            txtHodName.Enabled = false;
            txtVerification.Enabled = false;
            btnConfirmVerification.Enabled = false;

        }

        private void permissionUpdate()
        {
            if (chkModel.Checked)
            {
                modelForm = "1";
            }
            else
            {
                modelForm = "0";
            }
            if (chkTemplate.Checked)
            {
                templateForm = "1";
            }
            else
            {
                templateForm = "0";
            }
            if (chkActivities.Checked)
            {
                activityForm = "1";
            }
            else
            {
                activityForm = "0";
            }
            if (chkView.Checked)
            {
                viewForm = "1";
            }
            else
            {
                viewForm = "0";
            }
            if (chkSettings.Checked)
            {
                settingsForm = "1";
            }
            else
            {
                settingsForm = "0";
            }
            if (chkReports.Checked)
            {
                reportsForm = "1";
            }
            else
            {
                reportsForm = "0";
            }
            if (chkApproval.Checked)
            {
                approvalForm = "1";
            }
            else
            {
                approvalForm = "0";
            }
            if (chkAccountLockOut.Checked)
            {
                lockoutStatus = "1";
            }
            else
            {
                lockoutStatus = "0";
            }
            if (chkAdmin.Checked)
            {
                userAdmin = "1";
            }
            else
            {
                userAdmin = "0";
            }

        }

        private async void btnSaveUser_Click(object sender, EventArgs e)
        {
            
            SqlConnection Cons = new SqlConnection(conString);

            if (txtPersonName.Text == "")
            {
                MessageBox.Show("Person Number Cannot Be Blank");
            }
            else if (txtFirstName.Text == "")
            {
                MessageBox.Show("First Name Cannot Be Blank");
            }
            else if (txtLastName.Text == "")
            {
                MessageBox.Show("Last Name Cannot Be Blank");
            }
            else if (txtMobileNumber.Text == "")
            {
                MessageBox.Show("Mobile Number Cannot Be Blank");
            }
            else if (txtUsername.Text == "")
            {
                MessageBox.Show("Username Cannot Be Blank");
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password Cannot Be Blank");
            }
            else if (txtPasswordConfirmation.Text == "")
            {
                MessageBox.Show("Password Confirnation Cannot Be Blank");
            }

            else if (cmbDepartment.Text == "")
            {
                MessageBox.Show("Department Cannot Be Blank");
            }
            else if (cmbDesignation.Text == "")
            {
                MessageBox.Show("Designation Cannot Be Blank");
            }
            else
            {
                permissionUpdate();

                // Define your variables
                string username = txtUsername.Text;
                string personId = txtPersonName.Text;
                DateTime addedDate = DateTime.Now; // Current date and time
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string department = cmbDepartment.Text;
                string hod = txtHodName.Text;
                string mobile = txtMobileNumber.Text;
                string designation = cmbDesignation.Text;


                // Create the SQL INSERT statement with parameters
                string sqlInsert = @"
            INSERT INTO tblUsers (
                Username,
                Password,
                PersonId,
                LockOutStatus,
                ModelTA,
                TemplatesTA,
                ActivitiesTA,
                ViewTA,
                SettingTA,
                ReportsTA,
                First_Name,
                Last_Name,
                Department,
                Hod,
                Mobile,
                Admin,
                Approval,
                AddedDate,
                DesignationTa
            ) VALUES (
                @Username,
                @Password,
                @PersonId,
                @LockOutStatus,
                @ModelTA,
                @TemplatesTA,
                @ActivitiesTA,
                @ViewTA,
                @SettingTA,
                @ReportsTA,
                @First_Name,
                @Last_Name,
                @Department,
                @Hod,
                @Mobile,
                @Admin,
                @Approval,
                @AddedDate,
                @DesignationTa
            )";


                {
                    SqlCommand command = new SqlCommand(sqlInsert, Cons);

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Username", username);
                    if (txtPassword.Text != txtPasswordConfirmation.Text)
                    {
                        MessageBox.Show("The Password and Password Confirmation Is Not Matched");

                    }
                    else if (txtPassword.Text == txtPasswordConfirmation.Text)
                    {
                        command.Parameters.AddWithValue("@Password", txtPasswordConfirmation.Text);

                    }

                    command.Parameters.AddWithValue("@PersonId", personId);
                    command.Parameters.AddWithValue("@LockOutStatus", lockoutStatus);
                    command.Parameters.AddWithValue("@ModelTA", modelForm);
                    command.Parameters.AddWithValue("@TemplatesTA", templateForm);
                    command.Parameters.AddWithValue("@ActivitiesTA", activityForm);
                    command.Parameters.AddWithValue("@ViewTA", viewForm);
                    command.Parameters.AddWithValue("@SettingTA", settingsForm);
                    command.Parameters.AddWithValue("@ReportsTA", reportsForm);
                    command.Parameters.AddWithValue("@First_Name", firstName);
                    command.Parameters.AddWithValue("@Last_Name", lastName);
                    command.Parameters.AddWithValue("@Department", department);
                    command.Parameters.AddWithValue("@Hod", hod);
                    command.Parameters.AddWithValue("@Mobile", mobile);
                    command.Parameters.AddWithValue("@Admin", userAdmin);
                    command.Parameters.AddWithValue("@Approval", approvalForm);
                    command.Parameters.AddWithValue("@AddedDate", addedDate);
                    command.Parameters.AddWithValue("@DesignationTa", designation);

                    try
                    {
                        Cons.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected}  User Saved.");

                        // Replace with actual values or retrieve from input fields
                        string mobileNumber = mobile; // Mobile number to send the SMS
                        string message = " TandA Account is Created. Your Username Is '" + username + "' and Temporary Password Is '" + txtPasswordConfirmation.Text + "'"; // Message to be sent
                        string userNames = username; // Replace with your username if needed
                        string password = txtPasswordConfirmation.Text; // Replace with your password if needed
                        await SendSmsAsync(mobileNumber, message, userNames, password);

                        Cons.Close();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }





            }
               

        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {           

        }

        private void chkAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdmin.Checked)
            {
                chkActivities.Checked = true;
                chkApproval.Checked = true;
                chkModel.Checked = true;
                chkReports.Checked = true;
                chkSettings.Checked = true;
                chkTemplate.Checked = true;
                chkView.Checked = true;

                chkActivities.Enabled = false;
                chkApproval.Enabled = false;
                chkModel.Enabled = false;
                chkReports.Enabled = false;
                chkSettings.Enabled = false;
                chkTemplate.Enabled = false;
                chkView.Enabled = false;
            }
            else
            {
                chkActivities.Checked = false;
                chkApproval.Checked = false;
                chkModel.Checked = false;
                chkReports.Checked = false;
                chkSettings.Checked = false;
                chkTemplate.Checked = false;
                chkView.Checked = false;

                chkActivities.Enabled = true;
                chkApproval.Enabled = true;
                chkModel.Enabled = true;
                chkReports.Enabled = true;
                chkSettings.Enabled = true;
                chkTemplate.Enabled = true;
                chkView.Enabled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {

            foreach (Control control in this.Controls) // Enable all text boxes 
                if (control is TextBox)
                {
                    control.Enabled = true;
                }
            cmbDepartment.Enabled = true;
            cmbDesignation.Enabled = true;
            btnConfirmVerification.Enabled = true;
            btnVerify.Enabled = true;
            
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (TextBox)ctrl;
                    tb.Text = string.Empty;
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox cb = (ComboBox)ctrl;
                    cb.Text = string.Empty;
                    cb.SelectedIndex = -1;
                }
            }


        }

        private void txtVerification_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPersonName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }





        private async Task SendSmsAsync(string mobileNumber, string message, string username, string password)
        {
            // Replace with your actual key
            string esmsqk = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MTQ2MDksImN1c3RvbWVyX3JvbGUiOjAsImlhdCI6MTc0MDAyOTUxOSwiZXhwIjo0ODY0MjMxOTE5fQ.D6FMPID6GBuy_P8noSlIAFUexjkTbufFGn_nN4TdfIo";

            // Define the URLs using the provided parameters
            string url1 = $"https://e-sms.dialog.lk/api/v1/message-via-url/create/url-campaign?esmsqk={esmsqk}&list={mobileNumber}&source_address=MATRIX%20INFO&message={message} Please Change the Temporary Password at first once login";
            // string url = $"https://e-sms.dialog.lk/api/v1/message-via-url/create/url-campaign?esmsqk={esmsqk}&list={mobileNumber}&message={message}";

            try
            {
                using (var client = new HttpClient())
                {
                    //var response = await client.GetAsync(url);
                    var responsee = await client.GetAsync(url1);
                    // response.EnsureSuccessStatusCode();
                    responsee.EnsureSuccessStatusCode();
                    MessageBox.Show("SMS sent successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(1001, 77777);
            randNumber = randomNumber.ToString();

            // Replace with actual values or retrieve from input fields
            string mobileNumber = txtMobileNumber.Text; // Mobile number to send the SMS
            string message = "Hello!  Your Mobile is '" + mobileNumber + "'and your mobile verification code is '" + randNumber + "'"; // Message to be sent
            //string userNames = " "; // Replace with your username if needed
            //string password = " "; // Replace with your password if needed

            // await SendSmsAsync(mobileNumber, message, userNames, password);



            string esmsqk = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MTQ2MDksImN1c3RvbWVyX3JvbGUiOjAsImlhdCI6MTc0MDAyOTUxOSwiZXhwIjo0ODY0MjMxOTE5fQ.D6FMPID6GBuy_P8noSlIAFUexjkTbufFGn_nN4TdfIo";

            // Define the URLs using the provided parameters
            string url1 = $"https://e-sms.dialog.lk/api/v1/message-via-url/create/url-campaign?esmsqk={esmsqk}&list={mobileNumber}&source_address=MATRIX%20INFO&message={message} Please submit it for the verify";
            // string url = $"https://e-sms.dialog.lk/api/v1/message-via-url/create/url-campaign?esmsqk={esmsqk}&list={mobileNumber}&message={message}";

            if(txtMobileNumber.Text == "")
            {
                MessageBox.Show("Please enter valid mobile number for mobile number verification");
            }
            else
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        //var response = await client.GetAsync(url);
                        var responsee = await client.GetAsync(url1);
                        // response.EnsureSuccessStatusCode();
                        responsee.EnsureSuccessStatusCode();
                        MessageBox.Show("Verification SMS sent successfully.");
                        txtVerification.Enabled = true;
                        btnConfirmVerification.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

                

        }

        private void btnConfirmVerification_Click(object sender, EventArgs e)
        {
            if (txtVerification.Text == randNumber)
            {
                MessageBox.Show("Code is matched, please proceed with the registration");
                //txtVerification.Enabled = true;
                cmbDepartment.Enabled = true;
                cmbDesignation.Enabled = true;
                txtHodName.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                txtPasswordConfirmation.Enabled = true;
                txtHodName.Enabled = true;
                //btnEditUser.Enabled = true;
                btnSaveUser.Enabled = true;
                btnUpdateUser.Enabled = true;
                chkActivities.Enabled = true;
                chkAdmin.Enabled = true;
                chkApproval.Enabled = true;
                chkModel.Enabled = true;
                chkReports.Enabled = true;
                chkSettings.Enabled = true;
                chkTemplate.Enabled = true;
                chkView.Enabled = true;
            }
            else
            {
                MessageBox.Show("Invalid code. Please check the code and try again");
                //txtVerification.Enabled = true;
                cmbDepartment.Enabled = false;
                cmbDesignation.Enabled = false;
                txtPassword.Enabled = false;
                txtPasswordConfirmation.Enabled = false;
                txtHodName.Enabled = false;
                //btnEditUser.Enabled = true;
                btnSaveUser.Enabled = false;
                btnUpdateUser.Enabled = false;
                chkActivities.Enabled = false;
                chkAdmin.Enabled = false;
                chkApproval.Enabled = false;
                chkModel.Enabled = false;
                chkReports.Enabled = false;
                chkSettings.Enabled = false;
                chkTemplate.Enabled = false;
                chkView.Enabled = false;
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            SqlConnection Cons = new SqlConnection(conString);

            Cons.Open();
            permissionUpdate();
            try
            {
                SqlCommand cmdUpdate = new SqlCommand("update tblUsers set LockOutStatus = '" + lockoutStatus + "',ModelTA = '" + modelForm + "',TemplatesTA = '" + templateForm + "',ActivitiesTA = '" + activityForm + "',ViewTA='" + viewForm + "',SettingTA = '" + settingsForm + "',ReportsTA='" + reportsForm + "',Mobile='" + txtVerification.Text + "',Admin='" + activityForm + "',Approval='" + approvalForm + "' where (PersonId = '"+txtSearchPerson.Text+"')", Cons);
                SqlDataReader mealdr = cmdUpdate.ExecuteReader();
                while (mealdr.Read())
                {

                }

                MessageBox.Show(" Settings are updated .. ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cons.Close();
            }                  

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection Cons = new SqlConnection(conString);
            
            btnSaveUser.Enabled = false;
            btnVerify.Enabled = false;
            btnConfirmVerification.Enabled = false;
            string personId = txtSearchPerson.Text;

            foreach (Control control in this.Controls) // Disable all text boxes 
                if (control is TextBox)
                {
                    control.Enabled = false;
                }
            cmbDepartment.Enabled = false;
            cmbDesignation.Enabled = false;

            try
            {
                string lockOut, ModelTa, TemplateTa, activitiesTa, viewTa, settingTa, reportsTa, accountTa, adminTa;
                Cons.Open();
                SqlCommand cmd = new SqlCommand(" select * from tblusers where personid = '" + personId + "'", Cons);
                SqlDataReader loadDataDR = cmd.ExecuteReader();
                while (loadDataDR.Read())
                {                    
                    lockOut = loadDataDR["LockOutStatus"].ToString();
                    if(lockOut == "1")
                    {
                        chkAccountLockOut.Checked = true;
                    }
                    else
                    {
                        chkAccountLockOut.Checked = false;
                    }

                    ModelTa = loadDataDR["ModelTA"].ToString();
                    if(ModelTa == "1")
                    {
                        chkModel.Checked = true;
                    }
                    else
                    {
                        chkModel.Checked = false;
                    }
                    TemplateTa = loadDataDR["TemplatesTA"].ToString();
                    if(TemplateTa == "1")
                    {
                        chkTemplate.Checked = true;
                    }
                    else
                    {
                        chkTemplate.Checked = false;
                    }
                    activitiesTa = loadDataDR["ActivitiesTA"].ToString();
                    if(activitiesTa == "1")
                    {
                        chkActivities.Checked = true;
                    }
                    else
                    {
                        chkActivities.Checked = false;
                    }
                    viewTa = loadDataDR["ViewTA"].ToString();
                    if(viewTa == "1")
                    {
                        chkView.Checked = true;
                    }
                    else
                    {
                        chkView.Checked = false;
                    }
                    settingTa = loadDataDR["SettingTA"].ToString();
                    if(settingTa == "1")
                    {
                        chkSettings.Checked = true;
                    }
                    else
                    {
                        chkSettings.Checked = false;
                    }
                    reportsTa = loadDataDR["ReportsTA"].ToString();
                    if(reportsTa == "1")
                    {
                        chkReports.Checked = true;
                    }
                    else
                    {
                        chkReports.Checked = false;
                    }
                    accountTa = loadDataDR["Approval"].ToString();
                    if(accountTa == "1")
                    {
                        chkApproval.Checked = true;
                    }
                    else
                    {
                        chkApproval.Checked = false;
                    }
                    adminTa = loadDataDR["Admin"].ToString();
                    if(adminTa=="1")
                    {
                        chkAdmin.Checked = true;
                    }
                    else
                    {
                        chkAdmin.Checked = false;
                    }

                    txtFirstName.Text = loadDataDR["First_Name"].ToString();
                    txtLastName.Text = loadDataDR["Last_Name"].ToString();
                    //txtMobileNumber.Text = loadDataDR["Mobile"].ToString();
                    //txtUsername.Text = loadDataDR["username"].ToString();
                    txtPersonName.Text = loadDataDR["PersonId"].ToString();

                }
                loadDataDR.Close();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                       


        }

        private void txtSearchPerson_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

