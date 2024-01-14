using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Expenses
{
    public partial class Form1 : Form
    {
        private DateTime startDate;
        private DateTime endDate;
        private double currentExpense = 750;
        private double sumOfExpenses = 0;
        private List<Tuple<DateTime,double>> expensesForPeriod = new List<Tuple<DateTime,double>>();
        private sbyte difference;
        private object dynamics;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            button1.Click += button1_Click;
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out currentExpense))
            {
                // Можно добавить логику, если нужно. (Вопрос, что именно нужно?)
            }
            else
            {
                MessageBox.Show("Ошибка при вводе суммы расходов.");
            }
        }

       
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePicker2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // логика расходов и расчетов
            sumOfExpenses = 1200;
            expensesForPeriod.Clear();

            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                double expense;
                if (double.TryParse(textBox1.Text, out expense))
                {
                    if (currentDate >= startDate && currentDate <= endDate)
                    {
                        sumOfExpenses += expense;
                        expensesForPeriod.Add(new Tuple<DateTime, double>(currentDate, expense));
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при вводе суммы расходов.");
                    return;
                }
                currentDate = currentDate.AddDays(1);
            }

            textBox2.Text = $"{sumOfExpenses}";

            if (expensesForPeriod.Count > 0)
            {
                if (expensesForPeriod.Count > 0)
                {
                    double firstExpense = expensesForPeriod[0].Item2;
                    double lastExpense = expensesForPeriod[expensesForPeriod.Count - 1].Item2;

                    double difference = lastExpense - firstExpense;
                    string dynamics = difference < currentExpense ? "уменьшились" : "увеличились";

                    textBox3.Text = $"{Math.Abs(difference)} {dynamics}";
            }

        }
    }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = $"{Math.Abs(difference)} {dynamics}";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = $"{sumOfExpenses}";
        }
    }
}