namespace DoAnNet
{
    partial class FormThanhToan
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.dtpGioVao = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpGioRa = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.txtThanhTien = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThanhToan = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTieuDe.Location = new System.Drawing.Point(53, 37);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(290, 54);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "THANH TOÁN";
            // 
            // lblTenKH
            // 
            this.lblTenKH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTenKH.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTenKH.Location = new System.Drawing.Point(64, 111);
            this.lblTenKH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(1187, 37);
            this.lblTenKH.TabIndex = 1;
            this.lblTenKH.Text = "Khách hàng: ...";
            // 
            // dtpGioVao
            // 
            this.dtpGioVao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpGioVao.BorderRadius = 10;
            this.dtpGioVao.Checked = true;
            this.dtpGioVao.Enabled = false;
            this.dtpGioVao.FillColor = System.Drawing.Color.LightGray;
            this.dtpGioVao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpGioVao.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioVao.Location = new System.Drawing.Point(53, 209);
            this.dtpGioVao.Margin = new System.Windows.Forms.Padding(4);
            this.dtpGioVao.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpGioVao.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpGioVao.Name = "dtpGioVao";
            this.dtpGioVao.Size = new System.Drawing.Size(1213, 62);
            this.dtpGioVao.TabIndex = 2;
            this.dtpGioVao.Value = new System.DateTime(2025, 12, 13, 19, 55, 43, 224);
            // 
            // dtpGioRa
            // 
            this.dtpGioRa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpGioRa.BorderRadius = 10;
            this.dtpGioRa.Checked = true;
            this.dtpGioRa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpGioRa.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioRa.Location = new System.Drawing.Point(53, 332);
            this.dtpGioRa.Margin = new System.Windows.Forms.Padding(4);
            this.dtpGioRa.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpGioRa.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpGioRa.Name = "dtpGioRa";
            this.dtpGioRa.Size = new System.Drawing.Size(1213, 62);
            this.dtpGioRa.TabIndex = 3;
            this.dtpGioRa.Value = new System.DateTime(2025, 12, 13, 19, 55, 43, 252);
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThanhTien.BorderRadius = 10;
            this.txtThanhTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtThanhTien.DefaultText = "";
            this.txtThanhTien.FillColor = System.Drawing.Color.Yellow;
            this.txtThanhTien.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtThanhTien.ForeColor = System.Drawing.Color.Red;
            this.txtThanhTien.Location = new System.Drawing.Point(53, 431);
            this.txtThanhTien.Margin = new System.Windows.Forms.Padding(11);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.PlaceholderText = "";
            this.txtThanhTien.ReadOnly = true;
            this.txtThanhTien.SelectedText = "";
            this.txtThanhTien.Size = new System.Drawing.Size(1213, 86);
            this.txtThanhTien.TabIndex = 4;
            this.txtThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BorderRadius = 15;
            this.btnThanhToan.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(53, 554);
            this.btnThanhToan.Margin = new System.Windows.Forms.Padding(4);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(267, 68);
            this.btnThanhToan.TabIndex = 5;
            this.btnThanhToan.Text = "THANH TOÁN";
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 15;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(347, 554);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(267, 68);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "HỦY BỎ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(67, 172);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Giờ vào:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(67, 295);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Giờ ra:";
            // 
            // FormThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1320, 783);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.txtThanhTien);
            this.Controls.Add(this.dtpGioRa);
            this.Controls.Add(this.dtpGioVao);
            this.Controls.Add(this.lblTenKH);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormThanhToan";
            this.Text = "Thanh Toán";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTenKH;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpGioVao;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpGioRa;
        private Guna.UI2.WinForms.Guna2TextBox txtThanhTien;
        private Guna.UI2.WinForms.Guna2Button btnThanhToan;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}