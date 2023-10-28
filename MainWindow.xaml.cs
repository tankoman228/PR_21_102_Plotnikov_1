using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR_21_102_Plotnikov_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.SizeChanged += new TextResize(this).Window_SizeChanged;

            tb_input.TextChanged += Tb_input_TextChanged;
        }

        //Вывод данных
        private void print(string msg)
        {
            tb_result.Text = msg;
        }

        //Проверка введённого числа
        private void Tb_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = tb_input.Text;      
            long input_number = -1;

            //Проверка введённых данных на правильность
            if (!long.TryParse(input, out input_number))
            {
                print("Введённые данные не являются целым числом");
                return;
            }

            if (input_number <= 0)
            {
                print("Число должно быть неотрицательным");
                return;
            }

            try
            {
                //Финальная проверка введённых данных на правильность
                if (input.Length != 12 || input.StartsWith('0'))
                {
                    print("Введённое число должно быть 12-значным");
                    return;
                }

                //Поиск произведения первых 3 и суммы последних 9
                int firstMult = 1;
                int lastSum = 0;

                for (int i = 0; i < 3; i++)
                {
                    firstMult *= int.Parse(input[i].ToString());
                }
                for (int i = 3; i < 12; i++)
                {
                    lastSum += int.Parse(input[i].ToString());
                }

                //Вывод полученного ответа
                if (firstMult == lastSum)
                {
                    print("Произведение первых 3 десятичных цифр равно сумме 9 последних десятичных цифр");
                }
                else
                {
                    print("Произведение первых 3 десятичных цифр НЕ равно сумме 9 последних десятичных цифр");
                }
            }
            catch (Exception ex)
            {
                //обработка ошибки
                MessageBox.Show("Не удалось проверить число. Ошибка: " + ex.Message, "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
