using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class Kho : Form
    {
        private static readonly string connectionString = "Data Source=WolfLord\\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public Kho()
        {
            InitializeComponent();
        }

        private void Kho_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            DataTable dt = GetDataTable("SELECT * FROM Kho");
            dgvKho.DataSource = dt;
            dgvKho.AllowUserToAddRows = true;
            dgvKho.AllowUserToDeleteRows = true;
            dgvKho.CellEndEdit += dgvKho_CellEndEdit;
            dgvKho.UserDeletingRow += dgvKho_UserDeletingRow;
        }

        private DataTable GetDataTable(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private void dgvKho_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            DataGridViewRow row = dgv.Rows[e.RowIndex];

            if (row.IsNewRow) return;

            string tenNguyenLieu = row.Cells["TenNguyenLieu"].Value?.ToString();
            string ngayNhapHang = row.Cells["NgayNhapHang"].Value?.ToString();
            string soLuongNhap = row.Cells["SoLuongNhap"].Value?.ToString();

            if (string.IsNullOrEmpty(tenNguyenLieu) || string.IsNullOrEmpty(ngayNhapHang))
            {
                InsertRow(row);
            }
            else
            {
                UpdateRow(tenNguyenLieu, ngayNhapHang, soLuongNhap);
            }
        }

        private void InsertRow(DataGridViewRow row)
        {
            string query = "INSERT INTO Kho (TenNguyenLieu, NgayNhapHang, SoLuongNhap) VALUES (@TenNguyenLieu, @NgayNhapHang, @SoLuongNhap)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenNguyenLieu", row.Cells["TenNguyenLieu"].Value ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NgayNhapHang", row.Cells["NgayNhapHang"].Value ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SoLuongNhap", row.Cells["SoLuongNhap"].Value ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateRow(string tenNguyenLieu, string ngayNhapHang, string soLuongNhap)
        {
            string query = "UPDATE Kho SET SoLuongNhap = @SoLuongNhap WHERE TenNguyenLieu = @TenNguyenLieu AND NgayNhapHang = @NgayNhapHang";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SoLuongNhap", soLuongNhap ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TenNguyenLieu", tenNguyenLieu);
                    command.Parameters.AddWithValue("@NgayNhapHang", ngayNhapHang);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void dgvKho_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string tenNguyenLieu = e.Row.Cells["TenNguyenLieu"].Value?.ToString();
            string ngayNhapHang = e.Row.Cells["NgayNhapHang"].Value?.ToString();
            if (!string.IsNullOrEmpty(tenNguyenLieu) && !string.IsNullOrEmpty(ngayNhapHang))
            {
                DeleteRow(tenNguyenLieu, ngayNhapHang);
            }
        }

        private void DeleteRow(string tenNguyenLieu, string ngayNhapHang)
        {
            string query = "DELETE FROM Kho WHERE TenNguyenLieu = @TenNguyenLieu AND NgayNhapHang = @NgayNhapHang";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenNguyenLieu", tenNguyenLieu);
                    command.Parameters.AddWithValue("@NgayNhapHang", ngayNhapHang);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
