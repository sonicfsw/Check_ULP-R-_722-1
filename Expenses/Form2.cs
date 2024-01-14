using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Expenses
{
    public partial class Form2 : Form
    {
        private const string connectionString = "Data Source=LAPTOP-SIJNS5K6\\SQLEXPRESS;Initial Catalog=kursach;Integrated Security=True;";

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Перейти на Form3 при нажатии кнопки "Button1" для регистрации
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Подключение к базе данных и проверка логина с хешированием пароля
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Запрос на БД
                string query = "SELECT PasswordHash, Salt FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", textBox1.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            string storedSalt = reader["Salt"].ToString();

                            // Проверка хеша пароля
                            if (VerifyPasswordHash(textBox2.Text, storedHash, storedSalt))
                            {
                                // Вход успешен, переход на Form1
                                Form1 form1 = new Form1();
                                form1.Show();
                                this.Hide();
                            }
                            else
                            {
                                // Вход не удался, показать сообщение об ошибке
                                MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // Пользователь с указанным именем не найден
                            MessageBox.Show("Пользователь с указанным именем не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private static bool VerifyPasswordHash(string inputPassword, string storedHash, string storedSalt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Получение байтов соли из строки
                byte[] saltBytes = Convert.FromBase64String(storedSalt);

                // Хеширование введенного пароля с использованием соли
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputPassword);
                byte[] saltedPassword = new byte[inputBytes.Length + saltBytes.Length];
                Buffer.BlockCopy(inputBytes, 0, saltedPassword, 0, inputBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, saltedPassword, inputBytes.Length, saltBytes.Length);

                byte[] inputHash = sha256.ComputeHash(saltedPassword);

                // Сравнение хешей
                return BitConverter.ToString(inputHash).Replace("-", "").Equals(storedHash, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Обработчик изменения текста в TextBox1
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Обработчик изменения текста в TextBox2
        }
    }
}
