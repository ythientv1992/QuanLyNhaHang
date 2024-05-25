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
    public partial class TrangChinhNhanVien : Form
    {
        public TrangChinhNhanVien()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new Menu());
        }

        private void TrangChinhNhanVien_Load(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;

            // Đặt FlatStyle thành Flat để loại bỏ viền
            btExit.FlatStyle = FlatStyle.Flat;

            // Đặt FlatAppearance.BorderSize thành 0 để loại bỏ viền
            btExit.FlatAppearance.BorderSize = 0;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void loadform(object Form)
        {
            if(this.panel3.Controls.Count > 0) this.panel3.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(f);
            this.panel3.Tag = f;
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new TinhTrangBan());
        }

        

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
