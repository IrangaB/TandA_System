using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Invent
{
    public partial class Portal : Form
    {
        //private Size formOriginalSize;
        //private Rectangle recBut1;
        //private Rectangle recBut2;
        //private Rectangle recBut3;
        //private Rectangle recBut4;
        //private Rectangle recBut5;
        //private Rectangle recBut6;
        //private Rectangle recLbl1;
        //private Rectangle recLbl2;
        //private Rectangle recLbl3;
        //private Rectangle recLbl4;
        //private Rectangle recLbl5;
        //private Rectangle recLbl6;
        //private Rectangle recLbl7;
        //private Rectangle recLbl8;


        //private void resize_Control(Control c, Rectangle r)
        //{
        //    float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
        //    float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
        //    int newX = (int)(r.X * xRatio);
        //    int newY = (int)(r.Y * yRatio);

        //    int newWidth = (int)(r.Width * xRatio);
        //    int newHeight = (int)(r.Height * yRatio);

        //    c.Location = new Point(newX, newY);
        //    c.Size = new Size(newWidth, newHeight);
        //}


        public Portal(string publicUsername)
        {
            InitializeComponent();
            lblUser.Text = publicUsername;
            this.FormClosing += Portal_FormClosing;

            //this.Resize += Activityform_Resize;
            //formOriginalSize = this.Size;
            //recBut1 = new Rectangle(btnNewModel.Location, btnNewModel.Size);
            //recBut2 = new Rectangle(btnTemplate.Location, btnTemplate.Size);
            //recBut3 = new Rectangle(btnActivities.Location, btnTemplate.Size);
            //recBut4 = new Rectangle(btnSettings.Location, btnTemplate.Size);
            //recBut5 = new Rectangle(btnView.Location, btnView.Size);
            //recBut6 = new Rectangle(btnReports.Location, btnReports.Size);

            //recLbl1 = new Rectangle(label1.Location, label1.Size);
            //recLbl2 = new Rectangle(label2.Location, label2.Size);
            //recLbl3 = new Rectangle(label3.Location, label3.Size);
            //recLbl4 = new Rectangle(label4.Location, label4.Size);
            //recLbl5 = new Rectangle(label5.Location, label5.Size);
            //recLbl6 = new Rectangle(label6.Location, label6.Size);
            //recLbl7 = new Rectangle(label7.Location, label7.Size);
            //recLbl8 = new Rectangle(lblUser.Location, lblUser.Size);

        }

        private void Portal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to exit from the System ?", "Exit ", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
            {
                e.Cancel = true; // Cancel the closing event
            }
            else
            {
                Application.Exit(); // Exit the entire application
            }
        }

        //private void Activityform_Resize(object sender, EventArgs e)
        //{
        //    resize_Control(btnNewModel, recBut1);
        //    resize_Control(btnTemplate, recBut2);
        //    resize_Control(btnActivities, recBut3);
        //    resize_Control(btnSettings, recBut4);
        //    resize_Control(btnView, recBut5);
        //    resize_Control(btnReports, recBut6);

        //    resize_Control(label1, recLbl1);
        //    resize_Control(label2, recLbl2);
        //    resize_Control(label3, recLbl3);
        //    resize_Control(label4, recLbl4);
        //    resize_Control(label5, recLbl5);
        //    resize_Control(label6, recLbl6);
        //    resize_Control(label7, recLbl7);
        //    resize_Control(lblUser, recLbl8);

        //    //  throw new NotImplementedException();
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            NewModels NewModleEnter = new NewModels();
            NewModleEnter.Show();
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewActions NewActionForm = new NewActions();
            NewActionForm.Show();
            this.WindowState= FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TimeAndAction newTimeAndAction = new TimeAndAction();
            newTimeAndAction.Show();
            this.WindowState = FormWindowState.Minimized;


            //////Activityform newActivityForm = new Activityform();
            //////newActivityForm.Show();
            //////this.Hide();
        }

        private void Portal_Load(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {




            TaViews dashboardview = new TaViews();
            dashboardview.Show();
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm newSettings = new SettingsForm();
            newSettings.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            styleDateRange newReport = new styleDateRange();
            newReport.Show();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button1,0,button1.Height);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SalesView newSalesView = new SalesView();
            newSalesView.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
