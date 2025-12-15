namespace DoAnNet
{
    partial class FormDatSan
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenKH = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();

            // --- THAY ĐỔI: Tách Ngày và Giờ ---
            this.dtpNgayDat = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cboGioBatDau = new Guna.UI2.WinForms.Guna2ComboBox();
            // ----------------------------------

            this.SuspendLayout();

            // --- TIÊU ĐỀ ---
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblTieuDe.Location = new System.Drawing.Point(40, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(164, 45);
            this.lblTieuDe.Text = "ĐẶT SÂN";

            // --- SDT ---
            this.txtSDT.BorderRadius = 10;
            this.txtSDT.PlaceholderText = "Nhập SĐT khách hàng...";
            this.txtSDT.Location = new System.Drawing.Point(40, 100);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(910, 50);
            this.txtSDT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));

            // --- TÊN KH ---
            this.txtTenKH.BorderRadius = 10;
            this.txtTenKH.PlaceholderText = "Tên khách hàng";
            this.txtTenKH.ReadOnly = true;
            this.txtTenKH.Location = new System.Drawing.Point(40, 170);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(910, 50);
            this.txtTenKH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));

            // --- NGÀY ĐÁ (Mới) ---
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(45, 240);
            this.label1.Text = "Ngày đá:";

            this.dtpNgayDat.BorderRadius = 10;
            this.dtpNgayDat.Format = System.Windows.Forms.DateTimePickerFormat.Short; // Chỉ hiện ngày
            this.dtpNgayDat.Location = new System.Drawing.Point(40, 270);
            this.dtpNgayDat.Name = "dtpNgayDat";
            this.dtpNgayDat.Size = new System.Drawing.Size(440, 50); // Chiếm nửa form
            this.dtpNgayDat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));

            // --- GIỜ ĐÁ (Mới - Dùng ComboBox chọn cho sướng) ---
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(500, 240);
            this.label2.Text = "Giờ bắt đầu:";

            this.cboGioBatDau.BorderRadius = 10;
            this.cboGioBatDau.Location = new System.Drawing.Point(500, 270);
            this.cboGioBatDau.Name = "cboGioBatDau";
            this.cboGioBatDau.Size = new System.Drawing.Size(450, 36);
            this.cboGioBatDau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            // --- THÊM 2 DÒNG NÀY ĐỂ KHÔNG BỊ TRÀN MÀN HÌNH ---
            this.cboGioBatDau.MaxDropDownItems = 8; // Chỉ hiện tối đa 8 dòng
            this.cboGioBatDau.IntegralHeight = false; // Giúp list gọn gàng hơn

            // --- BUTTONS ---
            this.btnLuu.BorderRadius = 15;
            this.btnLuu.FillColor = System.Drawing.Color.ForestGreen;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(40, 380);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(200, 55);
            this.btnLuu.Text = "XÁC NHẬN";

            this.btnHuy.BorderRadius = 15;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(260, 380);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(200, 55);
            this.btnHuy.Text = "HỦY BỎ";

            // --- FORM ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(990, 636);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboGioBatDau); // Mới
            this.Controls.Add(this.dtpNgayDat);   // Mới
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtTenKH);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDatSan";
            this.Text = "FormDatSan";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTieuDe;
        private Guna.UI2.WinForms.Guna2TextBox txtSDT;
        private Guna.UI2.WinForms.Guna2TextBox txtTenKH;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        // 2 biến mới
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayDat;
        private Guna.UI2.WinForms.Guna2ComboBox cboGioBatDau;
    }
}