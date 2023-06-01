namespace Final_Project
{
    partial class BillDetailReport
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rpvBillDetail = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvBillDetail
            // 
            reportDataSource1.Name = "ProductBillDataset";
            reportDataSource1.Value = null;
            this.rpvBillDetail.LocalReport.DataSources.Add(reportDataSource1);
            this.rpvBillDetail.LocalReport.ReportEmbeddedResource = "Final_Project.Reporting.ReportProductBill.rdlc";
            this.rpvBillDetail.Location = new System.Drawing.Point(0, -1);
            this.rpvBillDetail.Name = "rpvBillDetail";
            this.rpvBillDetail.Size = new System.Drawing.Size(705, 449);
            this.rpvBillDetail.TabIndex = 0;
            // 
            // BillDetailReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 450);
            this.Controls.Add(this.rpvBillDetail);
            this.Name = "BillDetailReport";
            this.Text = "BillDetailReport";
            this.Load += new System.EventHandler(this.BillDetailReport_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Reporting.WinForms.ReportViewer rpvBillDetail;
    }
}