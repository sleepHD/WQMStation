namespace HJ212FieldTest
{
    partial class MainForm
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
            this.btnReportHData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReportHData
            // 
            this.btnReportHData.Location = new System.Drawing.Point(31, 21);
            this.btnReportHData.Name = "btnReportHData";
            this.btnReportHData.Size = new System.Drawing.Size(130, 38);
            this.btnReportHData.TabIndex = 0;
            this.btnReportHData.Text = "发送小时数据";
            this.btnReportHData.UseVisualStyleBackColor = true;
            this.btnReportHData.Click += new System.EventHandler(this.btnReportHData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 457);
            this.Controls.Add(this.btnReportHData);
            this.Name = "MainForm";
            this.Text = "HJ212FieldTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReportHData;
    }
}

