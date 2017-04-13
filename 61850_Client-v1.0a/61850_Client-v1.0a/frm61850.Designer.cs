namespace _61850_Client_v1._0a
{
    partial class frm61850
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
            this.btn61850_Client = new System.Windows.Forms.Button();
            this.btn61850_Server = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn61850_Client
            // 
            this.btn61850_Client.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn61850_Client.Location = new System.Drawing.Point(12, 12);
            this.btn61850_Client.Name = "btn61850_Client";
            this.btn61850_Client.Size = new System.Drawing.Size(227, 111);
            this.btn61850_Client.TabIndex = 0;
            this.btn61850_Client.Text = "61850 Client";
            this.btn61850_Client.UseVisualStyleBackColor = true;
            this.btn61850_Client.Click += new System.EventHandler(this.btn61850_Client_Click);
            // 
            // btn61850_Server
            // 
            this.btn61850_Server.Font = new System.Drawing.Font("Calibri", 24F);
            this.btn61850_Server.Location = new System.Drawing.Point(245, 12);
            this.btn61850_Server.Name = "btn61850_Server";
            this.btn61850_Server.Size = new System.Drawing.Size(227, 111);
            this.btn61850_Server.TabIndex = 1;
            this.btn61850_Server.Text = "61850 Server";
            this.btn61850_Server.UseVisualStyleBackColor = true;
            this.btn61850_Server.Click += new System.EventHandler(this.btn61850_Server_Click);
            // 
            // frm61850
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 139);
            this.Controls.Add(this.btn61850_Server);
            this.Controls.Add(this.btn61850_Client);
            this.Name = "frm61850";
            this.Text = "61850 Simulator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn61850_Client;
        private System.Windows.Forms.Button btn61850_Server;
    }
}