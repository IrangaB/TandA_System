using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using System.Windows.Forms;

namespace Invent
{
    public partial class TaViews : Form
    {
        public TaViews()
        {
            InitializeComponent();
        }
        //public string conString = "Data Source=MAS-5CD4241CYM\\SQLEXPRESS;Initial Catalog=Actiondb;Integrated Security=True;";
        public string conString = "Data Source=MTX-SRV-APP1;Initial Catalog=Actiondb;Integrated Security=True;Trust Server Certificate=True";

        private void TaViews_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'actiondbDataSet5.tblTimeAndAction' table. You can move, or remove it, as needed.
            this.tblTimeAndActionTableAdapter1.Fill(this.actiondbDataSet5.tblTimeAndAction);
            // TODO: This line of code loads data into the 'actiondbDataSet4.tblTimeAndAction' table. You can move, or remove it, as needed.
            this.tblTimeAndActionTableAdapter.Fill(this.actiondbDataSet4.tblTimeAndAction);


            this.reportViewer1.RefreshReport();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateTimePicker1.Value;
            DateTime toDate = dateTimePicker2.Value;
            string selectedActivity = cmbActivity.Text;
            SqlConnection Cons = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("select * from tblTimeAndAction where activity = '" + selectedActivity + "' and duedate between '" + fromDate + "' and '" + toDate + "'", Cons);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.ReportPath = @"C:\Users\irangab\source\repos\Invent\Invent\Ksd.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("startDate", fromDate.ToString());
            parameters[1] = new ReportParameter("fromDate", toDate.ToString());
            reportViewer1.LocalReport.SetParameters(parameters);


            reportViewer1.RefreshReport();



        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.tblTimeAndActionTableAdapter1.FillBy(this.actiondbDataSet5.tblTimeAndAction);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
