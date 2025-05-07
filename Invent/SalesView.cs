using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invent
{
    public partial class SalesView: Form
    {
        public SalesView()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=MTX-SRV-APP1;Initial Catalog=Actiondb;Integrated Security=True;Trust Server Certificate=True";
        private void SalesView_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'actiondbDataSet1.tblModel' table. You can move, or remove it, as needed.
            this.tblModelTableAdapter.Fill(this.actiondbDataSet1.tblModel);
            btnDelete.Enabled = false;
            //dataGridView1.Columns["Column4"].DefaultCellStyle.BackColor = Color.LightBlue;
            //dataGridView1.Columns["Column5"].DefaultCellStyle.BackColor = Color.LightBlue;
            //dataGridView1.Columns["Column6"].DefaultCellStyle.BackColor = Color.LightCoral;
            //dataGridView1.Columns["Column7"].DefaultCellStyle.BackColor = Color.LightCoral;
            //dataGridView1.Columns["Column8"].DefaultCellStyle.BackColor = Color.LightSeaGreen;
            //dataGridView1.Columns["Column9"].DefaultCellStyle.BackColor = Color.LightSeaGreen;
        }

        private void loadFobDate()
        {

            SqlConnection Cons = new SqlConnection(conString);
            Cons.Open();
            SqlCommand cmd = new SqlCommand("select * from tblModel where style_no = '" + comboBox3.Text + "'", Cons);
            SqlDataReader selectFobDr = cmd.ExecuteReader();
            while (selectFobDr.Read())
            {
                lblFob.Text = selectFobDr["fob"].ToString();
            }
            Cons.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            loadFobDate();
            string saleYear, saleQuarter, saleMonth, saleCustomer, saleStyle, saleQty, saleSeason, saleFob, saleValue, saledescription, saleUpdateUser, saleUpdatedatetime;
            
            saleStyle = comboBox3.Text;
            saleYear = cmbYear.Text;
            saleQuarter = cmbQuarter.Text;
            saleMonth = cmbMonth.Text;
            saleCustomer = txtCustomer.Text;
            saleQty = txtQty.Text;
            saleFob = lblFob.Text;
            saleSeason = txtSeason.Text;
            saledescription = txtSeason.Text;
            saleUpdatedatetime = DateTime.Now.ToString();
            saleUpdateUser = Login.PublicUsername;
            int budgetQty = int.Parse(saleQty);
            int fobValue = int.Parse(saleFob);
            int calculatedvalue = budgetQty * fobValue;

            string calValue = calculatedvalue.ToString();
            lblValue.Text = calValue;
            saleValue = calValue;


            //adding values to the data grid

            int i = dgBudgetEntry.Rows.Add();
            dgBudgetEntry.Rows[i].Cells[0].Value = saleStyle;
            dgBudgetEntry.Rows[i].Cells[1].Value = saleYear;
            dgBudgetEntry.Rows[i].Cells[2].Value = saleQuarter;
            dgBudgetEntry.Rows[i].Cells[3].Value = saleMonth;
            dgBudgetEntry.Rows[i].Cells[4].Value = saleCustomer;
            dgBudgetEntry.Rows[i].Cells[5].Value = saleQty;
            dgBudgetEntry.Rows[i].Cells[6].Value = saleValue;
            dgBudgetEntry.Rows[i].Cells[7].Value = saleFob;
            dgBudgetEntry.Rows[i].Cells[8].Value = saleSeason;
            dgBudgetEntry.Rows[i].Cells[9].Value = saledescription;



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgBudgetEntry.SelectedRows.Count > 0)
            {
                // Remove rows starting from the last selected row to avoid index issues
                for (int i = dgBudgetEntry.SelectedRows.Count - 1; i >= 0; i--)
                {
                    dgBudgetEntry.Rows.RemoveAt(dgBudgetEntry.SelectedRows[i].Index);
                }
            }
            else
            {
                MessageBox.Show("Please select at least one row to remove.");
            }
        }
    }
}
