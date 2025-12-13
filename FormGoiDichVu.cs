using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Configuration;
using System.Data.SqlClient;


namespace DoAnNet
{
    public partial class FormGoiDichVu : Form
    {
        private int _maDatSan;

        // Lấy chuỗi kết nối
        string strCon = ConfigurationManager.ConnectionStrings["QLSanBongMini"].ConnectionString;

        // Constructor chỉ cần nhận MaDatSan
        public FormGoiDichVu(int maDatSan)
        {
            InitializeComponent();
            _maDatSan = maDatSan;

            LoadMenu(); // Load dữ liệu khi mở form

            // Đăng ký sự kiện
            cboDichVu.SelectedIndexChanged += CboDichVu_SelectedIndexChanged;
            btnThem.Click += BtnThem_Click;
            btnQuayLai.Click += BtnQuayLai_Click;
        }

        // --- 1. TẢI MENU TỪ SQL ---
        private void LoadMenu()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    // Load tên và giá
                    SqlDataAdapter da = new SqlDataAdapter("SELECT MaDV, TenDV, DonGia, DonViTinh FROM DichVu", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboDichVu.DataSource = dt;
                    cboDichVu.DisplayMember = "TenDV";
                    cboDichVu.ValueMember = "MaDV";

                    // Update giá cho món đầu tiên
                    if (cboDichVu.Items.Count > 0) UpdateGiaTien();
                }
            }
            catch { }
        }

        // --- 2. HIỂN THỊ GIÁ ---
        private void CboDichVu_SelectedIndexChanged(object sender, EventArgs e) => UpdateGiaTien();

        private void UpdateGiaTien()
        {
            if (cboDichVu.SelectedItem is DataRowView row)
            {
                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                lblGiaTien.Text = $"Đơn giá: {donGia:N0} VNĐ / {row["DonViTinh"]}";
            }
        }

        // --- 3. NÚT THÊM MÓN ---
        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (cboDichVu.SelectedValue == null) return;

            try
            {
                int maDV = Convert.ToInt32(cboDichVu.SelectedValue);
                int soLuongThem = (int)numSoLuong.Value;

                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();

                    // Lấy giá hiện tại từ DB
                    SqlCommand cmdGia = new SqlCommand("SELECT DonGia FROM DichVu WHERE MaDV=@mdv", con);
                    cmdGia.Parameters.AddWithValue("@mdv", maDV);
                    decimal donGia = Convert.ToDecimal(cmdGia.ExecuteScalar());

                    // Kiểm tra món đã có chưa?
                    string sqlCheck = "SELECT COUNT(*) FROM ChiTietDichVu WHERE MaDatSan=@mds AND MaDV=@mdv";
                    SqlCommand cmdCheck = new SqlCommand(sqlCheck, con);
                    cmdCheck.Parameters.AddWithValue("@mds", _maDatSan);
                    cmdCheck.Parameters.AddWithValue("@mdv", maDV);

                    if ((int)cmdCheck.ExecuteScalar() > 0)
                    {
                        // Đã có -> Cộng dồn (UPDATE)
                        string sqlUp = "UPDATE ChiTietDichVu SET SoLuong=SoLuong+@sl, ThanhTien=ThanhTien+(@sl*@dg) WHERE MaDatSan=@mds AND MaDV=@mdv";
                        SqlCommand cmd = new SqlCommand(sqlUp, con);
                        cmd.Parameters.AddWithValue("@sl", soLuongThem);
                        cmd.Parameters.AddWithValue("@dg", donGia);
                        cmd.Parameters.AddWithValue("@mds", _maDatSan);
                        cmd.Parameters.AddWithValue("@mdv", maDV);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Chưa có -> Thêm mới (INSERT)
                        string sqlIn = "INSERT INTO ChiTietDichVu (MaDatSan, MaDV, SoLuong, ThanhTien) VALUES (@mds, @mdv, @sl, @tt)";
                        SqlCommand cmd = new SqlCommand(sqlIn, con);
                        cmd.Parameters.AddWithValue("@mds", _maDatSan);
                        cmd.Parameters.AddWithValue("@mdv", maDV);
                        cmd.Parameters.AddWithValue("@sl", soLuongThem);
                        cmd.Parameters.AddWithValue("@tt", donGia * soLuongThem);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Đã thêm món thành công!", "Thông báo");
                numSoLuong.Value = 1; // Reset số lượng về 1
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- 4. NÚT QUAY LẠI (TỰ TÌM ĐƯỜNG VỀ) ---
        private void BtnQuayLai_Click(object sender, EventArgs e)
        {
            try
            {
                int maSan = 0;
                string tenSan = "";

                // Tự kết nối SQL để tìm thông tin Sân dựa vào MaDatSan
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    string sql = @"SELECT s.MaSan, s.TenSan 
                                   FROM DatSan d 
                                   JOIN SanBong s ON d.MaSan = s.MaSan 
                                   WHERE d.MaDatSan = @mds";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@mds", _maDatSan);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            maSan = Convert.ToInt32(r["MaSan"]);
                            tenSan = r["TenSan"].ToString();
                        }
                    }
                }

                // Tìm Panel cha để đổi Form
                Panel pnlContainer = this.Parent as Panel;
                if (pnlContainer != null)
                {
                    // Tạo lại Form Thanh Toán (đúng tham số nó cần)
                    FormThanhToan frm = new FormThanhToan(maSan, tenSan);
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    frm.Dock = DockStyle.Fill;

                    pnlContainer.Controls.Clear();
                    pnlContainer.Controls.Add(frm);
                    frm.Show();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi quay lại: " + ex.Message); }
        }
    }
}
