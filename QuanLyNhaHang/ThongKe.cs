using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyNhaHang
{
    public partial class ThongKe : Form
    {
        private static readonly string connectionString = "Data Source=WolfLord\\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True;Encrypt=True";

        public ThongKe()
        {
            InitializeComponent();
        }

        private void btnTheoNgay_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn.BackColor == Color.White)
                {
                    btnTheoThang.BackColor = Color.White;
                    btn.BackColor = Color.Gray;
                }
                else
                {
                    btn.BackColor = Color.White;
                }

                DataTable dt = GetDataTable("GetThongKeTheoNgay");
                dgvThongKe.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTheoThang_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn.BackColor == Color.White)
                {
                    btnTheoNgay.BackColor = Color.White;
                    btn.BackColor = Color.Gray;
                }
                else
                {
                    btn.BackColor = Color.White;
                }

                DataTable dt = GetDataTable("GetThongKeTheoThang");
                dgvThongKe.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetDataTable(string storedProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
