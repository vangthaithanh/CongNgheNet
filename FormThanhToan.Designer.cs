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
            this.txtThanhTien = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThanhToan = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.btnGoiMon = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGioRa = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.SuspendLayout();

            // --- TIÊU ĐỀ ---
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTieuDe.Location = new System.Drawing.Point(40, 20);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(229, 45);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "THANH TOÁN";

            // --- TÊN KH ---
            this.lblTenKH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTenKH.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTenKH.Location = new System.Drawing.Point(48, 80);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(890, 30);
            this.lblTenKH.TabIndex = 1;
            this.lblTenKH.Text = "Khách hàng: ...";

            // --- GIỜ VÀO (QUAN TRỌNG: ĐỔI SANG 24H) ---
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(50, 115);
            this.label1.Name = "label1";
            this.label1.Text = "Giờ vào:";

            this.dtpGioVao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpGioVao.BorderRadius = 10;
            this.dtpGioVao.Checked = true;
            this.dtpGioVao.Enabled = false;
            this.dtpGioVao.FillColor = System.Drawing.Color.LightGray;
            this.dtpGioVao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpGioVao.Location = new System.Drawing.Point(40, 140);
            this.dtpGioVao.Name = "dtpGioVao";
            this.dtpGioVao.Size = new System.Drawing.Size(910, 45);
            this.dtpGioVao.TabIndex = 2;
            this.dtpGioVao.ShowUpDown = true;
            // --- CẤU HÌNH 24 GIỜ ---
            this.dtpGioVao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGioVao.CustomFormat = "HH:mm"; // HH hoa là 24h, mm là phút
            // -----------------------

            // --- GIỜ RA (ComboBox) ---
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(50, 195);
            this.label2.Name = "label2";
            this.label2.Text = "Giờ ra (Chọn khung giờ):";

            this.cboGioRa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGioRa.BackColor = System.Drawing.Color.Transparent;
            this.cboGioRa.BorderRadius = 10;
            this.cboGioRa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGioRa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioRa.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboGioRa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboGioRa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.cboGioRa.ForeColor = System.Drawing.Color.Black;
            this.cboGioRa.ItemHeight = 40;
            this.cboGioRa.Location = new System.Drawing.Point(40, 220);
            this.cboGioRa.Name = "cboGioRa";
            this.cboGioRa.Size = new System.Drawing.Size(910, 46);
            this.cboGioRa.TabIndex = 3;

            // --- DỊCH VỤ ---
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(50, 280);
            this.label3.Name = "label3";
            this.label3.Text = "Dịch vụ:";

            this.dgvDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Location = new System.Drawing.Point(40, 310);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.Size = new System.Drawing.Size(680, 150);
            this.dgvDichVu.TabIndex = 7;

            // --- GỌI MÓN ---
            this.btnGoiMon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoiMon.BorderRadius = 15;
            this.btnGoiMon.FillColor = System.Drawing.Color.Orange;
            this.btnGoiMon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGoiMon.ForeColor = System.Drawing.Color.White;
            this.btnGoiMon.Location = new System.Drawing.Point(740, 310);
            this.btnGoiMon.Name = "btnGoiMon";
            this.btnGoiMon.Size = new System.Drawing.Size(210, 45);
            this.btnGoiMon.TabIndex = 8;
            this.btnGoiMon.Text = "+ GỌI MÓN";

            // --- THÀNH TIỀN ---
            this.txtThanhTien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThanhTien.BorderRadius = 10;
            this.txtThanhTien.FillColor = System.Drawing.Color.Yellow;
            this.txtThanhTien.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.txtThanhTien.ForeColor = System.Drawing.Color.Red;
            this.txtThanhTien.Location = new System.Drawing.Point(40, 480);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.ReadOnly = true;
            this.txtThanhTien.Size = new System.Drawing.Size(910, 60);
            this.txtThanhTien.TabIndex = 4;
            this.txtThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // --- BUTTONS ---
            this.btnThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThanhToan.BorderRadius = 15;
            this.btnThanhToan.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(40, 560);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(200, 55);
            this.btnThanhToan.TabIndex = 5;
            this.btnThanhToan.Text = "THANH TOÁN";

            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BorderRadius = 15;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(750, 560);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(200, 55);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "HỦY BỎ";

            // --- FORM SETUP ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(990, 636);
            this.Controls.Add(this.cboGioRa);
            this.Controls.Add(this.btnGoiMon);
            this.Controls.Add(this.dgvDichVu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.txtThanhTien);
            this.Controls.Add(this.dtpGioVao);
            this.Controls.Add(this.lblTenKH);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormThanhToan";
            this.Text = "Thanh Toán";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTenKH;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpGioVao;
        private Guna.UI2.WinForms.Guna2TextBox txtThanhTien;
        private Guna.UI2.WinForms.Guna2Button btnThanhToan;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private Guna.UI2.WinForms.Guna2Button btnGoiMon;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox cboGioRa;
    }
}