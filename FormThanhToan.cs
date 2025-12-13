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
using System.Globalization;

namespace DoAnNet
{
    public partial class FormThanhToan : Form
    {
        private int _maSan;
        private int _maDatSan;
        private decimal _giaSanMoiGio = 0;
        private decimal _tongTienDichVu = 0;

        string strCon = ConfigurationManager.ConnectionStrings["QLSanBongMini"].ConnectionString;

        public FormThanhToan(int maSan, string tenSan)
        {
            InitializeComponent();
            _maSan = maSan;
            if (lblTieuDe != null) lblTieuDe.Text = "THANH TOÁN - " + tenSan.ToUpper();

            this.Load += FormThanhToan_Load;

            // Sự kiện
            if (cboGioRa != null) cboGioRa.SelectedIndexChanged += (s, e) => TinhTongTien();
            if (btnThanhToan != null) btnThanhToan.Click += BtnThanhToan_Click;
            if (btnHuy != null) btnHuy.Click += (s, e) => QuayVeSoDo();
            if (btnGoiMon != null) btnGoiMon.Click += BtnGoiMon_Click;
        }

        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string sql = @"
                        SELECT ds.MaDatSan, ds.GioBatDau, ds.NgayDat, ls.DonGia, kh.TenKH
                        FROM DatSan ds
                        JOIN SanBong sb ON ds.MaSan = sb.MaSan
                        JOIN LoaiSan ls ON sb.MaLoai = ls.MaLoai
                        JOIN KhachHang kh ON ds.MaKH = kh.MaKH
                        WHERE ds.MaSan = @ms AND ds.TrangThai = 'DangDa'";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ms", _maSan);
                    SqlDataReader r = cmd.ExecuteReader();

                    if (r.Read())
                    {
                        _maDatSan = Convert.ToInt32(r["MaDatSan"]);
                        lblTenKH.Text = "Khách hàng: " + r["TenKH"].ToString();
                        _giaSanMoiGio = Convert.ToDecimal(r["DonGia"]);

                        DateTime ngayDat = Convert.ToDateTime(r["NgayDat"]);
                        TimeSpan gioBatDau = (TimeSpan)r["GioBatDau"];

                        // Hiển thị giờ vào
                        dtpGioVao.Value = ngayDat.Date.Add(gioBatDau);

                        // TẠO LIST GIỜ RA (An toàn tuyệt đối)
                        LoadKhungGioRa(gioBatDau);
                    }
                    r.Close();
                }
                LoadDichVu();
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load: " + ex.Message);
            }
        }

        // --- HÀM TẠO LIST GIỜ RA (Đã tối ưu không parse ngược lại để tránh lỗi) ---
        private void LoadKhungGioRa(TimeSpan gioBatDau)
        {
            cboGioRa.Items.Clear();

            TimeSpan current = gioBatDau.Add(new TimeSpan(0, 30, 0)); // Bắt đầu sau 30p
            TimeSpan endOfDay = new TimeSpan(23, 59, 0); // Giới hạn cuối ngày

            // Tìm giờ hiện tại để auto-select
            TimeSpan now = DateTime.Now.TimeOfDay;
            int bestIndex = 0;
            double minDiff = double.MaxValue;
            int currentIndex = 0;

            // Chạy vòng lặp tạo giờ: 17:30, 18:00, 18:30...
            // Điều kiện: current <= 24h + 6h sáng hôm sau (để tính đá qua đêm nếu cần)
            // Ở đây mình giới hạn tới cuối ngày hoặc +1 ngày tùy logic của bạn. 
            // Tạm thời giới hạn đến 23:30 cùng ngày cho đơn giản.
            while (current.TotalHours < 24)
            {
                // Format cố định HH:mm để hiển thị đẹp
                string timeString = current.ToString(@"hh\:mm");
                cboGioRa.Items.Add(timeString);

                // Tính toán khoảng cách thời gian để tìm giờ gần nhất
                double diff = Math.Abs((current - now).TotalMinutes);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    bestIndex = currentIndex;
                }

                current = current.Add(new TimeSpan(0, 30, 0));
                currentIndex++;

                // Break nếu quá trễ (ví dụ 3h sáng hôm sau)
                if (current.TotalHours > 28) break;
            }

            // Chọn giờ gần nhất
            if (cboGioRa.Items.Count > 0)
            {
                if (bestIndex >= 0 && bestIndex < cboGioRa.Items.Count)
                    cboGioRa.SelectedIndex = bestIndex;
                else
                    cboGioRa.SelectedIndex = 0;
            }
        }

        // --- TÍNH TIỀN (Sử dụng ParseExact để không bao giờ lỗi Format) ---
        private void TinhTongTien()
        {
            if (cboGioRa.SelectedItem == null) return;

            try
            {
                DateTime vao = dtpGioVao.Value;

                // Lấy chuỗi giờ từ ComboBox
                string gioRaStr = cboGioRa.SelectedItem.ToString();

                // QUAN TRỌNG: Dùng ParseExact để ép kiểu theo định dạng mong muốn, bất chấp máy tính cài gì
                // Chấp nhận cả "HH:mm" (24h) và "h:mm" (số đơn)
                TimeSpan gioRaDaChon = TimeSpan.ParseExact(gioRaStr, new[] { "hh\\:mm", "h\\:mm" }, CultureInfo.InvariantCulture);

                DateTime ra = vao.Date.Add(gioRaDaChon);

                // Nếu giờ ra nhỏ hơn giờ vào (ví dụ Vào 23:00, Ra 00:30) -> Hiểu là sang ngày hôm sau
                if (ra < vao) ra = ra.AddDays(1);

                TimeSpan thoiGianDa = ra - vao;
                double soGio = Math.Max(0.5, thoiGianDa.TotalHours);

                decimal tienSan = (decimal)soGio * _giaSanMoiGio;
                decimal tongCong = tienSan + _tongTienDichVu;

                txtThanhTien.Text = string.Format("{0:N0} VNĐ", tongCong);
                txtThanhTien.Tag = tongCong;
            }
            catch
            {
                txtThanhTien.Text = "Chọn giờ lỗi";
                txtThanhTien.Tag = 0;
            }
        }

        private void LoadDichVu()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string sql = @"SELECT dv.TenDV, c.SoLuong, dv.DonGia, c.ThanhTien
                                   FROM ChiTietDichVu c
                                   JOIN DichVu dv ON c.MaDV = dv.MaDV
                                   WHERE c.MaDatSan = @mds";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    da.SelectCommand.Parameters.AddWithValue("@mds", _maDatSan);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dgvDichVu != null) dgvDichVu.DataSource = dt;

                    object sum = dt.Compute("Sum(ThanhTien)", "");
                    _tongTienDichVu = (sum == DBNull.Value) ? 0 : Convert.ToDecimal(sum);
                }
            }
            catch { }
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận thanh toán?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No) return;
            if (cboGioRa.SelectedItem == null) { MessageBox.Show("Chưa chọn giờ ra!"); return; }

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    decimal tongTien = Convert.ToDecimal(txtThanhTien.Tag);

                    string gioRaStr = cboGioRa.SelectedItem.ToString();
                    // Parse chuẩn để lưu vào SQL
                    TimeSpan gioRa = TimeSpan.ParseExact(gioRaStr, new[] { "hh\\:mm", "h\\:mm" }, CultureInfo.InvariantCulture);

                    string sqlUpdate = @"UPDATE DatSan SET GioKetThuc = @ra, TienSan = @tien, TrangThai = 'DaThanhToan' WHERE MaDatSan = @mds";
                    SqlCommand cmdUp = new SqlCommand(sqlUpdate, con);
                    cmdUp.Parameters.AddWithValue("@ra", gioRa);
                    cmdUp.Parameters.AddWithValue("@tien", tongTien);
                    cmdUp.Parameters.AddWithValue("@mds", _maDatSan);
                    cmdUp.ExecuteNonQuery();

                    string sqlHD = @"INSERT INTO HoaDon (MaDatSan, NgayLap, TongTien, PhuongThucThanhToan) VALUES (@mds, GETDATE(), @tong, N'Tiền mặt')";
                    SqlCommand cmdHD = new SqlCommand(sqlHD, con);
                    cmdHD.Parameters.AddWithValue("@mds", _maDatSan);
                    cmdHD.Parameters.AddWithValue("@tong", tongTien);
                    cmdHD.ExecuteNonQuery();

                    string sqlSan = "UPDATE SanBong SET TrangThai = 'Trong' WHERE MaSan = @ms";
                    SqlCommand cmdSan = new SqlCommand(sqlSan, con);
                    cmdSan.Parameters.AddWithValue("@ms", _maSan);
                    cmdSan.ExecuteNonQuery();
                }
                MessageBox.Show("Thanh toán thành công!");
                QuayVeSoDo();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thanh toán: " + ex.Message); }
        }

        private void QuayVeSoDo()
        {
            Panel pnl = this.Parent as Panel;
            if (pnl != null)
            {
                FormSoDoSan frm = new FormSoDoSan();
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                pnl.Controls.Clear();
                pnl.Controls.Add(frm);
                frm.Show();
            }
        }

        private void BtnGoiMon_Click(object sender, EventArgs e)
        {
            Panel pnl = this.Parent as Panel;
            if (pnl != null)
            {
                FormGoiDichVu frm = new FormGoiDichVu(_maDatSan);
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                pnl.Controls.Clear();
                pnl.Controls.Add(frm);
                frm.Show();
            }
        }
    }
}
