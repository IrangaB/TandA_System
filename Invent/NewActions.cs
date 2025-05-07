using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Invent
{
    public partial class NewActions : Form
    {
        public NewActions()
        {
            InitializeComponent();
        }

        //public string conString = "Data Source=MAS-5CD4241CYM\\SQLEXPRESS;Initial Catalog=Actiondb;Integrated Security=True;";
        public string conString = "Data Source=MTX-SRV-APP1;Initial Catalog=Actiondb;Integrated Security=True;Trust Server Certificate=True";

        private void NewActions_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'actiondbDataSet.tbtStandard_TandA' table. You can move, or remove it, as needed.
            this.tbtStandard_TandATableAdapter.Fill(this.actiondbDataSet.tbtStandard_TandA);
            dgstand.ReadOnly = true;
        }


        private void CopyDataBetweenDataGrids(DataGridView source, DataGridView destination)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();

            // Clone columns from source to destination
            foreach (DataGridViewColumn column in dgstand.Columns)
            {
                dataGridView2.Columns.Add((DataGridViewColumn)column.Clone());
            }


            // Copy rows from source to destination
            foreach (DataGridViewRow row in dgstand.Rows)
            {
                // Skip new row placeholder if it exists
                if (row.IsNewRow) continue;

                int rowIndex = dataGridView2.Rows.Add(); // Add a new row and get its index
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    // Copy cell values
                    dataGridView2.Rows[rowIndex].Cells[i].Value = row.Cells[i].Value;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            CopyDataBetweenDataGrids(dgstand, dataGridView2);



        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            this.tbtStandard_TandATableAdapter.Fill(this.actiondbDataSet.tbtStandard_TandA);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Loop through the selected rows in reverse order
                for (int i = dataGridView2.SelectedRows.Count - 1; i >= 0; i--)
                {
                    // Get the index of the selected row
                    int rowIndex = dataGridView2.SelectedRows[i].Index;
                    // Remove the row from the DataGridView
                    dataGridView2.Rows.RemoveAt(rowIndex);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string ActivName = txtActivityName.Text;


                string query = "SELECT COUNT(*) AS TotalCount FROM Custom_TandA WHERE Custome_Name = @ActivName";
                using (SqlCommand cmdd = new SqlCommand(query, conn))
                {
                    // Use parameterized query to prevent SQL injection
                    cmdd.Parameters.AddWithValue("@ActivName", ActivName);

                    // Execute the query and get the count
                    int totalCount = (int)cmdd.ExecuteScalar();

                    if (totalCount > 0)
                    {
                        MessageBox.Show($"The activity name '{ActivName}' already exists in the database.");
                    }
                    else
                    {
                        
                        if (dataGridView2.Rows.Count == 0)
                        {
                            MessageBox.Show("Table is empty so you cannot submit.");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(txtActivityName.Text))
                        {
                            MessageBox.Show("Please enter a valid activity name.");
                            return;
                        }

                        try
                        {
                            using (SqlConnection Cons = new SqlConnection(conString))
                            {
                                Cons.Open();

                                using (SqlTransaction transaction = Cons.BeginTransaction())
                                {
                                    try
                                    {
                                        foreach (DataGridViewRow row in dataGridView2.Rows)
                                        {
                                            if (!row.IsNewRow)
                                            {
                                                string queryinsert = "INSERT INTO Custom_TandA (Activity_Order, Activity_Name, Responsible_Name, Range, Custome_Name, Created_By) VALUES (@value1, @value2, @value3, @value4, @value5, @value6)";
                                                using (SqlCommand cmd = new SqlCommand(queryinsert, Cons, transaction))
                                                {
                                                    cmd.Parameters.AddWithValue("@value1", row.Cells["activityidDataGridViewTextBoxColumn"].Value ?? DBNull.Value);
                                                    cmd.Parameters.AddWithValue("@value2", row.Cells["activityDataGridViewTextBoxColumn"].Value ?? DBNull.Value);
                                                    cmd.Parameters.AddWithValue("@value3", row.Cells["responsibleDataGridViewTextBoxColumn"].Value ?? DBNull.Value);
                                                    cmd.Parameters.AddWithValue("@value4", row.Cells["rangeDataGridViewTextBoxColumn"].Value ?? DBNull.Value);
                                                    cmd.Parameters.AddWithValue("@value5", txtActivityName.Text);
                                                    cmd.Parameters.AddWithValue("@value6", Login.PublicUsername);

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }

                                        transaction.Commit();
                                        MessageBox.Show("New Template " + txtActivityName.Text + " Added");
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show($"An error occurred: {ex.Message}");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while connecting to the database: {ex.Message}");
                        }

                    }

                }

            }


        }


    }
}


