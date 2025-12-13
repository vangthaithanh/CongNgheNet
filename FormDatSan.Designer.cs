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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenKH = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpGioBatDau = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // --- 1. TIÊU ĐỀ ---
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblTieuDe.Location = new System.Drawing.Point(40, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(164, 45);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐẶT SÂN";

            // --- 2. TEXTBOX SĐT ---
            this.txtSDT.BorderRadius = 10;
            this.txtSDT.PlaceholderText = "Nhập SĐT khách hàng...";
            this.txtSDT.Location = new System.Drawing.Point(40, 110);
            this.txtSDT.Name = "txtSDT";
            // Width = 910 (để chừa lề 40 mỗi bên: 40+910+40 = 990)
            this.txtSDT.Size = new System.Drawing.Size(910, 50);
            this.txtSDT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDT.TabIndex = 1;

            // --- 3. TEXTBOX TÊN KH ---
            this.txtTenKH.BorderRadius = 10;
            this.txtTenKH.PlaceholderText = "Tên khách hàng";
            this.txtTenKH.ReadOnly = true;
            this.txtTenKH.Location = new System.Drawing.Point(40, 190);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(910, 50);
            this.txtTenKH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenKH.TabIndex = 2;

            // --- 4. DATE PICKER ---
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(45, 260);
            this.label1.Name = "label1";
            this.label1.Text = "Giờ bắt đầu:";

            this.dtpGioBatDau.BorderRadius = 10;
            this.dtpGioBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioBatDau.Location = new System.Drawing.Point(40, 290);
            this.dtpGioBatDau.Name = "dtpGioBatDau";
            this.dtpGioBatDau.Size = new System.Drawing.Size(910, 50);
            this.dtpGioBatDau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpGioBatDau.TabIndex = 3;

            // --- 5. NÚT BẤM ---
            this.btnLuu.BorderRadius = 15;
            this.btnLuu.FillColor = System.Drawing.Color.ForestGreen;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(40, 400);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(200, 55);
            this.btnLuu.Text = "XÁC NHẬN";
            this.btnLuu.TabIndex = 4;

            this.btnHuy.BorderRadius = 15;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(260, 400);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(200, 55);
            this.btnHuy.Text = "HỦY BỎ";
            this.btnHuy.TabIndex = 5;

            // --- FORM SETTINGS ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            // KÍCH THƯỚC CHUẨN: 990, 636
            this.ClientSize = new System.Drawing.Size(990, 636);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dtpGioBatDau);
            this.Controls.Add(this.txtTenKH);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDatSan";
            this.Text = "FormDatSan";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private Guna.UI2.WinForms.Guna2TextBox txtSDT;
        private Guna.UI2.WinForms.Guna2TextBox txtTenKH;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpGioBatDau;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.Label label1;
    }
}