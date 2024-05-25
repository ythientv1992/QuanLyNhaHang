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
    public partial class Menu : Form
    {
        private int total = 0;
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();
        private Dictionary<Button, Label> buttonLabelMap = new Dictionary<Button, Label>();

        public Menu()
        {
            InitializeComponent();

            // Thêm các cặp nút và nhãn vào từ điển
            buttonLabelMap.Add(button2, label7);
            buttonLabelMap.Add(button1, label8);
            buttonLabelMap.Add(btmon1, label9);
            buttonLabelMap.Add(btmon2, label10);
            buttonLabelMap.Add(button3, label12);
            buttonLabelMap.Add(button5, label18);
            buttonLabelMap.Add(button6, label17);
            buttonLabelMap.Add(button8, label16);
            buttonLabelMap.Add(button7, label15);
            buttonLabelMap.Add(button4, label13);

            // Thêm các sự kiện click cho các nút
            foreach (var button in buttonLabelMap.Keys)
            {
                button.Click += Button_Click;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0; // Chọn mục đầu tiên là "Bàn 1"
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddItemToCart(label2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddItemToCart(label3.Text);
        }

        private void btmon1_Click(object sender, EventArgs e)
        {
            AddItemToCart(label5.Text);
        }

        private void btmon2_Click(object sender, EventArgs e)
        {
            AddItemToCart(label6.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddItemToCart(label11.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddItemToCart(label22.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddItemToCart(label21.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddItemToCart(label20.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddItemToCart(label19.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddItemToCart(label14.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Xóa toàn bộ giá trị trong listBox1
            listBox1.Items.Clear();

            // Đặt lại tổng thành 0
            total = 0;

            // Cập nhật lại giá trị của TextBox tổng tiền
            txtTotal.Text = "Tổng: 0";
        }

        private void AddItemToCart(string labelText)
        {
            int itemCount = 1;

            //Kiểm tra dòng đã có trong list box chưa có thì itemcount tăng 1 để xuất x....
            if (itemCounts.ContainsKey(labelText))
            {
                itemCount = itemCounts[labelText] + 1;
                itemCounts[labelText] = itemCount;
            }
            else
            {
                itemCounts.Add(labelText, itemCount);
            }

            listBox1.Items.Clear();
            foreach (var item in itemCounts)
            {
                string displayText = item.Key.Length > 20 ? item.Key.Substring(0, 20) + "..." : item.Key;
                listBox1.Items.Add(displayText + " x" + item.Value);
            }
        }

        private void btGoiMon_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ textbox "Tổng: số tiền"
            string tongTienText = txtTotal.Text;

            // Loại bỏ chuỗi "Tổng: " từ giá trị của textbox
            string soTienText = tongTienText.Substring(6);

            // Chuyển đổi chuỗi thành kiểu dữ liệu decimal
            decimal thanhTien;
            if (decimal.TryParse(soTienText, out thanhTien))
            {
                // Lấy giá trị từ ComboBox
                string selectedTable = comboBox1.SelectedItem.ToString();
                string tableNumber = selectedTable.Split(' ')[1]; // Loại bỏ phần "Bàn " và chỉ lấy số

                // Thêm giá trị vào cơ sở dữ liệu
                AddOrdersToDatabase(selectedTable);

                // Cập nhật cột NhanDon của bàn đó thành false
                UpdateTableStatus(tableNumber);

                // Sau khi thêm dữ liệu và cập nhật trạng thái bàn, cập nhật lại giá trị total và txtTotal.Text
                total = 0;
                txtTotal.Text = "Tổng: " + total;
                itemCounts.Clear();
                listBox1.Items.Clear();

                MessageBox.Show("Đơn hàng đã được gửi đi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Giá trị không hợp lệ!");
            }
        }

        private void AddOrdersToDatabase(string tableName)
        {
            string connectionString = @"Data Source=WolfLord\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True";
            string query = "INSERT INTO " + tableName + " (TenMonAn, SoLuong) VALUES (@TenMonAn, @SoLuong)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenMonAn", "");
                command.Parameters.AddWithValue("@SoLuong", 0);

                try
                {
                    connection.Open();

                    foreach (var item in itemCounts)
                    {
                        command.Parameters["@TenMonAn"].Value = item.Key;
                        command.Parameters["@SoLuong"].Value = item.Value;

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Thêm dữ liệu thành công!");
                        }
                        else
                        {
                            Console.WriteLine("Thêm dữ liệu không thành công!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void UpdateTableStatus(string tableNumber)
        {
            string connectionString = @"Data Source=WolfLord\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True";
            string query = "UPDATE Ban SET NhanDon = 0 WHERE SoBan = @SoBan";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SoBan", int.Parse(tableNumber));

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Cập nhật trạng thái bàn thành công.");
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy số bàn.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }





        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string maKhuyenMai = textBox1.Text.Trim(); // Lấy giá trị từ TextBox

            // Kết nối đến cơ sở dữ liệu và truy vấn dữ liệu từ bảng KhuyenMai
            string connectionString = @"Data Source=WolfLord\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True";
            string query = "SELECT SoTienGiam FROM KhuyenMai WHERE MaKhuyenMai = @MaKhuyenMai";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaKhuyenMai", maKhuyenMai);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int soTienGiam = Convert.ToInt32(result);

                        // Trừ giá trị SoTienGiam vào tổng
                        SubtractFromTotal(soTienGiam);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void SubtractFromTotal(int soTienGiam)
        {
            // Lấy giá trị từ textbox "Tổng: số tiền"
            string tongTienText = txtTotal.Text;

            // Loại bỏ chuỗi "Tổng: " từ giá trị của textbox
            string soTienText = tongTienText.Substring(6);

            // Chuyển đổi chuỗi thành kiểu dữ liệu decimal
            decimal tongTien;
            if (decimal.TryParse(soTienText, out tongTien))
            {
                // Trừ giá trị SoTienGiam vào tổng
                tongTien -= soTienGiam;

                // Cập nhật lại giá trị của TextBox tổng tiền
                txtTotal.Text = "Tổng: " + tongTien.ToString();
            }
            else
            {
                MessageBox.Show("Giá trị không hợp lệ!");
            }
        }

        // Phương thức để cập nhật giá trị của textbox
        private void UpdateTotalTextBox()
        {
            txtTotal.Text = "Tổng: " + total.ToString();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null && buttonLabelMap.ContainsKey(button))
            {
                Label label = buttonLabelMap[button];
                if (label != null)
                {
                    // Lấy giá trị từ nhãn và cộng vào tổng
                    int value;
                    if (int.TryParse(label.Text, out value))
                    {
                        total += value;
                        // Cập nhật giá trị của textbox
                        UpdateTotalTextBox();
                    }
                    else
                    {
                        MessageBox.Show("Giá trị không hợp lệ!");
                    }
                }
            }
        }
    }
}
