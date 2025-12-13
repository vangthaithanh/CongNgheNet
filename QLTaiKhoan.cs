using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnNet
{
    public partial class QLTaiKhoan : Form
    {
        // Lấy chuỗi kết nối từ App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QLSanBongMini"].ConnectionString;
        private int selectedMaNV = -1; // Biến lưu MaNV được chọn

        public QLTaiKhoan()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadDataTaiKhoan();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            guna2DataGridView1.CellClick += guna2DataGridView1_CellClick;
        }

        private void SetupDataGridView()
        {
            // Thiết lập ComboBox Quyền
            cboQuyen.Items.Add("Admin");
            cboQuyen.Items.Add("NhanVien");
            cboQuyen.SelectedIndex = 0;

            // Thiết lập DataGridView
            guna2DataGridView1.AutoGenerateColumns = false;
            guna2DataGridView1.Columns.Clear();

            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "MaNV", HeaderText = "Mã NV", Name = "colMaNV", Visible = false });
            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "TenNV", HeaderText = "Tên người dùng", Name = "colTenNV", Width = 180 });
            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "TaiKhoan", HeaderText = "Tên tài khoản", Name = "colTaiKhoan", Width = 180 });
            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "MatKhau", HeaderText = "Mật khẩu", Name = "colMatKhau", Width = 130 });
            guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Quyen", HeaderText = "Quyền", Name = "colQuyen", Width = 130 });
        }

        // --- HÀM TRUY VẤN DỮ LIỆU CHUNG (SELECT) ---
        private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }

        // --- HÀM THỰC THI LỆNH (INSERT, UPDATE, DELETE) ---
        private int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi lệnh: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected;
        }


        // --- LOAD DỮ LIỆU TÀI KHOẢN ---
        private void LoadDataTaiKhoan()
        {
            string query = "SELECT MaNV, TenNV, TaiKhoan, MatKhau, Quyen FROM NhanVien";
            guna2DataGridView1.DataSource = ExecuteQuery(query);
            ClearInputs();
        }

        // --- XỬ LÝ SỰ KIỆN CLICK CELL DATAGRIDVIEW ---
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];

                // Lấy MaNV và hiển thị dữ liệu
                if (row.Cells["colMaNV"].Value != DBNull.Value)
                {
                    selectedMaNV = (int)row.Cells["colMaNV"].Value;
                }

                txtTenND.Text = row.Cells["colTenNV"].Value.ToString();
                tctTenTK.Text = row.Cells["colTaiKhoan"].Value.ToString();
                txtMatKhau.Text = row.Cells["colMatKhau"].Value.ToString();

                string quyenValue = row.Cells["colQuyen"].Value.ToString();
                cboQuyen.SelectedItem = quyenValue;
            }
        }

        // --- CHỨC NĂNG THÊM (btnThem) ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenNV = txtTenND.Text.Trim();
            string taiKhoan = tctTenTK.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string quyen = cboQuyen.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(tenNV) || string.IsNullOrWhiteSpace(taiKhoan) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Tên người dùng, Tên tài khoản và Mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO NhanVien (TenNV, TaiKhoan, MatKhau, Quyen) VALUES (@TenNV, @TaiKhoan, @MatKhau, @Quyen)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenNV", tenNV),
                new SqlParameter("@TaiKhoan", taiKhoan),
                new SqlParameter("@MatKhau", matKhau),
                new SqlParameter("@Quyen", quyen)
            };

            if (ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataTaiKhoan();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại! (Tài khoản có thể đã tồn tại)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CHỨC NĂNG SỬA (btnSua) ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaNV == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenNV = txtTenND.Text.Trim();
            string taiKhoan = tctTenTK.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string quyen = cboQuyen.SelectedItem.ToString();
            int maNV = selectedMaNV;

            if (string.IsNullOrWhiteSpace(tenNV) || string.IsNullOrWhiteSpace(taiKhoan) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Tên người dùng, Tên tài khoản và Mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE NhanVien SET TenNV = @TenNV, TaiKhoan = @TaiKhoan, MatKhau = @MatKhau, Quyen = @Quyen WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenNV", tenNV),
                new SqlParameter("@TaiKhoan", taiKhoan),
                new SqlParameter("@MatKhau", matKhau),
                new SqlParameter("@Quyen", quyen),
                new SqlParameter("@MaNV", maNV)
            };

            if (ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataTaiKhoan();
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CHỨC NĂNG XÓA (btnXoa) ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaNV == -1)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", selectedMaNV)
                };

                if (ExecuteNonQuery(query, parameters) > 0)
                {
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataTaiKhoan();
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- CHỨC NĂNG TÌM KIẾM (btnTimKiem) ---
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTImKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadDataTaiKhoan(); // Tải lại toàn bộ nếu ô tìm kiếm trống
                return;
            }

            string query = "SELECT MaNV, TenNV, TaiKhoan, MatKhau, Quyen FROM NhanVien WHERE TenNV LIKE @Keyword OR TaiKhoan LIKE @Keyword";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };

            guna2DataGridView1.DataSource = ExecuteQuery(query, parameters);
        }

        // --- HÀM HỖ TRỢ DỌN DẸP INPUTS ---
        private void ClearInputs()
        {
            txtTenND.Clear();
            tctTenTK.Clear();
            txtMatKhau.Clear();
            cboQuyen.SelectedIndex = 0;
            selectedMaNV = -1; // Reset MaNV đã chọn
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {


        }

        private void QLTaiKhoan_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            guna2DragControl1.TargetControl = this;
        }
    }
}