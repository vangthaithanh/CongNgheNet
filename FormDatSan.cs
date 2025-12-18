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

            LoadKhungGio(); // Tạo giờ
            LoadDSKhachHang(); // 1. Tải danh sách khách

            // Gắn sự kiện
            txtSDT.TextChanged += TxtSDT_TextChanged;
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += (s, e) => QuayVeSoDo();

            // 2. Sự kiện khi click vào bảng khách hàng
            dgvKhachHang.CellClick += DgvKhachHang_CellClick;
        }

        // --- HÀM MỚI: TẢI DANH SÁCH KHÁCH HÀNG ---
        private void LoadDSKhachHang()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    // Lấy Mã, Tên, SĐT
                    SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 50 MaKH, TenKH, SDT FROM KhachHang ORDER BY MaKH DESC", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKhachHang.DataSource = dt;

                    // Ẩn cột Mã KH cho gọn (nếu muốn)
                    if (dgvKhachHang.Columns["MaKH"] != null) dgvKhachHang.Columns["MaKH"].Visible = false;
                }
            }
            catch { }
        }

        // --- HÀM MỚI: CHỌN TỪ BẢNG TỰ ĐIỀN VÀO Ô ---
        private void DgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không click vào tiêu đề
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                // Lấy dữ liệu từ dòng được chọn
                string sdt = row.Cells["SDT"].Value.ToString();
                string ten = row.Cells["TenKH"].Value.ToString();
                int ma = Convert.ToInt32(row.Cells["MaKH"].Value);

                // Điền ngược lên ô nhập liệu
                txtSDT.Text = sdt;
                txtTenKH.Text = ten;
                _maKH = ma; // Lưu luôn mã để lát khỏi tìm lại
            }
        }

        private void LoadKhungGio()
        {
            cboGioBatDau.Items.Clear();
            TimeSpan start = new TimeSpan(6, 0, 0);
            TimeSpan end = new TimeSpan(23, 0, 0);
            while (start <= end)
            {
                cboGioBatDau.Items.Add(start.ToString(@"hh\:mm"));
                start = start.Add(new TimeSpan(0, 30, 0));
            }
            cboGioBatDau.SelectedIndex = 0;
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
            // Logic tìm kiếm cũ vẫn giữ nguyên để hỗ trợ gõ tay
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
                                // Nếu gõ tay không thấy thì cho phép nhập mới
                                // (Không reset text để người dùng tự nhập)
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSDT.Text)) { MessageBox.Show("Vui lòng nhập SĐT!"); return; }
            if (string.IsNullOrWhiteSpace(txtTenKH.Text)) { MessageBox.Show("Vui lòng nhập Tên Khách Hàng!"); return; }
            if (cboGioBatDau.SelectedItem == null) { MessageBox.Show("Vui lòng chọn giờ!"); return; }

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();

                    // LOGIC THÊM KHÁCH MỚI (Nếu _maKH = -1)
                    if (_maKH == -1)
                    {
                        string sqlAddKH = @"INSERT INTO KhachHang (TenKH, SDT) VALUES (@ten, @sdt); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmdKH = new SqlCommand(sqlAddKH, con))
                        {
                            cmdKH.Parameters.AddWithValue("@ten", txtTenKH.Text);
                            cmdKH.Parameters.AddWithValue("@sdt", txtSDT.Text);
                            _maKH = Convert.ToInt32(cmdKH.ExecuteScalar());
                        }
                    }

                    DateTime ngay = dtpNgayDat.Value.Date;
                    TimeSpan gio = TimeSpan.Parse(cboGioBatDau.SelectedItem.ToString());

                    string sqlInsert = @"INSERT INTO DatSan (MaKH, MaSan, MaNV, NgayDat, GioBatDau, TrangThai) VALUES (@kh, @ms, 1, @ngay, @gio, 'DangDa')";
                    using (SqlCommand cmd = new SqlCommand(sqlInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@kh", _maKH);
                        cmd.Parameters.AddWithValue("@ms", _maSan);
                        cmd.Parameters.AddWithValue("@ngay", ngay);
                        cmd.Parameters.AddWithValue("@gio", gio);
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
