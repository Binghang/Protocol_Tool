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
            this.txtCID_Path = new System.Windows.Forms.TextBox();
            this.btnReadXML = new System.Windows.Forms.Button();
            this.btnStartTest = new System.Windows.Forms.Button();
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
            this.rtbDispley.Size = new System.Drawing.Size(560, 290);
            this.rtbDispley.TabIndex = 2;
            this.rtbDispley.Text = "";
            // 
            // txtCID_Path
            // 
            this.txtCID_Path.Location = new System.Drawing.Point(134, 12);
            this.txtCID_Path.Name = "txtCID_Path";
            this.txtCID_Path.Size = new System.Drawing.Size(438, 22);
            this.txtCID_Path.TabIndex = 3;
            // 
            // btnReadXML
            // 
            this.btnReadXML.Location = new System.Drawing.Point(497, 37);
            this.btnReadXML.Name = "btnReadXML";
            this.btnReadXML.Size = new System.Drawing.Size(75, 23);
            this.btnReadXML.TabIndex = 4;
            this.btnReadXML.Text = "Read XML";
            this.btnReadXML.UseVisualStyleBackColor = true;
            this.btnReadXML.Click += new System.EventHandler(this.btnReadXML_Click);
            // 
            // btnStartTest
            // 
            this.btnStartTest.Location = new System.Drawing.Point(416, 37);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(75, 23);
            this.btnStartTest.TabIndex = 5;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // frm61850Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.btnStartTest);
            this.Controls.Add(this.btnReadXML);
            this.Controls.Add(this.txtCID_Path);
            this.Controls.Add(this.rtbDispley);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.ckbCID_Files);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm61850Client";
            this.Text = "frm61850Client";
            this.Load += new System.EventHandler(this.frm61850Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ckbCID_Files;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.RichTextBox rtbDispley;
        private System.Windows.Forms.TextBox txtCID_Path;
        private System.Windows.Forms.Button btnReadXML;
        private System.Windows.Forms.Button btnStartTest;
    }
}