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
using static System.Collections.Specialized.BitVector32;

namespace DoAnNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            // Khuyến nghị: che mật khẩu
            guna2TextBox2.UseSystemPasswordChar = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string taiKhoan = (guna2TextBox1.Text ?? "").Trim();
            string matKhau = guna2TextBox2.Text ?? "";

            if (string.IsNullOrWhiteSpace(taiKhoan))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox2.Focus();
                return;
            }

            // 1) Authenticate + lấy thông tin NV
            string err;
            AuthUser user;
            if (!TryLogin(taiKhoan, matKhau, out user, out err))
            {
                MessageBox.Show(err, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Lưu session
            Session.SetUser(user);

            // 3) Đúng -> Loading -> TrangChu
            this.Hide();

            using (var load = new Loading())
            {
                var r = load.ShowDialog();
                if (r != DialogResult.OK)
                {
                    // Loading bị đóng bất thường -> quay lại login
                    Session.Clear();
                    this.Show();
                    return;
                }
            }

            // 4) Mở TrangChu (dialog để xử lý logout/exit gọn)
            using (var main = new TrangChu())
            {
                var rMain = main.ShowDialog();

                // Nếu nhấn Đăng xuất -> quay lại Login
                if (rMain == DialogResult.Yes)
                {
                    Session.Clear();
                    guna2TextBox2.Text = "";
                    this.Show();
                    return;
                }
            }

            // Đóng TrangChu theo kiểu bình thường -> thoát app
            this.Close();
        }


        private bool TryLogin(string taiKhoan, string matKhauNhap, out AuthUser user, out string errorMessage)
        {
            user = new AuthUser();
            errorMessage = "";

            string connStr = GetConnectionString();
            if (string.IsNullOrWhiteSpace(connStr))
            {
                errorMessage = "Thiếu ConnectionString 'QLSanBongMini' trong App.config.";
                return false;
            }

            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(@"
                        SELECT TOP 1 MaNV, TenNV, TaiKhoan, MatKhau, Quyen
                        FROM NhanVien
                        WHERE TaiKhoan = @TaiKhoan
                    ", conn))
                {
                    cmd.Parameters.Add("@TaiKhoan", SqlDbType.VarChar, 50).Value = taiKhoan;

                    conn.Open();
                    using (var r = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!r.Read())
                        {
                            errorMessage = "Tài khoản không tồn tại.";
                            return false;
                        }

                        string matKhauDb = r["MatKhau"]?.ToString() ?? "";
                        if (!string.Equals(matKhauNhap, matKhauDb, StringComparison.Ordinal))
                        {
                            errorMessage = "Mật khẩu không đúng.";
                            return false;
                        }

                        user = new AuthUser
                        {
                            MaNV = Convert.ToInt32(r["MaNV"]),
                            TenNV = r["TenNV"]?.ToString() ?? "",
                            TaiKhoan = r["TaiKhoan"]?.ToString() ?? "",
                            Quyen = r["Quyen"]?.ToString() ?? ""
                        };

                        return true;
                    }
                }
            }
            catch (SqlException)
            {
                errorMessage = "Không kết nối được CSDL hoặc truy vấn lỗi.";
                return false;
            }
            catch
            {
                errorMessage = "Có lỗi hệ thống khi xử lý đăng nhập.";
                return false;
            }
        }


        private string GetConnectionString()
        {
            try
            {
                var cs = ConfigurationManager.ConnectionStrings["QLSanBongMini"];
                return cs?.ConnectionString ?? "";
            }
            catch
            {
                return "";
            }
        }
    }
}