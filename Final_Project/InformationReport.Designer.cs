namespace Final_Project
{
    partial class InformationReport
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
            this.rpvInfor = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvInfor
            // 
            this.rpvInfor.Location = new System.Drawing.Point(0, -1);
            this.rpvInfor.Name = "rpvInfor";
            this.rpvInfor.Size = new System.Drawing.Size(523, 454);
            this.rpvInfor.TabIndex = 0;
            // 
            // InformationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 349);
            this.Controls.Add(this.rpvInfor);
            this.Name = "InformationReport";
            this.Text = "InformationReport";
            this.Load += new System.EventHandler(this.InformationReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvInfor;
    }
}