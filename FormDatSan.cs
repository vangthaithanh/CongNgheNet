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
        private int _maKH = -1; // ID khách hàng tìm được
        string strCon = ConfigurationManager.ConnectionStrings["QLSanBongMini"].ConnectionString;

        public FormDatSan(int maSan, string tenSan)
        {
            InitializeComponent();
            _maSan = maSan;
            lblTieuDe.Text = "ĐẶT SÂN - " + tenSan.ToUpper();

            // Set giờ mặc định là hiện tại
            dtpGioBatDau.Value = DateTime.Now;

            // Gắn sự kiện
            txtSDT.TextChanged += TxtSDT_TextChanged;
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += (s, e) => this.Close();
        }

        // Tự động tìm tên khách khi nhập SĐT
        private void TxtSDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length < 9) return; // Chưa đủ số thì chưa tìm

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
                                txtTenKH.Text = "Khách mới (Vui lòng thêm KH trước)";
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (_maKH == -1)
            {
                MessageBox.Show("Chưa tìm thấy khách hàng! Vui lòng kiểm tra lại SĐT.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();

                    // 1. Thêm vào bảng DatSan
                    // Giả sử MaNV = 1 (Admin) cho nhanh, sau này bạn thay bằng Session.CurrentUser.MaNV
                    string sqlInsert = @"
                        INSERT INTO DatSan (MaKH, MaSan, MaNV, NgayDat, GioBatDau, TrangThai) 
                        VALUES (@kh, @ms, 1, @ngay, @gio, 'DangDa')";

                    using (SqlCommand cmd = new SqlCommand(sqlInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@kh", _maKH);
                        cmd.Parameters.AddWithValue("@ms", _maSan);
                        cmd.Parameters.AddWithValue("@ngay", DateTime.Now);
                        cmd.Parameters.AddWithValue("@gio", dtpGioBatDau.Value.TimeOfDay);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Cập nhật trạng thái Sân bóng -> DangDa
                    string sqlUpdate = "UPDATE SanBong SET TrangThai = 'DangDa' WHERE MaSan = @ms";
                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, con))
                    {
                        cmd.Parameters.AddWithValue("@ms", _maSan);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đặt sân thành công!");
                this.DialogResult = DialogResult.OK; // Báo cho Form cha biết là OK rồi
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
                // Tạo lại Sơ đồ sân
                FormSoDoSan frmSoDo = new FormSoDoSan();
                frmSoDo.TopLevel = false;
                frmSoDo.FormBorderStyle = FormBorderStyle.None;
                frmSoDo.Dock = DockStyle.Fill;

                // Xóa Đặt sân đi, hiện lại Sơ đồ
                pnlContainer.Controls.Clear();
                pnlContainer.Controls.Add(frmSoDo);
                frmSoDo.Show();
            }
        }
    }
}
