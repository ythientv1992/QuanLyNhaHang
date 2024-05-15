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
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

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

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string labelText = label2.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string labelText = label3.Text;
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

        private void btmon1_Click(object sender, EventArgs e)
        {
            string labelText = label5.Text;
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

        private void btmon2_Click(object sender, EventArgs e)
        {
            string labelText = label6.Text;
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

        private void button3_Click(object sender, EventArgs e)
        {
            string labelText = label11.Text;
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
                string ban = comboBox1.SelectedItem.ToString();

                // Thêm giá trị vào cơ sở dữ liệu
                string connectionString = @"Data Source=WolfLord\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True";
                string query = "INSERT INTO ThanhToan (DonGia, NgayLap, Ban) VALUES (@DonGia, @NgayLap, @Ban)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DonGia", thanhTien);
                    command.Parameters.AddWithValue("@NgayLap", DateTime.Now); // Thời gian hiện tại
                    command.Parameters.AddWithValue("@Ban", ban); // Giá trị từ ComboBox

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm dữ liệu thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Thêm dữ liệu không thành công!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
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

        private void button5_Click(object sender, EventArgs e)
        {
            string labelText = label22.Text;
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

        private void button6_Click(object sender, EventArgs e)
        {
            string labelText = label21.Text;
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

        private void button8_Click(object sender, EventArgs e)
        {
            string labelText = label20.Text;
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

        private void button7_Click(object sender, EventArgs e)
        {
            string labelText = label19.Text;
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

        private void button4_Click(object sender, EventArgs e)
        {
            string labelText = label14.Text;
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
    }
}
