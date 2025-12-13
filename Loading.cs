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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            // Xin chào + tên NV (label3 đang là "Xin chào" trong Designer)
            var ten = Session.CurrentUser?.TenNV?.Trim();
            if (!string.IsNullOrEmpty(ten))
                label3.Text = "Xin chào, " + ten;
            else
                label3.Text = "Xin chào";

            timer1.Interval = 35;
            guna2CircleProgressBar1.Value = 0;
            label2.Text = "0";

            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            int v = guna2CircleProgressBar1.Value;

            int step = 2;                
            if (v >= 70) step = 1;
            int next = guna2CircleProgressBar1.Value + step;

            if (next >= 100)
            {
                timer1.Stop();
                guna2CircleProgressBar1.Value = 100;
                label2.Text = "100";

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            guna2CircleProgressBar1.Value = next;
            label2.Text = next.ToString();
        }
    }
}
