using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Expenses
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Создаем экземпляры всех трех форм
            Form1 form1 = new Form1();
            Form2 form2 = new Form2();
            Form3 form3 = new Form3();

            // Подписываемся на событие FormClosed для обработки закрытия каждой из форм
            form1.FormClosed += (sender, e) => Form_FormClosed();
            form2.FormClosed += (sender, e) => Form_FormClosed();
            form3.FormClosed += (sender, e) => Form_FormClosed();

            Application.Run(new Form2());
            
            string connectionString = "Data Source=LAPTOP-SIJNS5K6\\SQLEXPRESS;Initial Catalog=kursach;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Соединение с базой данных установлено успешно.");

                // Запуск приложения
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form2());
            }
            catch (Exception ex)
            {
                // Обработка ошибок при подключении к базе данных
                Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
            }
            finally
            {
                // Важно закрыть соединение, когда оно больше не нужно
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        private static void Form_FormClosed()
        {
            Environment.Exit(0);
        }
    }
}
