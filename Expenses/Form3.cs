using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Expenses
{
    public partial class Form3 : Form
    {
        private const string connectionString = "Data Source=LAPTOP-SIJNS5K6\\SQLEXPRESS;Initial Catalog=kursach;Integrated Security=True;";

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Переход на Form2 при нажатии кнопки "Button1"
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Логика регистрации пользователя (перенесена в Form2)
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Добавим проверку наличия данных в текстовых полях
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowWarning("Введите имя пользователя и пароль");
                return;
            }

            // Проверка существования пользователя
            if (UserExists(username))
            {
                ShowWarning("Пользователь с таким именем уже существует");
                return;
            }

            // Генерация случайной соли
            string salt = GenerateSalt();

            // Хеширование пароля
            string hashedPassword = HashPassword(password, salt);

            // Сохранение пользователя в базе данных
            bool userSaved = SaveUserToDatabase(username, hashedPassword, salt);

            if (userSaved)
            {
                ShowSuccess("Пользователь успешно зарегистрирован");
            }
            else
            {
                ShowError("Ошибка регистрации пользователя");
            }
        }

        private bool UserExists(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка базы данных: {ex.Message}");
                return false;
            }
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);

                byte[] hashedPassword = sha256.ComputeHash(saltedPassword);

                return BitConverter.ToString(hashedPassword).Replace("-", "").ToLower();
            }
        }

        private bool SaveUserToDatabase(string username, string hashedPassword, string salt)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (Username, PasswordHash, Salt) VALUES (@Username, @PasswordHash, @Salt)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        command.Parameters.AddWithValue("@Salt", salt);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка базы данных: {ex.Message}");
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Логика при изменении текста в TextBox1
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Логика при изменении текста в TextBox2
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Очистка текста в текстбоксах при закрытии формы
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }
    }
}
