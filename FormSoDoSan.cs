using Guna.UI2.WinForms;
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
    public partial class FormSoDoSan : Form
    {
        string chuoiKetNoi = ConfigurationManager.ConnectionStrings["QLSanBongMini"].ConnectionString;

        public FormSoDoSan()
        {
            InitializeComponent(); 
            LoadSanBong();         
        }

        // --- HÀM LOAD DỮ LIỆU TỪ SQL ---
        public void LoadSanBong()
        {
            // flowPanelSan đã có sẵn nhờ Designer, ta chỉ cần Clear
            flowPanelSan.Controls.Clear();

            try
            {
                using (SqlConnection con = new SqlConnection(chuoiKetNoi))
                {
                    con.Open();
                    string sql = "SELECT * FROM SanBong";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        // Gọi hàm tạo nút (Tách riêng cho gọn code)
                        Control btn = CreateSanButton(row);
                        flowPanelSan.Controls.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu sân: " + ex.Message);
            }
        }

        // --- HÀM TẠO NÚT SÂN (Logic hiển thị từng sân) ---
        private Control CreateSanButton(DataRow row)
        {
            Guna2GradientButton btn = new Guna2GradientButton();

            // 1. Lấy thông tin
            int maSan = Convert.ToInt32(row["MaSan"]);
            string tenSan = row["TenSan"].ToString();
            string trangThai = row["TrangThai"].ToString();

            // 2. Trang trí nút (Style)
            btn.Size = new Size(180, 180);
            btn.BorderRadius = 25;
            btn.Margin = new Padding(15);
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn.ForeColor = Color.White;

            // Text hiển thị
            btn.Text = $"{tenSan}\n\n{LayTrangThaiText(trangThai)}";

            // Icon (Nếu có)
            try
            {
                btn.Image = Properties.Resources.logo2;
                btn.ImageSize = new Size(55, 55);
                btn.ImageOffset = new Point(0, -15);
            }
            catch { }

            // Đổ bóng
            btn.ShadowDecoration.Enabled = true;
            btn.ShadowDecoration.Depth = 10;
            btn.ShadowDecoration.Color = Color.Gray;

            // 3. Xử lý màu sắc Gradient theo trạng thái
            if (trangThai == "Trong") // Xanh
            {
                btn.FillColor = Color.FromArgb(46, 204, 113);
                btn.FillColor2 = Color.FromArgb(39, 174, 96);
            }
            else if (trangThai == "DangDa") // Đỏ
            {
                btn.FillColor = Color.FromArgb(231, 76, 60);
                btn.FillColor2 = Color.FromArgb(192, 57, 43);
            }
            else // Xám
            {
                btn.FillColor = Color.FromArgb(149, 165, 166);
                btn.FillColor2 = Color.FromArgb(127, 140, 141);
            }

            // 4. GẮN SỰ KIỆN CLICK (Xử lý nghiệp vụ tại đây)
            btn.Click += (s, e) =>
            {
                XuLyClickSan(maSan, tenSan, trangThai);
            };

            return btn;
        }

        // --- HÀM XỬ LÝ LOGIC KHI BẤM VÀO SÂN ---
        // ... (Các using giữ nguyên) ...

        // Thay thế hàm XuLyClickSan cũ bằng hàm này:
        private void XuLyClickSan(int maSan, string tenSan, string trangThai)
        {
            // Lấy cái Panel đang chứa form này (chính là guna2Panel3 của TrangChu)
            Panel pnlContainer = this.Parent as Panel;

            if (pnlContainer != null)
            {
                Form formMoi = null;

                if (trangThai == "Trong")
                {
                    formMoi = new FormDatSan(maSan, tenSan);
                }
                else if (trangThai == "DangDa")
                {
                    formMoi = new FormThanhToan(maSan, tenSan);
                }
                else
                {
                    MessageBox.Show("Sân đang bảo trì!");
                    return;
                }

                if (formMoi != null)
                {
                    // Setup form con để nhúng (Embed)
                    formMoi.TopLevel = false;
                    formMoi.FormBorderStyle = FormBorderStyle.None;
                    formMoi.Dock = DockStyle.Fill; // Tự lấp đầy kích thước 990x636

                    // Xóa Sơ đồ sân đi, thêm Form mới vào
                    pnlContainer.Controls.Clear();
                    pnlContainer.Controls.Add(formMoi);
                    pnlContainer.Tag = formMoi;
                    formMoi.Show();
                }
            }
        }

        // --- HÀM PHỤ TRỢ ---
        private string LayTrangThaiText(string status)
        {
            if (status == "Trong") return "(TRỐNG)";
            if (status == "DangDa") return "(ĐANG ĐÁ)";
            return "(BẢO TRÌ)";
        }
    }
}
