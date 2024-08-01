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
using System.Data;
using System.IO;
using Microsoft.Win32;

static class VissualArray
{
    public static DataTable ToDataTable<T>(this T[,] matrix)
    {
        var res = new DataTable();
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            res.Columns.Add("Столбец " + (i + 1), typeof(T));
        }
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            var row = res.NewRow();

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                row[j] = matrix[i, j];
            }
            res.Rows.Add(row);
        }
        return res;
    }
}
namespace Адовые_интерфейсы__21_part2._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void exit_Click(object sender, RoutedEventArgs e)
        {
            // Добавляем данные для MessageBox
            string t1 = "Уверены, что хотите выйти?";
            string t2 = "Предупреждение";
            // В данном случае Да\Нет
            MessageBoxButton bt = MessageBoxButton.YesNo;
            // Подставляем иконку вопроса к тексту
            MessageBoxImage icon = MessageBoxImage.Question;
            // Создаем переменную для присваивания всех данных и присваиваем данные
            MessageBoxResult result;
            result = MessageBox.Show(t1, t2, bt, icon, MessageBoxResult.Yes);
            // Оператор switch помогает нам сменить окно, в случае положительного ответа в MessageBox
            switch (result)
            {
                // Описан только случай с положительным ответом, так как при отрицательном ничего не происходит
                case MessageBoxResult.Yes:
                    // Закрытие рабочего окна
                    this.Close();
                    break;
            }
        }

        int[,] matrix;

        private void create2_Click(object sender, RoutedEventArgs e) //Кнопка создания
        {
            ras2.IsEnabled = true;
            int k, s = 2, d;
            bool f1 = Int32.TryParse(kolk.Text, out k);
            bool f3 = Int32.TryParse(n.Text, out d);
            if (f1 == true && f3 == true)
            {
                if (k > 0)
                {
                    matrix = new int[s, k];
                    Random rnd = new Random();
                    for (int i = 0; i < s; i++)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            matrix[i, j] = rnd.Next(-d, d); ;
                        }
                    }
                    dg2.ItemsSource = VissualArray.ToDataTable(matrix).DefaultView;
                }
                else
                {
                    // Очистка окна ввода
                    kolk.Clear();
                    n.Clear();
                    // Предупреждение об ошибке при помощи MessageBox
                    string t1 = "Недопустимое значение!";
                    string t2 = "Ошибочка";
                    // В данном случае единственный возможный ответ ОК
                    MessageBoxButton button = MessageBoxButton.OK;
                    // Добавление иконки ошибки к тексту
                    MessageBoxImage icon = MessageBoxImage.Error;
                    // Переменная для объединения данных
                    MessageBoxResult result;
                    result = MessageBox.Show(t1, t2, button, icon, MessageBoxResult.Yes);
                }
            }
            else
            {
                // Очистка окна ввода
                kolk.Clear();
                n.Clear();
                // Предупреждение об ошибке при помощи MessageBox
                string t1 = "Недопустимое значение!";
                string t2 = "Ошибочка";
                // В данном случае единственный возможный ответ ОК
                MessageBoxButton button = MessageBoxButton.OK;
                // Добавление иконки ошибки к тексту
                MessageBoxImage icon = MessageBoxImage.Error;
                // Переменная для объединения данных
                MessageBoxResult result;
                result = MessageBox.Show(t1, t2, button, icon, MessageBoxResult.Yes);
            }
        }

        private void ras2_Click(object sender, RoutedEventArgs e) //Биндим расчёт
        {
            int x = 2;
            int y = Convert.ToInt32(kolk.Text);
            int a = 0;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0)
                    {
                        if (matrix[i, j] > 0 && matrix[i + 1, j] < 0 || matrix[i + 1, j] > 0 && matrix[i, j] < 0)
                        {
                            a = j + 1;
                        }
                    }
                }
            }

            if (a != 0)
            {
                viv2.Text = Convert.ToString(a);
            }
            else
            {
                viv2.Text = Convert.ToString(0);
            }

        }

        private void clir_Click(object sender, RoutedEventArgs e) //Чистка
        {
            ras2.IsEnabled = false;
            dg2.ItemsSource = null;
            n.Clear();
            kolk.Clear();
            viv2.Clear();
        }

        private void about_Click(object sender, RoutedEventArgs e) //Поясняем
        {
            MessageBox.Show("Горе-дизайнер Бирюков Георгий из ИСП-23. Дана целочисленная матрица размера M * N. Найти номер последнего из ее столбцов, содержащих равное количество положительных и отрицательных элементов(нулевые элементы матрицы не учитываются).Если таких столбцов нет, то вывести 0");
        }


        private void anek_Click(object sender, RoutedEventArgs e) //Хихикаем
        {
            MessageBox.Show("Что становится с черепашкой, когда она вырастает? Она становится черепавлом"); //Выводим анекдот 
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //Создаём и настраиваем элемент
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "сишарповаябимба.txt";
            save.Filter = "Все файлы(*.*) | *.* | Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохрание таблицы";

            //Открываем диалоговое окно и при успехе работаем с файлом
            if (save.ShowDialog() == true)
            {
                StreamWriter file = new StreamWriter(save.FileName); //Создаём поток для работы с файлом и указываем ему имя файла
                file.WriteLine(matrix.Length); //Записываем размер массива в файл
                //Записываем элемент матрицы в файл
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        file.WriteLine(matrix[i, j]);
                    }
                } 
                    
               
                file.Close();
            }

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            //Создаём и настраиваем элемент
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = "сишарповаябимба.txt";
            open.Filter = "Все файлы(*.*) | *.* | Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Открываем";
            //Открываем диаологовое окно и при успехе работаем с файлом
            if (open.ShowDialog() == true)
            {
                StreamReader file = new StreamReader(open.FileName); //Создамём поток для работы с файлом и указываем ему имя файла
                int lenght = Convert.ToInt32(file.ReadLine()); //Читаем размер массива
                lenght = lenght / 2; //Считываем все элементы и делим 2, т.к. кол-во элементов всегда чётное и они считываются подряд
                matrix = new int[2, lenght]; //Создаём массив
                //Считываем матрицу с файла
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j=0; j<matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = Convert.ToInt32(file.ReadLine());
                    }
                }
               
                file.Close();
                dg2.ItemsSource = VissualArray.ToDataTable(matrix).DefaultView; //Выводим массив на форму

            }

        }
    }
}
