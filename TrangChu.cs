using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnNet
{
    public partial class TrangChu : Form
    {
        private Label _lbXinChao;

        public TrangChu()
        {
            InitializeComponent();

            this.Load += TrangChu_Load;

            // Nếu bạn đã có xử lý logout ở nơi khác thì bỏ dòng này
            guna2Button5.Click += guna2Button5_Click;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            bool isAdmin = (Session.CurrentUser?.Quyen ?? "").Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase);

            guna2Button2.Visible = isAdmin;   // Nút "Quản trị"

            CreateOrUpdateHelloLabel();
            PositionHelloLabel();

            guna2Panel1.Resize += (s, args) => PositionHelloLabel();
            _lbXinChao.SizeChanged += (s, args) => PositionHelloLabel();
        }

        private void CreateOrUpdateHelloLabel()
        {
            if (_lbXinChao == null)
            {
                _lbXinChao = new Label();
                _lbXinChao.AutoSize = true;
                _lbXinChao.BackColor = Color.Transparent;
                _lbXinChao.ForeColor = Color.DimGray;
                _lbXinChao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                _lbXinChao.ForeColor = Color.ForestGreen;
                _lbXinChao.MaximumSize = new Size(190, 0); //
                _lbXinChao.AutoEllipsis = true;

                // Add cuối để nằm “trên” các control khác
                guna2Panel1.Controls.Add(_lbXinChao);
            }

            var ten = Session.CurrentUser?.TenNV?.Trim();
            _lbXinChao.Text = string.IsNullOrEmpty(ten) ? "Xin chào" : $"Xin chào, {ten}";
        }

        private void PositionHelloLabel()
        {
            if (_lbXinChao == null) return;

            int paddingX = 10;
            int paddingY = 10;

            // Đặt label ngay phía trên nút Đăng xuất
            int x = paddingX;
            int y = guna2Button5.Top - paddingY - _lbXinChao.Height;

            // Chặn trường hợp âm khi layout thay đổi
            if (y < 0) y = 0;

            _lbXinChao.Location = new Point(x, y);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Nếu bạn đang dùng flow "Logout về Login"
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            container(new QLTaiKhoan());
        }
        private void container(object _form)
        {
            if (guna2Panel3.Controls.Count > 0) guna2Panel3.Controls.Clear();

            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            guna2Panel3.Controls.Add(fm);
            guna2Panel3.Tag = fm;
            fm.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            container(new FormSoDoSan());
        }
    }
}