namespace _61850_Client_v1._0a
{
    partial class frm61850Client
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
            this.ckbCID_Files = new System.Windows.Forms.CheckedListBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.rtbDispley = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ckbCID_Files
            // 
            this.ckbCID_Files.FormattingEnabled = true;
            this.ckbCID_Files.Location = new System.Drawing.Point(12, 63);
            this.ckbCID_Files.Name = "ckbCID_Files";
            this.ckbCID_Files.Size = new System.Drawing.Size(560, 191);
            this.ckbCID_Files.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnImport.Location = new System.Drawing.Point(12, 12);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(116, 45);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import CID";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // rtbDispley
            // 
            this.rtbDispley.Location = new System.Drawing.Point(12, 260);
            this.rtbDispley.Name = "rtbDispley";
            this.rtbDispley.ReadOnly = true;
            this.rtbDispley.Size = new System.Drawing.Size(546, 290);
            this.rtbDispley.TabIndex = 2;
            this.rtbDispley.Text = "";
            // 
            // frm61850Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.rtbDispley);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.ckbCID_Files);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm61850Client";
            this.Text = "frm61850Client";
            this.Load += new System.EventHandler(this.frm61850Client_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ckbCID_Files;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.RichTextBox rtbDispley;
    }
}