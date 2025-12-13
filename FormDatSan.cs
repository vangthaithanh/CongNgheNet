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
    public partial class FormDatSan : Form
    {
        private int _maSan;
        private int _maKH = -1;
        string strCon = ConfigurationManager.ConnectionStrings["QLSanBongMini"].ConnectionString;

        public FormDatSan(int maSan, string tenSan)
        {
            InitializeComponent();
            _maSan = maSan;
            lblTieuDe.Text = "ĐẶT SÂN - " + tenSan.ToUpper();

            // 1. Tự động thêm các khung giờ vào ComboBox (06:00 -> 22:30)
            LoadKhungGio();

            // Gắn sự kiện
            txtSDT.TextChanged += TxtSDT_TextChanged;
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += (s, e) => QuayVeSoDo();
        }

        private void LoadKhungGio()
        {
            cboGioBatDau.Items.Clear();
            // Tạo giờ từ 6h sáng đến 23h đêm, mỗi nấc 30 phút
            TimeSpan start = new TimeSpan(6, 0, 0);
            TimeSpan end = new TimeSpan(23, 0, 0);

            while (start <= end)
            {
                // Format hh:mm (ví dụ: 06:00, 06:30)
                cboGioBatDau.Items.Add(start.ToString(@"hh\:mm"));
                start = start.Add(new TimeSpan(0, 30, 0));
            }

            // Mặc định chọn giờ gần nhất với hiện tại
            cboGioBatDau.SelectedIndex = 0; // Hoặc logic phức tạp hơn nếu muốn
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

        private void TxtSDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length < 9) return;
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string sql = "SELECT MaKH, TenKH FROM KhachHang WHERE SDT = @sdt";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                _maKH = Convert.ToInt32(r["MaKH"]);
                                txtTenKH.Text = r["TenKH"].ToString();
                            }
                            else
                            {
                                _maKH = -1;
                                txtTenKH.Text = "Khách mới (Hãy thêm KH trước)";
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (_maKH == -1) { MessageBox.Show("Chưa có thông tin khách!"); return; }
            if (cboGioBatDau.SelectedItem == null) { MessageBox.Show("Vui lòng chọn giờ!"); return; }

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();

                    // 1. GHÉP NGÀY + GIỜ
                    DateTime ngay = dtpNgayDat.Value.Date; // Lấy phần ngày
                    TimeSpan gio = TimeSpan.Parse(cboGioBatDau.SelectedItem.ToString()); // Lấy phần giờ từ ComboBox

                    // (Tùy chọn: Nếu muốn lưu chính xác DateTime bắt đầu)
                    // DateTime thoiGianBatDau = ngay.Add(gio);

                    string sqlInsert = @"INSERT INTO DatSan (MaKH, MaSan, MaNV, NgayDat, GioBatDau, TrangThai) 
                                         VALUES (@kh, @ms, 1, @ngay, @gio, 'DangDa')";

                    using (SqlCommand cmd = new SqlCommand(sqlInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@kh", _maKH);
                        cmd.Parameters.AddWithValue("@ms", _maSan);
                        cmd.Parameters.AddWithValue("@ngay", ngay); // Lưu ngày
                        cmd.Parameters.AddWithValue("@gio", gio);   // Lưu giờ
                        cmd.ExecuteNonQuery();
                    }

                    string sqlUpdate = "UPDATE SanBong SET TrangThai = 'DangDa' WHERE MaSan = @ms";
                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, con))
                    {
                        cmd.Parameters.AddWithValue("@ms", _maSan);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Đặt sân thành công!");
                QuayVeSoDo();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}
