namespace DoAnNet
{
    partial class FormSoDoSan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.flowPanelSan = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowPanelSan
            // 
            this.flowPanelSan.AutoScroll = true;
            this.flowPanelSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.flowPanelSan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelSan.Location = new System.Drawing.Point(0, 0);
            this.flowPanelSan.Name = "flowPanelSan";
            this.flowPanelSan.Padding = new System.Windows.Forms.Padding(20);
            this.flowPanelSan.Size = new System.Drawing.Size(990, 636); // Chuẩn size panel
            this.flowPanelSan.TabIndex = 0;
            // 
            // FormSoDoSan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            // KÍCH THƯỚC CHUẨN: 990, 636
            this.ClientSize = new System.Drawing.Size(990, 636);
            this.Controls.Add(this.flowPanelSan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSoDoSan";
            this.Text = "FormSoDoSan";
            this.ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel flowPanelSan;
    }
}