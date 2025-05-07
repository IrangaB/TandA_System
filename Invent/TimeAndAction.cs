//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.IO;

namespace Invent
{
    public partial class TimeAndAction : Form
    {

        private DateTimePicker dateTimePicker1;
        public TimeAndAction()
        {
            InitializeComponent();
        }
        //public string conString = "Data Source=MAS-5CD4241CYM\\SQLEXPRESS;Initial Catalog=Actiondb;Integrated Security=True;";
        public string conString = "Data Source=MTX-SRV-APP1;Initial Catalog=Actiondb;Integrated Security=True;Trust Server Certificate=True";
        public static string popUpString;
        string styleName, deliveryDate, activityId, activityOrder, activityName, resposibleName, range, custom_Name, calDate, activityTa, responsibilityTa, dataRangeTa, dueDateTa, planTa, deviationTa,forcastedTa, actualTa, actualDateTa, remarksTa, styleNumberTa, modelNoTa, totalDaysTa, deliverydateTa, startDateTa, forcastedDateTa, usernameTa, editedTa, checkStyleAvailability;
        int calRevisionDate;
        string srchStyleNo, srchModelNo, srchTotalToDelivery, srchActualDelivery, srchstartDate, srchActivity, srchResponsibility, srchRange, srchDuedate, srchPlandate, srchDeviation, srchActual, srchRemarks, srchUsername, srchForcastDelay, srchEditedDate;
        string personId, roleId, modelTa, templateTa, activitiesTa, viewTa, settingsTa, reportsTa, firstNameTa, lastNameTa, deaprtmentTa, hodTa, mobileTa, adminTa, approvalTa, addedDateTa, departmentTa;
        private void button1_Click(object sender, EventArgs e)
        {
            calculateMinusAndAddingDates();
            calDeviation();

            //CalculateAndDisplayCalculatedDates();
           UpdateDeviation();
        }     

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            loadRevisionId();
            calDeviation();
            reviseData();
            ReplaceEntireTable();
        }

        private void ReplaceEntireTable()
        {
            // This method clears the entire table and inserts the new data
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection Cons = new SqlConnection(conString);
                Cons.Open();
                cmd.Connection = Cons;
                cmd.CommandType = CommandType.Text;

                // Delete all existing records
                cmd.CommandText = "DELETE FROM tblTimeAndAction where Style_No = '" + cmdstyle.Text + "' and Model_No = '" + cmbActivity.Text + "'";
                cmd.ExecuteNonQuery();

                // Insert new records
                saveTandAData();
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgActionView.Rows.Clear();
            checkStyle();
        }

        private void CalculateAndDisplayCalculatedDates()
        {
            

            for (int i = 0; i < dgActionView.Rows.Count; i++)
            {
                // Ensure that the current row is not the "Add New Row" and that relevant cells are not null
                if (!dgActionView.Rows[i].IsNewRow &&
                    dgActionView.Rows[i].Cells[3].Value != null &&
                    dgActionView.Rows[i].Cells[4].Value != null &&
                    dgActionView.Rows[i].Cells[5].Value != null)
                {
                    DateTime planDate;
                    int range;

                    // Parse the Plan date
                    if (!DateTime.TryParse(dgActionView.Rows[i].Cells[4].Value.ToString(), out planDate))
                    {
                        MessageBox.Show($"Invalid date format in row {i + 1} for Plan date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue; // Skip this row if parsing fails
                    }

                    // Parse the range (days difference)
                    if (!int.TryParse(dgActionView.Rows[i].Cells[5].Value.ToString(), out range))
                    {
                        MessageBox.Show($"Invalid range format in row {i + 1} for Range.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue; // Skip this row if parsing fails
                    }

                    // Calculate the new date by adding the range to the Plan date
                    DateTime calculatedDate = planDate.AddDays(range);

                    // Display the calculated date in Column 6
                    dgActionView.Rows[i].Cells[6].Value = calculatedDate.ToString("yyyy-MM-dd"); // Format as needed

                    // Optional: You can also format the cell or perform additional checks here
                }
            }
        }



        private void checkStyle()
        {
            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();
            string custerStyle = txtSearchStyle.Text;
            try
            {
                SqlCommand cmd = new SqlCommand(" select * from tblTimeAndAction where Style_No = '" + custerStyle + "'", Cons);
                SqlDataReader loadDataDR = cmd.ExecuteReader();
                while (loadDataDR.Read())
                {
                    checkStyleAvailability = loadDataDR["Style_No"].ToString();
                }
                loadDataDR.Close();
                dgActionView.Rows.Clear();
                if (checkStyleAvailability.IsNullOrEmpty())
                {
                    MessageBox.Show("No Style Number Available under this name..");
                    txtSearchStyle.Text = "";
                    txtSearchStyle.Focus();
                }
                else
                {
                    loadTimeActivity();
                    btnSubmit.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnEdit.Enabled = true;
                    //btnSubmit.Enabled = false;
                    foreach (DataGridViewColumn column in dgActionView.Columns)
                    {
                        //column.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadTimeActivity()
        {
            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();
            dgActionView.Rows.Clear();
            btnSubmit.Enabled = false;
            string custerStyle = txtSearchStyle.Text;

            try
            {
                SqlCommand cmd = new SqlCommand(" select * from tblTimeAndAction where Style_No = '" + custerStyle + "'", Cons);
                SqlDataReader loadDataDR = cmd.ExecuteReader();
                while (loadDataDR.Read())
                {
                    srchStyleNo = loadDataDR["Style_No"].ToString();
                    srchModelNo = loadDataDR["Model_No"].ToString();
                    srchTotalToDelivery = loadDataDR["Total_ToDelivery"].ToString();
                    srchActualDelivery = loadDataDR["Actual_Delivery"].ToString();
                    srchstartDate = loadDataDR["Start_Date"].ToString();
                    srchActivity = loadDataDR["Activity"].ToString();
                    srchResponsibility = loadDataDR["Resposibility"].ToString();
                    srchRange = loadDataDR["Range"].ToString();
                    srchDuedate = loadDataDR["Duedate"].ToString();
                    srchPlandate = loadDataDR["PlanDate"].ToString();
                    srchDeviation = loadDataDR["Deviation"].ToString();
                    srchActual = loadDataDR["Actual"].ToString();
                    srchRemarks = loadDataDR["Remarks"].ToString();
                    srchUsername = loadDataDR["Username"].ToString();
                    srchForcastDelay = loadDataDR["ForcastedDelay"].ToString();
                    srchEditedDate = loadDataDR["EditedDate"].ToString();


                    int i = dgActionView.Rows.Add();
                    dgActionView.Rows[i].Cells[0].Value = srchActivity;
                    dgActionView.Rows[i].Cells[1].Value = srchResponsibility;
                    dgActionView.Rows[i].Cells[2].Value = srchRange;
                    dgActionView.Rows[i].Cells[3].Value = srchDuedate;
                    dgActionView.Rows[i].Cells[4].Value = srchPlandate;
                    dgActionView.Rows[i].Cells[5].Value = srchDeviation;
                    dgActionView.Rows[i].Cells[7].Value = srchActual;
                    dgActionView.Rows[i].Cells[8].Value = srchRemarks;

                    lblDays.Text = srchTotalToDelivery;
                    lblStylenumber.Text = srchStyleNo;
                    lblDeliveryDate.Text = srchActualDelivery;
                    lblStartDate.Text = srchstartDate;

                }
                loadDataDR.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            btnUpdate.Enabled = true;
            btnSubmit.Enabled = false;

            loadPermissions();
            ApplyRowRestrictions();

            btnEdit.Enabled = false;
        }

        private void ApplyRowRestrictions()
        {
            foreach (DataGridViewRow row in dgActionView.Rows)
            {
                // Ensure row is not the header row or a new row
                if (row.IsNewRow) continue; // Skip new rows


                // Get the cell value from the specified column
                if (row.Cells[1].Value == null)
                {
                    //Handle a null value
                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    continue;
                }
                string cellValue = row.Cells[1].Value.ToString();


                if (string.IsNullOrEmpty(cellValue))
                {
                    row.ReadOnly = true; // Disable editing if empty
                    row.DefaultCellStyle.BackColor = Color.LightSalmon; // Highlight as disabled
                    continue; // Skip to the next row
                }
                else
                {
                    cellValue = cellValue.Trim(); // Remove leading/trailing spaces


                    // Check if the cell value matches the specified string value
                    if (cellValue == departmentTa)
                    {
                        // If the department matches, enable editing and set to default background
                        row.ReadOnly = false;
                        row.DefaultCellStyle.BackColor = Color.White; // Or your default color
                    }
                    else
                    {
                        // If the department does not match, disable editing and highlight
                        row.ReadOnly = true;
                        row.DefaultCellStyle.BackColor = Color.AliceBlue; // Or your disabled color
                    }
                }
            }
        }




        int intRange;
        private void TimeAndAction_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnEdit.Enabled = false;
            // TODO: This line of code loads data into the 'actiondbDataSet3.Custom_TandA' table. You can move, or remove it, as needed.
            this.custom_TandATableAdapter.Fill(this.actiondbDataSet3.Custom_TandA);
            // TODO: This line of code loads data into the 'actiondbDataSet1.tblModel' table. You can move, or remove it, as needed.
            this.tblModelTableAdapter.Fill(this.actiondbDataSet1.tblModel);
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.custom_TandATableAdapter.FillBy(this.actiondbDataSet3.Custom_TandA);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }


        private void fillByNameToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.custom_TandATableAdapter1.FillByName(this.actiondbDataSet2.Custom_TandA);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void fillByNameToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.custom_TandATableAdapter.FillByName(this.actiondbDataSet3.Custom_TandA);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void dgActionView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }
              


        private void calDeviation()
        {
            double total = 0;

            // Loop through the rows
            for (int i = 0; i < dgActionView.Rows.Count; i++)
            {
                // Ensure that the row is not the "Add New Row" and that the cell is not null
                if (!dgActionView.Rows[i].IsNewRow && dgActionView.Rows[i].Cells[5].Value != null)
                {
                    string cellValue = dgActionView.Rows[i].Cells[5].Value.ToString().Trim(); // Get the cell value as a string and trim any whitespace

                    double cellValueAsDouble;

                    // Attempt to convert the cell value to a double using TryParse
                    if (double.TryParse(cellValue, out cellValueAsDouble))
                    {
                        // Conversion was successful
                        total += cellValueAsDouble;

                    }
                    else
                    {
                        // Conversion failed. Handle the error:
                        // Option 1: Skip the row and continue with the calculation
                        //Console.WriteLine($"Warning: Could not parse '{cellValue}' as a double in row {i}. Skipping this row.");
                        //Option 2:  Set the total to zero (handle this carefully - is it right?)
                        //total = 0;
                        //Option 3:  Show a message to the user
                        //MessageBox.Show($"Invalid data in row {i}, column 5.  Please correct it.", "Data Error");
                        //break; // Stop calculation completely (you might want to do this in some cases)
                    }
                }
            }

            // Display the result in the label, formatted to two decimal places
            lblProjectedDelivery.Text = "Delivery Date Deviated By: " + total.ToString("F2") + " Days";
            if(total>0)
            {
                popUpString = total.ToString();
                PopUpWindow newPopUp = new PopUpWindow(popUpString);
                newPopUp.ShowDialog();

            }
            else
            {
                popUpString = total.ToString();
                PopUpWindowMinus newPopUpMinus = new PopUpWindowMinus(popUpString);
                newPopUpMinus.ShowDialog();
            }
        }



        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
            btnSubmit.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            //loadImage();
            loadActivity();
            btnCalculate_Click();
            UpdatePlanDates();
            copyCellData();
            Retriveimage();
            // loadPermissions();


        }

       
           

        //private void restrictColumns()
        //{
        //   for(int i = 0;i < dgActionView.Rows.Count;i++)
        //    {
        //        string responsibleName = dgActionView.Rows[i].Cells[1].Value.ToString();
        //        MessageBox.Show(resposibleName);
        //        bool isEditable = (departmentTa == responsibleName);

        //        dgActionView.Rows[i].Cells[4].ReadOnly = isEditable;
        //        dgActionView.Rows[i].Cells[7].ReadOnly = isEditable;
        //        dgActionView.Rows[i].Cells[8].ReadOnly = isEditable;
        //    }
        //}

        private void Retriveimage()
        {
            SqlConnection Cons = new SqlConnection(conString);

            try
            {
                string styleNumber = cmdstyle.Text;

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
        private void loadPermissions()
        {
            try
            {
                SqlConnection Cons = new SqlConnection(conString);
                Cons.Open();
                SqlCommand cmd = new SqlCommand(" select * from tblUsers where Username = '" + Login.PublicUsername + "'", Cons);
                SqlDataReader loadDataDR = cmd.ExecuteReader();
                while (loadDataDR.Read())
                {
                    personId = loadDataDR["PersonId"].ToString();
                    roleId = loadDataDR["Roleid"].ToString();
                    modelTa = loadDataDR["ModelTA"].ToString();
                    templateTa = loadDataDR["TemplatesTA"].ToString();
                    activitiesTa = loadDataDR["ActivitiesTA"].ToString();
                    roleId = loadDataDR["ViewTA"].ToString();
                    settingsTa = loadDataDR["SettingTA"].ToString();
                    reportsTa = loadDataDR["ReportsTA"].ToString();
                    firstNameTa = loadDataDR["First_Name"].ToString();
                    lastNameTa = loadDataDR["Last_Name"].ToString();
                    deaprtmentTa = loadDataDR["Department"].ToString();
                    hodTa = loadDataDR["Hod"].ToString();
                    mobileTa = loadDataDR["Mobile"].ToString();
                    adminTa = loadDataDR["Admin"].ToString();
                    approvalTa = loadDataDR["Approval"].ToString();
                    addedDateTa = loadDataDR["AddedDate"].ToString();
                    departmentTa = loadDataDR["DesignationTa"].ToString();

                }
                loadDataDR.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection Cons = new SqlConnection(conString);
            string styleName = cmdstyle.Text;
            string modelName = cmbActivity.Text;
            if (string.IsNullOrEmpty(styleName) || string.IsNullOrEmpty(modelName))
            {
                MessageBox.Show("Please select values from both combo boxes.");
                return; // Stop further execution
            }
            else
            {
                Cons.Open();
                string query = "SELECT COUNT (*) FROM tblTimeAndAction WHERE Style_No = @value1 AND Model_No = @value2";

                // 3. Create a SqlCommand object
                using (SqlCommand sqlCommand = new SqlCommand(query, Cons))
                {
                    // 4. Add parameters to the query to prevent SQL injection
                    sqlCommand.Parameters.AddWithValue("@value1", styleName);
                    sqlCommand.Parameters.AddWithValue("@value2", modelName);

                    try
                    {

                        // 5. Execute the query and get the count
                        int count = (int)sqlCommand.ExecuteScalar();
                        Cons.Close();

                        // 6. Check if data exists
                        if (count > 0)
                        {
                            MessageBox.Show("T & A Data Already exists! Cannot Submit another. Please proceed with Edit followed by Revised function.. ", MessageBoxIcon.Exclamation.ToString());
                            btnEdit.Enabled = true;
                            btnUpdate.Enabled = true;
                            return;

                        }
                        else
                        {
                            calDeviation();
                            saveTandAData();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }

        }

        private void saveTandAData()
        {
            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();

            try
            {
                foreach (DataGridViewRow rows in dgActionView.Rows)
                {
                    if (!rows.IsNewRow)
                    {
                        styleNumberTa = lblStylenumber.Text;
                        string selectedActivity = cmbActivity.SelectedValue.ToString();
                        modelNoTa = selectedActivity;
                        totalDaysTa = lblDays.Text;
                        actualTa = lblDeliveryDate.Text;
                        startDateTa = lblStartDate.Text;
                        activityTa = rows.Cells[0].Value.ToString();
                        responsibilityTa = rows.Cells[1].Value.ToString();
                        dataRangeTa = rows.Cells[2].Value.ToString();
                        dueDateTa = rows.Cells[3].Value.ToString();
                        planTa = rows.Cells[4].Value.ToString();
                        deviationTa = rows.Cells[5].Value.ToString();
                        actualDateTa = rows.Cells[7].Value.ToString();
                        remarksTa = rows.Cells[8].Value.ToString();

                        usernameTa = Login.PublicUsername;
                        forcastedDateTa = lblProjectedDelivery.Text;
                        editedTa = DateTime.Now.ToString();

                        using (SqlCommand cmd = new SqlCommand("insert into tblTimeAndAction (Style_No,Model_No,Total_ToDelivery,Actual_Delivery,Start_Date,Activity,Resposibility,Range,Duedate,PlanDate,Deviation,Actual,Remarks,Username,ForcastedDelay,EditedDate) values (@col1,@col2,@col3,@col4,@col5,@col6,@col7,@col8,@col9,@col10,@col11,@col12,@col13,@col14,@col15,@col16)", Cons))
                        {
                            cmd.Parameters.AddWithValue("@col1", styleNumberTa);
                            cmd.Parameters.AddWithValue("@col2", modelNoTa);
                            cmd.Parameters.AddWithValue("@col3", totalDaysTa);
                            cmd.Parameters.AddWithValue("@col4", actualTa);
                            cmd.Parameters.AddWithValue("@col5", startDateTa);
                            cmd.Parameters.AddWithValue("@col6", activityTa);
                            cmd.Parameters.AddWithValue("@col7", responsibilityTa);
                            cmd.Parameters.AddWithValue("@col8", dataRangeTa);
                            cmd.Parameters.AddWithValue("@col9", dueDateTa);
                            cmd.Parameters.AddWithValue("@col10", planTa);
                            cmd.Parameters.AddWithValue("@col11", deviationTa);
                            cmd.Parameters.AddWithValue("@col12", actualDateTa);
                            cmd.Parameters.AddWithValue("@col13", remarksTa);
                            cmd.Parameters.AddWithValue("@col14", usernameTa);
                            cmd.Parameters.AddWithValue("@col15", forcastedDateTa);
                            cmd.Parameters.AddWithValue("@col16", editedTa);

                            cmd.ExecuteNonQuery();
                        }
                    }

                }

                Cons.Close();
                MessageBox.Show("T&A Saved");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void loadRevisionId()
        {

            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();
            string style = lblStylenumber.Text;
            try
            {
                SqlCommand cmd = new SqlCommand(" select * from tblTimeAndActionRevision where style_no = '" + style + "'", Cons);
                SqlDataReader loadDataDR = cmd.ExecuteReader();
                while (loadDataDR.Read())
                {
                    string revisionString = loadDataDR["Revisions"].ToString();
                    if (revisionString.IsNullOrEmpty())
                    {
                        revisionString = "1";
                        //calRevisionDate = 0;
                    }
                    else
                    {
                        calRevisionDate = int.Parse(revisionString);
                        calRevisionDate = calRevisionDate + 1;
                    }


                }
                loadDataDR.Close();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void reviseData()
        {
            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();

            try
            {
                foreach (DataGridViewRow rows in dgActionView.Rows)
                {
                    if (!rows.IsNewRow)
                    {
                        styleNumberTa = lblStylenumber.Text;
                        string selectedActivity = cmbActivity.SelectedValue.ToString();
                        modelNoTa = selectedActivity;
                        totalDaysTa = lblDays.Text;
                        actualTa = lblDeliveryDate.Text;
                        startDateTa = lblStartDate.Text;
                        activityTa = rows.Cells[0].Value.ToString();
                        responsibilityTa = rows.Cells[1].Value.ToString();
                        dataRangeTa = rows.Cells[2].Value.ToString();
                        dueDateTa = rows.Cells[3].Value.ToString();
                        planTa = rows.Cells[4].Value.ToString();
                        deviationTa = rows.Cells[5].Value.ToString();
                        forcastedTa = rows.Cells[6].Value.ToString();
                        actualDateTa = rows.Cells[7].Value.ToString();
                        remarksTa = rows.Cells[8].Value.ToString();
                        usernameTa = Login.PublicUsername;
                        forcastedDateTa = lblProjectedDelivery.Text;
                        editedTa = DateTime.Now.ToString();
                        string revisionCal = calRevisionDate.ToString();

                        using (SqlCommand cmd = new SqlCommand("insert into tblTimeAndActionRevision (Style_No,Model_No,Total_ToDelivery,Actual_Delivery,Start_Date,Activity,Resposibility,Range,Duedate,PlanDate,Deviation,ForcastedDates,Actual,Remarks,Username,ForcastedDelay,EditedDate,Revisions) values (@col1,@col2,@col3,@col4,@col5,@col6,@col7,@col8,@col9,@col10,@col11,@col12,@col13,@col14,@col15,@col16,@col17)", Cons))
                        {
                            cmd.Parameters.AddWithValue("@col1", styleNumberTa);
                            cmd.Parameters.AddWithValue("@col2", modelNoTa);
                            cmd.Parameters.AddWithValue("@col3", totalDaysTa);
                            cmd.Parameters.AddWithValue("@col4", actualTa);
                            cmd.Parameters.AddWithValue("@col5", startDateTa);
                            cmd.Parameters.AddWithValue("@col6", activityTa);
                            cmd.Parameters.AddWithValue("@col7", responsibilityTa);
                            cmd.Parameters.AddWithValue("@col8", dataRangeTa);
                            cmd.Parameters.AddWithValue("@col9", dueDateTa);
                            cmd.Parameters.AddWithValue("@col10", planTa);
                            cmd.Parameters.AddWithValue("@col11", deviationTa);
                            cmd.Parameters.AddWithValue("@col12", forcastedTa);
                            cmd.Parameters.AddWithValue("@col13", actualDateTa);
                            cmd.Parameters.AddWithValue("@col14", remarksTa);
                            cmd.Parameters.AddWithValue("@col15", usernameTa);
                            cmd.Parameters.AddWithValue("@col16", forcastedDateTa);
                            cmd.Parameters.AddWithValue("@col17", editedTa);
                            cmd.Parameters.AddWithValue("@col18", revisionCal);

                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                Cons.Close();
                MessageBox.Show("T&A Revised");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void UpdateDeviation1()
        {
            bool deviationStarted = false; // Flag to indicate if deviation has started
            double deviationToApply = 0;    // Store the deviation value to apply to subsequent rows

            if (dgActionView.Rows.Count == 0)
            {
                MessageBox.Show("No data available in DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int row = 0; row < dgActionView.Rows.Count; row++)
            {
                if (dgActionView.Rows[row].IsNewRow) continue;

                var dueDateValue = dgActionView.Rows[row].Cells[3].Value;
                var planDateValue = dgActionView.Rows[row].Cells[4].Value;
                var deviationDaysValue = dgActionView.Rows[row].Cells[2].Value;

                if (dueDateValue == null || planDateValue == null || deviationDaysValue == null)
                {
                    MessageBox.Show($"Missing data in Row {row + 1}.  Ensure all cells are populated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DateTime.TryParse(dueDateValue.ToString(), out DateTime dueDate) &&
                    DateTime.TryParse(planDateValue.ToString(), out DateTime planDate) &&
                    double.TryParse(deviationDaysValue.ToString(), out double deviationDays))
                {
                    TimeSpan difference = planDate - dueDate;
                    int dayDifference = difference.Days;

                    if (dayDifference != 0|| deviationStarted ) // Apply deviation if dayDifference != 0 OR we've already started deviating
                    {
                       
                            deviationStarted = true;   // Flag that we've started the deviation process

                            deviationToApply = deviationDays; // Store the initial deviation value to keep applying
                     

                        planDate = planDate.AddDays(deviationToApply); // Apply the deviation
                       
                        dgActionView.Rows[row].Cells[6].Value = planDate.ToString("yyyy-MM-dd"); // Update column 6
                    }
                    else
                    {
                       
                            dgActionView.Rows[row].Cells[6].Value = planDate.ToString("yyyy-MM-dd");
                   

                    }
                }
                else
                {
                    MessageBox.Show($"Invalid data format in Row {row + 1}.  Check Due Date, Plan Date (yyyy-MM-dd), and Deviation Days (number).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }



        private void UpdateDeviation()
        {
            bool deviationStarted = false; // Flag to indicate if deviation has started
            //double deviationToApply = 0;    // Store the deviation value to apply to subsequent rows

            if (dgActionView.Rows.Count == 0)
            {
                MessageBox.Show("No data available in DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int row = 0; row < dgActionView.Rows.Count; row++)
            {
                if (dgActionView.Rows[row].IsNewRow) continue;

                var dueDateValue = dgActionView.Rows[row].Cells[3].Value;
                var planDateValue = dgActionView.Rows[row].Cells[4].Value;
                var deviationDaysValue = dgActionView.Rows[row].Cells[2].Value;

                if (dueDateValue == null || planDateValue == null || deviationDaysValue == null)
                {
                    MessageBox.Show($"Missing data in Row {row + 1}.  Ensure all cells are populated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DateTime.TryParse(dueDateValue.ToString(), out DateTime dueDate) &&
                    DateTime.TryParse(planDateValue.ToString(), out DateTime planDate) &&
                    double.TryParse(deviationDaysValue.ToString(), out double deviationDays))
                {
                    TimeSpan difference = planDate - dueDate;
                    int dayDifference = difference.Days;

                    if (dayDifference != 0 || deviationStarted)
                    {
                        deviationStarted = true;  //Set deviation strat flag true.
                        planDate = planDate.AddDays(deviationDays);                        
                        dgActionView.Rows[row].Cells[6].Value = planDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        // Set column 6 to 0 (or empty string, depending on your column type) if there's no date difference
                        dgActionView.Rows[row].Cells[6].Value = "0"; // Or "" if it's a string column
                    }
                }
                else
                {
                    MessageBox.Show($"Invalid data format in Row {row + 1}.  Check Due Date, Plan Date (yyyy-MM-dd), and Deviation Days (number).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


        }


        DateTime lastModifiedDate; // To keep track of the last modified date  
        private string format;

        private void updateDeviationDates()
        {
            {
                if (dgActionView.Rows.Count > 0)
                {
                    DateTime planDate = DateTime.MinValue;

                    // Loop through all rows in DataGridView
                    for (int row = 0; row < dgActionView.Rows.Count; row++)
                    {
                        // Ensure the row is not a new row
                        if (!dgActionView.Rows[row].IsNewRow)
                        {
                            // Get the date from column index 0 (Deviation Dates)
                            var workingDateValue = dgActionView.Rows[row].Cells[5].Value;
                            //CopyFilledColumnValues();
                            // Get the plan date from column index 1 (Plan Date)
                            var planDateValue = dgActionView.Rows[row].Cells[4].Value;

                            // If there's a valid plan date, parse it
                            if (planDateValue != null && DateTime.TryParse(planDateValue.ToString(), out planDate))
                            {
                                if (workingDateValue != null && int.TryParse(workingDateValue.ToString(), out int workingDays))
                                {
                                    // Add working days to the plan date
                                    planDate = planDate.AddDays(workingDays);

                                    // Update the plan date in column 6
                                    dgActionView.Rows[row].Cells[6].Value = planDate.ToString("yyyy-MM-dd");
                                    dgActionView.Rows[row + 1].Cells[6].Value = planDate;

                                }
                            }
                            else
                            {
                                MessageBox.Show($"Invalid Plan Date in Row {row + 1}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Exit if there is an invalid date
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No data available in DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void UpdatePlanDates()
        {
            {
                if (dgActionView.Rows.Count > 0)
                {
                    DateTime planDate = DateTime.MinValue;

                    // Loop through all rows in DataGridView
                    for (int row = 0; row < dgActionView.Rows.Count; row++)
                    {
                        // Ensure the row is not a new row
                        if (!dgActionView.Rows[row].IsNewRow)
                        {
                            // Get the working date from column index 0 (Working Dates)
                            var workingDateValue = dgActionView.Rows[row].Cells[2].Value;
                            //CopyFilledColumnValues();
                            // Get the plan date from column index 1 (Plan Date)
                            var planDateValue = dgActionView.Rows[row].Cells[3].Value;

                            // If there's a valid plan date, parse it
                            if (planDateValue != null && DateTime.TryParse(planDateValue.ToString(), out planDate))
                            {
                                if (workingDateValue != null && int.TryParse(workingDateValue.ToString(), out int workingDays))
                                {
                                    // Add working days to the plan date
                                    planDate = planDate.AddDays(workingDays);

                                    // Update the plan date in column 1
                                    dgActionView.Rows[row].Cells[3].Value = planDate.ToString("yyyy-MM-dd");
                                    dgActionView.Rows[row + 1].Cells[3].Value = planDate;

                                }
                            }
                            else
                            {
                                MessageBox.Show($"Invalid Plan Date in Row {row + 1}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Exit if there is an invalid date
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No data available in DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void CopyFilledColumnValues()
        {
            if (dgActionView.Rows.Count > 0)
            {
                // Loop through all rows in DataGridView
                for (int row = 0; row < dgActionView.Rows.Count - 1; row++) // Avoid last row to prevent out of range
                {
                    // Ensure the row is not a new row
                    if (!dgActionView.Rows[row].IsNewRow)
                    {
                        // Get the value from the current row in column index 0 (Working Dates)
                        var currentValue = dgActionView.Rows[row].Cells[3].Value;

                        // Check if current value is not null or empty
                        if (currentValue != null && !string.IsNullOrWhiteSpace(currentValue.ToString()))
                        {
                            // Copy value to the next row in the same column
                            dgActionView.Rows[row + 1].Cells[3].Value = currentValue;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No data available in DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void loadActivity()
        {
            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();
            string customName = cmbActivity.Text;
            dgActionView.Rows.Clear();
            try
            {
                SqlCommand cmd = new SqlCommand(" select * from Custom_TandA where Custome_Name = '" + customName + "'", Cons);
                SqlDataReader loadDataDR = cmd.ExecuteReader();
                while (loadDataDR.Read())
                {
                    // activityId = loadDataDR["Activity_id"].ToString();
                    activityOrder = loadDataDR["Activity_Order"].ToString();
                    activityName = loadDataDR["Activity_Name"].ToString();
                    resposibleName = loadDataDR["Responsible_Name"].ToString();
                    range = loadDataDR["range"].ToString();

                    int i = dgActionView.Rows.Add();
                    dgActionView.Rows[i].Cells[0].Value = activityName;
                    dgActionView.Rows[i].Cells[1].Value = resposibleName;
                    dgActionView.Rows[i].Cells[2].Value = range;
                    dgActionView.Rows[i].Cells[5].Value = "0";
                    dgActionView.Rows[i].Cells[7].Value = "0";
                    dgActionView.Rows[i].Cells[8].Value = "0";
                    intRange = int.Parse(range);
                    btnCalculate_Click();

                }
                loadDataDR.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void copyCellData()
        {
            // Iterate through each row and copy data from source to target column
            foreach (DataGridViewRow row in dgActionView.Rows)
            {
                if (!row.IsNewRow) // Skip the new row placeholder
                {
                    // Copy value from source column to target column
                    row.Cells[4].Value = row.Cells[3].Value;
                }
            }

        }

        private void loadData()
        {
            SqlConnection Cons = new SqlConnection(conString);

            string style = cmdstyle.Text;
            try
            {
                Cons.Open();
                SqlCommand cmd = new SqlCommand(" select * from tblModel where style_no = '" + style + "'", Cons);
                SqlDataReader loadDataDR = cmd.ExecuteReader();
                while (loadDataDR.Read())
                {
                    styleName = loadDataDR["style_no"].ToString();
                    deliveryDate = loadDataDR["delivery_date"].ToString();
                    lblDeliveryDate.Text = deliveryDate;
                    lblStylenumber.Text = styleName;
                }
                loadDataDR.Close();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void calculateMinusAndAddingDates()
        {
            // Iterate through the rows of the DataGridView
            for (int i = 0; i < dgActionView.Rows.Count; i++)
            {
                // Ensure that the current row is not the Add New Row and that the cells we are accessing are not null
                if (!dgActionView.Rows[i].IsNewRow && dgActionView.Rows[i].Cells[2].Value != null && dgActionView.Rows[i].Cells[3].Value != null)
                {
                    DateTime? dueDate = null;
                    DateTime? planDate = null;

                    try
                    {
                        dueDate = Convert.ToDateTime(dgActionView.Rows[i].Cells[3].Value);
                    }
                    catch (FormatException)
                    {
                        dgActionView.Rows[i].Cells[5].Value = "Invalid Date";
                        dgActionView.Rows[i].Cells[5].Style.BackColor = Color.White;
                        continue; // Skip to the next row
                    }

                    try
                    {
                        planDate = Convert.ToDateTime(dgActionView.Rows[i].Cells[4].Value);
                    }
                    catch (FormatException)
                    {
                        dgActionView.Rows[i].Cells[5].Value = "Invalid Date";
                        dgActionView.Rows[i].Cells[5].Style.BackColor = Color.White;
                        continue; // Skip to the next row
                    }

                    if (dueDate.HasValue && planDate.HasValue)
                    {
                        // Calculate the initial date difference (Plan Date - Due Date)
                        TimeSpan initialDifference = dueDate.Value - planDate.Value;
                        int daysDifference = initialDifference.Days;

                        // Store the difference in Column 5 for the current row
                        dgActionView.Rows[i].Cells[5].Value = daysDifference.ToString();

                        // Highlight the cell based on the initial difference
                        if (daysDifference > 0)
                        {
                            dgActionView.Rows[i].Cells[5].Style.BackColor = Color.LightGreen;
                        }
                        else if (daysDifference < 0)
                        {
                            dgActionView.Rows[i].Cells[5].Style.BackColor = Color.LightCoral;
                        }
                        else
                        {
                            dgActionView.Rows[i].Cells[5].Style.BackColor = Color.White;
                        }

                        // Rolling Calculation and Formatting for subsequent rows
                        double accumulatedValue = daysDifference; // Use double for possible non-integer days

                        for (int j = i + 1; j < dgActionView.Rows.Count; j++)
                        {
                            if (!dgActionView.Rows[j].IsNewRow && dgActionView.Rows[j].Cells[4].Value != null)
                            {
                                DateTime? currentRowPlanDate = null;

                                try
                                {
                                    currentRowPlanDate = Convert.ToDateTime(dgActionView.Rows[j].Cells[4].Value);
                                }
                                catch (FormatException)
                                {
                                    dgActionView.Rows[j].Cells[5].Value = "Invalid Date";
                                    continue; // Skip to the next row
                                }

                                if (currentRowPlanDate.HasValue)
                                {
                                    // Perform the rolling calculation
                                    DateTime calculatedDate;
                                    if (daysDifference > 0) // Subtract if initial difference was positive
                                    {
                                        calculatedDate = currentRowPlanDate.Value.AddDays(-accumulatedValue);

                                    }
                                    else // Add if initial difference was negative
                                    {
                                        calculatedDate = currentRowPlanDate.Value.AddDays(accumulatedValue);

                                    }

                                    TimeSpan nextDifference = currentRowPlanDate.Value - calculatedDate;
                                    double nextDaysDifference = nextDifference.TotalDays; //Use double to keep the correct values
                                    dgActionView.Rows[j].Cells[5].Value = nextDaysDifference.ToString();
                                }
                                else
                                {
                                    dgActionView.Rows[j].Cells[5].Value = "N/A";
                                }

                            }
                            else
                            {
                                dgActionView.Rows[j].Cells[5].Value = "";
                            }
                        }

                    }
                    else
                    {
                        dgActionView.Rows[i].Cells[5].Value = "N/A";
                        dgActionView.Rows[i].Cells[5].Style.BackColor = Color.White;
                    }
                }
            }
        


    }

    

    private void btnCalculate_Click()
        {
            // Get the date from the TextBox (assuming deliveryDate is a TextBox)
            DateTime givenDate;

            // Ensure you are getting the text from the TextBox correctly
            string deliveryDatee = deliveryDate; // Replace with your actual TextBox name

            // Try to parse the date
            if (DateTime.TryParse(deliveryDatee, out givenDate))
            {
                // Calculate the total value from a specific column (e.g., index 2)
                double totalValue = 0;

                foreach (DataGridViewRow row in dgActionView.Rows)
                {
                    // Check if the cell in column index 2 has a value
                    if (row.Cells[2].Value != null && !string.IsNullOrWhiteSpace(row.Cells[2].Value.ToString()))
                    {
                        // Convert the cell value to double and add to total
                        double cellValue;
                        if (double.TryParse(row.Cells[2].Value.ToString(), out cellValue))
                        {
                            totalValue += cellValue;                        
                        }
                    }
                }
                lblDays.Text = totalValue.ToString();
                // Subtract the total value from the given date
                DateTime resultDate = givenDate.AddDays(-totalValue);

                // Display the result in a TextBox or any other control
                lblStartDate.Text = resultDate.ToString("dd/MM/yyyy"); // Format as desired
                dgActionView.Rows[0].Cells[3].Value = lblStartDate.Text;
                //lblStartDate.Text = resultDate.ToString("yyyy-MM-dd");
                
            }
            else
            {
                MessageBox.Show("Please enter a valid date.");
            }

        }



    }

     



}

