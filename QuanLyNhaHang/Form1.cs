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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //xóa nút thu nhỏ, phóng to và thoát

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;

            // Đặt FlatStyle thành Flat để loại bỏ viền
            btExit.FlatStyle = FlatStyle.Flat;

            // Đặt FlatAppearance.BorderSize thành 0 để loại bỏ viền
            btExit.FlatAppearance.BorderSize = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=WolfLord\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True";
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT quyen FROM TaiKhoan WHERE username = @username AND password = @password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string role = reader["quyen"].ToString();
                        if (role == "admin")
                        {
                            AdminForm adminForm = new AdminForm();
                            adminForm.Show();
                        }
                        else if (role == "nhanvien")
                        {
                            TrangChinhNhanVien form2 = new TrangChinhNhanVien();
                            form2.Show();
                        }
                        // Đóng kết nối và đóng form đăng nhập
                        connection.Close();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Tên người dùng hoặc mật khẩu không chính xác!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
