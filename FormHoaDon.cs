using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace DoAnNet
{
    public partial class FormHoaDon : Form
    {
        private int _maDatSan;
        string strCon = ConfigurationManager.ConnectionStrings["QLSanBongMini"].ConnectionString;

        // Biến lưu dữ liệu để in
        private string _tenKH_Print = "";
        private string _ngay_Print = "";
        private string _gioRa_Print = "";
        private decimal _tongTien_Print = 0;
        private DataTable _dtChiTiet_Print = new DataTable();

        public FormHoaDon(int maDatSan)
        {
            InitializeComponent();
            _maDatSan = maDatSan;
            
            this.Load += FormHoaDon_Load;
            btnDong.Click += (s, e) => this.Close();
            
            // Gắn sự kiện cho nút IN
            btnIn.Click += BtnIn_Click;
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();

                    // 1. Lấy thông tin chung
                    string sqlInfo = @"SELECT k.TenKH, d.NgayDat, d.GioBatDau, d.GioKetThuc, d.TienSan
                                       FROM DatSan d JOIN KhachHang k ON d.MaKH = k.MaKH
                                       WHERE d.MaDatSan = @mds";
                    SqlCommand cmd = new SqlCommand(sqlInfo, con);
                    cmd.Parameters.AddWithValue("@mds", _maDatSan);
                    SqlDataReader r = cmd.ExecuteReader();
                    
                    decimal tienSan = 0;
                    if (r.Read())
                    {
                        _tenKH_Print = r["TenKH"].ToString();
                        _ngay_Print = Convert.ToDateTime(r["NgayDat"]).ToString("dd/MM/yyyy");
                        _gioRa_Print = DateTime.Now.ToString("HH:mm");
                        tienSan = Convert.ToDecimal(r["TienSan"]);

                        lblKhachHang.Text = "Khách hàng: " + _tenKH_Print;
                        lblNgay.Text = "Ngày: " + _ngay_Print + " - Giờ in: " + _gioRa_Print;
                    }
                    r.Close();

                    // 2. Lấy dịch vụ
                    string sqlDV = @"SELECT dv.TenDV, c.SoLuong, dv.DonGia, c.ThanhTien 
                                     FROM ChiTietDichVu c JOIN DichVu dv ON c.MaDV = dv.MaDV 
                                     WHERE c.MaDatSan = @mds";
                    SqlDataAdapter da = new SqlDataAdapter(sqlDV, con);
                    da.SelectCommand.Parameters.AddWithValue("@mds", _maDatSan);
                    _dtChiTiet_Print = new DataTable();
                    da.Fill(_dtChiTiet_Print);

                    // 3. Chèn Tiền Sân vào bảng dịch vụ để hiển thị chung
                    DataRow rowTienSan = _dtChiTiet_Print.NewRow();
                    rowTienSan["TenDV"] = "Tiền Giờ Đá";
                    rowTienSan["SoLuong"] = 1;
                    rowTienSan["DonGia"] = tienSan;
                    rowTienSan["ThanhTien"] = tienSan;
                    _dtChiTiet_Print.Rows.InsertAt(rowTienSan, 0);

                    // 4. Hiển thị lên GridView
                    dgvBill.DataSource = _dtChiTiet_Print;
                    dgvBill.Columns["TenDV"].HeaderText = "Mặt Hàng";
                    dgvBill.Columns["SoLuong"].HeaderText = "SL";
                    dgvBill.Columns["DonGia"].HeaderText = "Đ.Giá";
                    dgvBill.Columns["ThanhTien"].HeaderText = "T.Tiền";
                    
                    dgvBill.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                    dgvBill.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

                    // 5. Tính tổng
                    object sum = _dtChiTiet_Print.Compute("Sum(ThanhTien)", "");
                    _tongTien_Print = (sum == DBNull.Value) ? 0 : Convert.ToDecimal(sum);
                    lblTongTien.Text = "TỔNG: " + _tongTien_Print.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load: " + ex.Message); }
        }

        // --- XỬ LÝ IN ẤN ---
        private void BtnIn_Click(object sender, EventArgs e)
        {
            // Cấu hình trang in
            PrintDocument pdoc = new PrintDocument();
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            // Mở hộp thoại xem trước (Preview)
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pdoc;
            dlg.Height = 800; 
            dlg.Width = 600;
            dlg.StartPosition = FormStartPosition.CenterScreen;
            dlg.ShowDialog();
        }

        // Hàm "Vẽ" nội dung lên giấy
        // Thay thế hàm này trong FormHoaDon.cs
        private void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // 1. CẤU HÌNH FONT VÀ MÀU SẮC
            Font fontTieuDe = new Font("Arial", 22, FontStyle.Bold);
            Font fontSubTitle = new Font("Arial", 11, FontStyle.Regular);
            Font fontHeaderTable = new Font("Arial", 10, FontStyle.Bold);
            Font fontItem = new Font("Arial", 10, FontStyle.Regular);
            Font fontTongTien = new Font("Arial", 14, FontStyle.Bold);

            Brush brushText = Brushes.Black;
            Pen penDash = new Pen(Color.Gray) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }; // Nét đứt
            Pen penSolid = new Pen(Color.Black, 1); // Nét liền

            // 2. THIẾT LẬP KHUNG GIẤY (Lề trang in)
            int margin = 50;
            int width = e.PageBounds.Width - 2 * margin;
            int startY = 50;
            int currentY = startY;

            // Các điểm Tab căn lề (Cột)
            // Cột 1: Tên (x=50), Cột 2: SL (x=350), Cột 3: Giá (x=450), Cột 4: Tiền (x=600)
            int col1 = margin;
            int col2 = margin + 300;
            int col3 = margin + 400;
            int col4 = margin + 550;

            // Định dạng căn lề
            StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far }; // Căn phải cho số tiền
            StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near };

            // ================= BẮT ĐẦU VẼ =================

            // 3. LOGO & HEADER
            // Vẽ khung bao quanh hóa đơn
            g.DrawRectangle(penSolid, margin - 10, startY - 10, width + 20, 800);

            // Tên quán
            g.DrawString("SÂN BÓNG MINI", fontTieuDe, Brushes.ForestGreen, e.PageBounds.Width / 2, currentY, centerFormat);
            currentY += 40;

            // Địa chỉ & Hotline
            g.DrawString("123 Đường Số 1, Q.Bình Thạnh, TP.HCM", fontSubTitle, brushText, e.PageBounds.Width / 2, currentY, centerFormat);
            currentY += 20;
            g.DrawString("Hotline: 0909.123.456", fontSubTitle, brushText, e.PageBounds.Width / 2, currentY, centerFormat);
            currentY += 40;

            // Tiêu đề hóa đơn
            g.DrawString("HÓA ĐƠN THANH TOÁN", new Font("Arial", 16, FontStyle.Bold), Brushes.OrangeRed, e.PageBounds.Width / 2, currentY, centerFormat);
            currentY += 40;

            // 4. THÔNG TIN KHÁCH HÀNG
            g.DrawString("Ngày: " + _ngay_Print + " " + _gioRa_Print, fontItem, brushText, col1, currentY);
            currentY += 25;
            g.DrawString("Khách hàng: " + _tenKH_Print, fontItem, brushText, col1, currentY);
            currentY += 30;

            // 5. VẼ TIÊU ĐỀ BẢNG (Có nền màu xám cho đẹp)
            g.FillRectangle(Brushes.LightGray, col1, currentY, width, 30); // Tô nền
            g.DrawRectangle(penSolid, col1, currentY, width, 30); // Viền bảng

            // Chữ tiêu đề
            float textY = currentY + 7;
            g.DrawString("TÊN DỊCH VỤ", fontHeaderTable, brushText, col1 + 5, textY);
            g.DrawString("SL", fontHeaderTable, brushText, col2, textY);
            g.DrawString("ĐƠN GIÁ", fontHeaderTable, brushText, col3 + 50, textY, rightFormat);
            g.DrawString("THÀNH TIỀN", fontHeaderTable, brushText, col4 + 100, textY, rightFormat);
            currentY += 40;

            // 6. DANH SÁCH MÓN ĂN
            foreach (DataRow row in _dtChiTiet_Print.Rows)
            {
                string ten = row["TenDV"].ToString();
                // Cắt tên nếu dài quá
                if (ten.Length > 30) ten = ten.Substring(0, 30) + "...";

                string sl = row["SoLuong"].ToString();
                string gia = string.Format("{0:N0}", row["DonGia"]);
                string tien = string.Format("{0:N0}", row["ThanhTien"]);

                g.DrawString(ten, fontItem, brushText, col1 + 5, currentY);
                g.DrawString(sl, fontItem, brushText, col2, currentY);
                g.DrawString(gia, fontItem, brushText, col3 + 50, currentY, rightFormat);
                g.DrawString(tien, fontItem, brushText, col4 + 100, currentY, rightFormat);

                currentY += 25;
                // Vẽ đường gạch đứt bên dưới mỗi món
                g.DrawLine(penDash, col1, currentY, col1 + width, currentY);
                currentY += 10;
            }

            // 7. TỔNG TIỀN (Vẽ khung tổng tiền)
            currentY += 10;
            g.DrawLine(penSolid, col1, currentY, col1 + width, currentY); // Đường chốt sổ
            currentY += 10;

            g.DrawString("TỔNG CỘNG:", fontHeaderTable, brushText, col3, currentY);
            g.DrawString(string.Format("{0:N0} VNĐ", _tongTien_Print), fontTongTien, Brushes.Red, col4 + 100, currentY - 5, rightFormat);

            // 8. LỜI CẢM ƠN (Chân trang)
            currentY += 60;
            g.DrawString("Cảm ơn quý khách & Hẹn gặp lại!", new Font("Arial", 10, FontStyle.Italic), Brushes.DimGray, e.PageBounds.Width / 2, currentY, centerFormat);
            currentY += 20;
            g.DrawString("(Mọi thắc mắc xin liên hệ quản lý sân)", new Font("Arial", 8, FontStyle.Regular), Brushes.DimGray, e.PageBounds.Width / 2, currentY, centerFormat);
        }
    }
}
