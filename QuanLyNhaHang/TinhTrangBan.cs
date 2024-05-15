using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class TinhTrangBan : Form
    {
        // Danh sách các nút và danh sách tương ứng với giá trị cột Nhận đơn
        private List<Button> buttons = new List<Button>();
        private List<bool> receivedOrders = new List<bool>();
        public TinhTrangBan()
        {
            InitializeComponent();
            // Khởi tạo các nút và danh sách giá trị Nhận đơn
            InitializeButtons();
        }
        private void InitializeButtons()
        {
            // Thêm các nút vào danh sách
            buttons.Add(ban1);
            buttons.Add(ban2);
            buttons.Add(ban3);
            buttons.Add(ban4);
            buttons.Add(ban5);
            buttons.Add(ban6);
            buttons.Add(ban7);
            buttons.Add(ban8);
            buttons.Add(ban9);
            buttons.Add(ban10);

            // Thêm các sự kiện click cho các nút
            foreach (var button in buttons)
            {
                button.Click += Button_Click;
            }

            // Lấy dữ liệu từ cơ sở dữ liệu và điền vào danh sách giá trị Nhận đơn
            FetchDataFromDatabase();

            // Thiết lập màu cho từng nút dựa trên giá trị của cột Nhận đơn
            SetButtonColors();
        }
        private void SetButtonColors()
        {
            // Duyệt qua danh sách nút và thiết lập màu dựa trên giá trị của cột Nhận đơn
            for (int i = 0; i < buttons.Count; i++)
            {
                Button button = buttons[i];
                bool receivedOrder = receivedOrders[i];
                if (receivedOrder)
                {
                    button.BackColor = Color.Red; // Màu đỏ nếu Nhận đơn là true
                }
                else
                {
                    button.BackColor = Color.Green; // Màu xanh lá cây nếu Nhận đơn là false
                }
            }
        }
        private void FetchDataFromDatabase()
        {
            // Kết nối đến cơ sở dữ liệu và lấy dữ liệu từ cột Nhận đơn
            string connectionString = @"Data Source=WolfLord\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True";
            string query = "SELECT NhanDon FROM Ban";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bool receivedOrder = Convert.ToBoolean(reader["NhanDon"]); // Sửa tên cột thành "NhanDon"
                    receivedOrders.Add(receivedOrder);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện click của các nút
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TinhTrangBan_Load(object sender, EventArgs e)
        {

        }
    }
}
