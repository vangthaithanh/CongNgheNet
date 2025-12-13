namespace DoAnNet
{
    partial class FormGoiDichVu
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDichVu = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numSoLuong = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuayLai = new Guna.UI2.WinForms.Guna2Button();
            this.lblGiaTien = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.Orange;
            this.lblTieuDe.Location = new System.Drawing.Point(40, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(490, 45);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "MENU ĐỒ ĂN / NƯỚC UỐNG";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(50, 110);
            this.label1.Name = "label1";
            this.label1.Text = "Chọn món:";
            // 
            // cboDichVu
            // 
            this.cboDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDichVu.BackColor = System.Drawing.Color.Transparent;
            this.cboDichVu.BorderRadius = 10;
            this.cboDichVu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDichVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDichVu.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboDichVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboDichVu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cboDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboDichVu.ItemHeight = 40;
            this.cboDichVu.Location = new System.Drawing.Point(40, 140);
            this.cboDichVu.Name = "cboDichVu";
            this.cboDichVu.Size = new System.Drawing.Size(910, 46);
            this.cboDichVu.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(50, 220);
            this.label2.Name = "label2";
            this.label2.Text = "Số lượng:";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.numSoLuong.BorderRadius = 10;
            this.numSoLuong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numSoLuong.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numSoLuong.Location = new System.Drawing.Point(40, 250);
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(910, 50);
            this.numSoLuong.TabIndex = 2;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 15;
            this.btnThem.FillColor = System.Drawing.Color.Orange;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(40, 380);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(220, 55);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "THÊM MÓN";
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.BorderRadius = 15;
            this.btnQuayLai.FillColor = System.Drawing.Color.Gray;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.Location = new System.Drawing.Point(280, 380);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(220, 55);
            this.btnQuayLai.TabIndex = 4;
            this.btnQuayLai.Text = "QUAY LẠI THANH TOÁN";
            // 
            // lblGiaTien
            // 
            this.lblGiaTien.AutoSize = true;
            this.lblGiaTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this.lblGiaTien.ForeColor = System.Drawing.Color.DimGray;
            this.lblGiaTien.Location = new System.Drawing.Point(50, 310);
            this.lblGiaTien.Name = "lblGiaTien";
            this.lblGiaTien.Size = new System.Drawing.Size(85, 21);
            this.lblGiaTien.Text = "Đơn giá: ...";
            // 
            // FormGoiDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(990, 636);
            this.Controls.Add(this.lblGiaTien);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.numSoLuong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboDichVu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormGoiDichVu";
            this.Text = "FormGoiDichVu";
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // CÁC BIẾN CÔNG KHAI ĐỂ FILE LOGIC SỬ DỤNG
        public System.Windows.Forms.Label lblTieuDe;
        public System.Windows.Forms.Label label1;
        public Guna.UI2.WinForms.Guna2ComboBox cboDichVu;
        public System.Windows.Forms.Label label2;
        public Guna.UI2.WinForms.Guna2NumericUpDown numSoLuong;
        public Guna.UI2.WinForms.Guna2Button btnThem;
        public Guna.UI2.WinForms.Guna2Button btnQuayLai;
        public System.Windows.Forms.Label lblGiaTien;
    }
}