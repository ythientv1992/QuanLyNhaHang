using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class QLLichLamVC : Form
    {
        private const string connectionString = "Data Source=WolfLord\\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True;Encrypt=True";

        public QLLichLamVC()
        {
            InitializeComponent();
        }

        private void btLichLam_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian bắt đầu từ DateTimePicker1
                DateTime NgayBatDau = dtNgayBatDau.Value.Date;

                // Lấy thời gian kết thúc từ DateTimePicker2 và thêm một ngày để bao gồm cả ngày đã chọn
                DateTime NgayKetThuc = dtNgayKetThuc.Value.Date.AddDays(1);

                // Truyền thời gian bắt đầu và thời gian kết thúc như là các tham số cho thủ tục
                DataTable dataTable = GetDataTable("LayNVCoCaLam", new object[] { NgayBatDau, NgayKetThuc });

                dgvLichLamViec.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void dtNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtNgayBatDau.Value;
            DayOfWeek dayOfWeek = selectedDate.DayOfWeek;

            // Kiểm tra nếu ngày được chọn không phải là ngày thứ Hai (Monday)
            if (dayOfWeek != DayOfWeek.Monday)
            {
                // Tìm ngày thứ Hai gần nhất
                DateTime nearestMonday = selectedDate.AddDays(-(int)dayOfWeek + (int)DayOfWeek.Monday);

                // Đặt giá trị cho DateTimePicker thành ngày thứ Hai gần nhất
                dtNgayBatDau.Value = nearestMonday;
            }

            // Lấy ngày từ DateTimePicker đầu tiên
            DateTime ngayBatDau = dtNgayBatDau.Value;

            // Cộng thêm 7 ngày
            DateTime ngayKetThuc = ngayBatDau.AddDays(7);

            // Đặt giá trị cho DateTimePicker thứ hai
            dtNgayKetThuc.Value = ngayKetThuc;
        }

        private DataTable GetDataTable(string storedProcedure, object[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@NgayBatDau", parameters[0]);
                    command.Parameters.AddWithValue("@NgayKetThuc", parameters[1]);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }
    }
}
