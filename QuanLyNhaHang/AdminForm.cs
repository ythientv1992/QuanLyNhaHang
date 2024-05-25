using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            loadform(new ThongKe());
        }
        public void loadform(object Form)
        {
            if (this.panel3.Controls.Count > 0) this.panel3.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(f);
            this.panel3.Tag = f;
            f.Show();
        }

        private void btnTinhTrangBan_Click(object sender, EventArgs e)
        {
            loadform(new TinhTrangBan());
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            loadform(new Menu());
        }

        private void btnLichLamViec_Click(object sender, EventArgs e)
        {
            loadform(new QLLichLamVC());
        }

        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            loadform(new TinhLuong());
        }

        private void btnBep_Click(object sender, EventArgs e)
        {
            loadform(new Kho());
        }
    }
}
