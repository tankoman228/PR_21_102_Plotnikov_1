using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PR_21_102_Plotnikov_1
{
    /// <summary>
    /// Реализует изменение размера текста элементов интерфейса окна
    /// </summary>
    internal class TextResize
    {
        Window window; //Цеевое окно (в нём меняет размер текста)

        public TextResize(Window window)
        {
            this.window = window;
        }

        /// <summary>
        /// Передавать делегату окна (обычно слушатель: при изменении размера)
        /// </summary>
        public void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var textboxes = FindVisualChildren<TextBox>(window);
            var textBlocks = FindVisualChildren<TextBlock>(window);

            var NewTextSize = (e.NewSize.Height / 20.0 + e.NewSize.Width / 10.0) / 2;
            if (NewTextSize > 48) NewTextSize = 48;

            foreach (var textbox in textboxes)
            {
                textbox.FontSize = NewTextSize / 1.5;
            }

            foreach (var textblock in textBlocks)
            {
                textblock.FontSize = NewTextSize / 2;
            }
        }

        /// <summary>
        /// возвращает список всех элементов интерфейса данного окна заданного типа
        /// </summary>
        /// <typeparam name="T"> тип искомых элементов интерфейса</typeparam>
        /// <param name="parent">окно</param>
        /// <returns></returns>
        public IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            //Все элементы окна
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                //Дочерние элементы элементов
                foreach (T nestedChild in FindVisualChildren<T>(child))
                {
                    yield return nestedChild;
                }
            }     
        }
    }
}
