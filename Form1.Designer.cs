namespace UDP_TCP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTCPPing = new Button();
            btnUDPPing = new Button();
            rtbResults = new RichTextBox();
            SuspendLayout();
            // 
            // btnTCPPing
            // 
            btnTCPPing.AutoSize = true;
            btnTCPPing.BackColor = Color.DarkKhaki;
            btnTCPPing.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnTCPPing.ForeColor = Color.Crimson;
            btnTCPPing.Location = new Point(29, 85);
            btnTCPPing.Name = "btnTCPPing";
            btnTCPPing.Size = new Size(576, 102);
            btnTCPPing.TabIndex = 1;
            btnTCPPing.Text = "TCP  PİNG BAŞLAT";
            btnTCPPing.UseVisualStyleBackColor = false;
            btnTCPPing.Click += btnTCPPing_Click;
            // 
            // btnUDPPing
            // 
            btnUDPPing.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnUDPPing.BackColor = Color.DarkGreen;
            btnUDPPing.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnUDPPing.ForeColor = Color.DarkOrange;
            btnUDPPing.Location = new Point(661, 85);
            btnUDPPing.Name = "btnUDPPing";
            btnUDPPing.Size = new Size(601, 102);
            btnUDPPing.TabIndex = 1;
            btnUDPPing.Text = "UDP  PİNG BAŞLAT";
            btnUDPPing.UseVisualStyleBackColor = false;
            btnUDPPing.Click += btnUDPPing_Click_1;
            // 
            // rtbResults
            // 
            rtbResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbResults.BackColor = SystemColors.ActiveBorder;
            rtbResults.Font = new Font("Segoe UI Black", 22F, FontStyle.Bold, GraphicsUnit.Point, 162);
            rtbResults.ForeColor = Color.Black;
            rtbResults.Location = new Point(12, 204);
            rtbResults.Name = "rtbResults";
            rtbResults.ReadOnly = true;
            rtbResults.Size = new Size(1250, 771);
            rtbResults.TabIndex = 2;
            rtbResults.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(1274, 985);
            Controls.Add(rtbResults);
            Controls.Add(btnUDPPing);
            Controls.Add(btnTCPPing);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTCPPing;
        private Button btnUDPPing;
        private RichTextBox rtbResults;
    }
}
