namespace Invent
{
    partial class TaViews
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaViews));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbActivity = new System.Windows.Forms.ComboBox();
            this.actiondbDataSet4 = new Invent.ActiondbDataSet4();
            this.tblTimeAndActionBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.actiondbDataSet5 = new Invent.ActiondbDataSet5();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tblTimeAndActionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblTimeAndActionTableAdapter = new Invent.ActiondbDataSet4TableAdapters.tblTimeAndActionTableAdapter();
            this.tblTimeAndActionTableAdapter1 = new Invent.ActiondbDataSet5TableAdapters.tblTimeAndActionTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actiondbDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblTimeAndActionBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actiondbDataSet5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblTimeAndActionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(150, 35);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(438, 35);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(74, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 24);
            this.label1.TabIndex = 59;
            this.label1.Text = "From :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(385, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 24);
            this.label2.TabIndex = 60;
            this.label2.Text = "To :";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.DimGray;
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(1401, 32);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(86, 31);
            this.btnSubmit.TabIndex = 61;
            this.btnSubmit.Text = "View";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbActivity);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(17, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1602, 82);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Activity By Date Range";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(700, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 24);
            this.label3.TabIndex = 63;
            this.label3.Text = "Select Activity :";
            // 
            // cmbActivity
            // 
            this.cmbActivity.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.actiondbDataSet4, "tblTimeAndAction.Activity", true));
            this.cmbActivity.DataSource = this.tblTimeAndActionBindingSource1;
            this.cmbActivity.DisplayMember = "Activity";
            this.cmbActivity.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbActivity.FormattingEnabled = true;
            this.cmbActivity.Location = new System.Drawing.Point(842, 32);
            this.cmbActivity.Margin = new System.Windows.Forms.Padding(2);
            this.cmbActivity.Name = "cmbActivity";
            this.cmbActivity.Size = new System.Drawing.Size(360, 27);
            this.cmbActivity.TabIndex = 62;
            this.cmbActivity.ValueMember = "Activity";
            // 
            // actiondbDataSet4
            // 
            this.actiondbDataSet4.DataSetName = "ActiondbDataSet4";
            this.actiondbDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblTimeAndActionBindingSource1
            // 
            this.tblTimeAndActionBindingSource1.DataMember = "tblTimeAndAction";
            this.tblTimeAndActionBindingSource1.DataSource = this.actiondbDataSet5;
            // 
            // actiondbDataSet5
            // 
            this.actiondbDataSet5.DataSetName = "ActiondbDataSet5";
            this.actiondbDataSet5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(17, 134);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1602, 632);
            this.reportViewer1.TabIndex = 63;
            // 
            // tblTimeAndActionBindingSource
            // 
            this.tblTimeAndActionBindingSource.DataMember = "tblTimeAndAction";
            this.tblTimeAndActionBindingSource.DataSource = this.actiondbDataSet4;
            // 
            // tblTimeAndActionTableAdapter
            // 
            this.tblTimeAndActionTableAdapter.ClearBeforeFill = true;
            // 
            // tblTimeAndActionTableAdapter1
            // 
            this.tblTimeAndActionTableAdapter1.ClearBeforeFill = true;
            // 
            // TaViews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Invent.Properties.Resources.Sysback;
            this.ClientSize = new System.Drawing.Size(1631, 790);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaViews";
            this.Text = "TaViews";
            this.Load += new System.EventHandler(this.TaViews_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.actiondbDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblTimeAndActionBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actiondbDataSet5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblTimeAndActionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbActivity;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ActiondbDataSet4 actiondbDataSet4;
        private System.Windows.Forms.BindingSource tblTimeAndActionBindingSource;
        private ActiondbDataSet4TableAdapters.tblTimeAndActionTableAdapter tblTimeAndActionTableAdapter;
        private ActiondbDataSet5 actiondbDataSet5;
        private System.Windows.Forms.BindingSource tblTimeAndActionBindingSource1;
        private ActiondbDataSet5TableAdapters.tblTimeAndActionTableAdapter tblTimeAndActionTableAdapter1;
        private System.Windows.Forms.Label label3;
    }
}