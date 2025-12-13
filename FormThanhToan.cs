using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnNet
{
    public partial class FormThanhToan : Form
    {
        private int _maSan;
        private int _maDatSan;
        private decimal _giaMoiGio = 200000; // Giá tiền demo (nên lấy từ bảng LoaiSan)
        string strCon = ConfigurationManager.ConnectionStrings["QLSanBongMini"].ConnectionString;

        public FormThanhToan(int maSan, string tenSan)
        {
            InitializeComponent();
            _maSan = maSan;
            lblTieuDe.Text = "THANH TOÁN - " + tenSan.ToUpper();

            this.Load += FormThanhToan_Load;
            dtpGioRa.ValueChanged += (s, e) => TinhTien();
            btnThanhToan.Click += BtnThanhToan_Click;
            btnHuy.Click += (s, e) => this.Close();
        }

        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    // Lấy thông tin đặt sân đang 'DangDa'
                    string sql = @"
                        SELECT d.MaDatSan, k.TenKH, d.GioBatDau, d.NgayDat 
                        FROM DatSan d
                        JOIN KhachHang k ON d.MaKH = k.MaKH
                        WHERE d.MaSan = @ms AND d.TrangThai = 'DangDa'";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@ms", _maSan);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                _maDatSan = Convert.ToInt32(r["MaDatSan"]);
                                lblTenKH.Text = "Khách hàng: " + r["TenKH"].ToString();

                                // Xử lý ngày giờ
                                DateTime ngayDat = Convert.ToDateTime(r["NgayDat"]);
                                TimeSpan gioBatDau = (TimeSpan)r["GioBatDau"];

                                dtpGioVao.Value = ngayDat.Date.Add(gioBatDau);
                                dtpGioRa.Value = DateTime.Now; // Giờ ra mặc định là hiện tại

                                TinhTien();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void TinhTien()
        {
            DateTime vao = dtpGioVao.Value;
            DateTime ra = dtpGioRa.Value;

            if (ra < vao)
            {
                txtThanhTien.Text = "Giờ ra không hợp lệ";
                return;
            }

            TimeSpan thoiGianDa = ra - vao;
            double soGio = Math.Max(0.5, thoiGianDa.TotalHours); // Tối thiểu tính 30 phút
            decimal tongTien = (decimal)soGio * _giaMoiGio;

            txtThanhTien.Text = tongTien.ToString("#,##0") + " VNĐ";
            txtThanhTien.Tag = tongTien; // Lưu giá trị số để insert vào DB
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận thanh toán?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();

                    // 1. Cập nhật DatSan (Giờ kết thúc, Tiền, Trạng thái)
                    string sqlUpdateDat = @"
                        UPDATE DatSan 
                        SET GioKetThuc = @ra, TienSan = @tien, TrangThai = 'DaThanhToan'
                        WHERE MaDatSan = @md";

                    using (SqlCommand cmd = new SqlCommand(sqlUpdateDat, con))
                    {
                        cmd.Parameters.AddWithValue("@ra", dtpGioRa.Value.TimeOfDay);

                        decimal tien = 0;
                        if (txtThanhTien.Tag != null) tien = Convert.ToDecimal(txtThanhTien.Tag);
                        cmd.Parameters.AddWithValue("@tien", tien);

                        cmd.Parameters.AddWithValue("@md", _maDatSan);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Trả sân về 'Trong'
                    string sqlUpdateSan = "UPDATE SanBong SET TrangThai = 'Trong' WHERE MaSan = @ms";
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateSan, con))
                    {
                        cmd.Parameters.AddWithValue("@ms", _maSan);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Thanh toán thành công!");
                this.DialogResult = DialogResult.OK; // Báo cho cha biết để reload màu sân
                QuayVeSoDo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void QuayVeSoDo()
        {
            Panel pnlContainer = this.Parent as Panel;
            if (pnlContainer != null)
            {
                FormSoDoSan frmSoDo = new FormSoDoSan();
                frmSoDo.TopLevel = false;
                frmSoDo.FormBorderStyle = FormBorderStyle.None;
                frmSoDo.Dock = DockStyle.Fill;

                pnlContainer.Controls.Clear();
                pnlContainer.Controls.Add(frmSoDo);
                frmSoDo.Show();
            }
        }

    }
}
