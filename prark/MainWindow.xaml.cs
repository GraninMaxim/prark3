using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static prark.Class1;

namespace prark
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
            Class1 class1 = new Class1();
            private int[] array;
        

            public ObservableCollection<ArrayItem> ArrayItems { get; set; }

            public MainWindow()
            {
                InitializeComponent();

                ArrayItems = new ObservableCollection<ArrayItem>();
                dataGrid.ItemsSource = ArrayItems;

                DataContext = this;
            }
        private void DrawEqualNumbersGraph()
        {
            double x = 0;
            graphCanvas.Children.Clear(); // Очистить любой предыдущий график

            double canvasWidth = graphCanvas.ActualWidth;
            double canvasHeight = graphCanvas.ActualHeight;

            double barWidth = canvasWidth / array.Length;
            double maxValue = array.Max();

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] == array[i - 1]) // Проверить, равен ли текущий номер предыдущему
                {
                    
                    // Рассчитать размер и позицию столбца
                    double barHeight = (array[i] / maxValue) * canvasHeight;
                    x+=21;
                   double  y = canvasHeight - barHeight;

                    // Создать и добавить прямоугольник, представляющий столбец
                    Rectangle rect = new Rectangle
                    {
                        Width = 20,
                        Height = barHeight,
                        Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0)), // Используйте другой цвет, чтобы отличить одинаковые числа
                        Margin = new Thickness(x, y, 0, 0)
                    };
                    graphCanvas.Children.Add(rect);

                    // Добавить текстовый блок для значения над столбцом
                    TextBlock textBlock = new TextBlock
                    {
                        Text = array[i].ToString(),
                        Foreground = Brushes.Black,
                        FontSize = 12,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(x + (barWidth - 10) / 2, y - 10, 0, 0)
                    };

                    graphCanvas.Children.Add(textBlock);
                }
            }
        }

        private void DrawGraph()
        {
            canvas.Children.Clear(); // Очистить любой предыдущий график

            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;

            double barWidth = canvasWidth / array.Length;
            double maxValue = array.Max();

            for (int i = 0; i < array.Length; i++)
            {
                // Вычисление размеров столбца и его позиции
                double barHeight = (array[i] / maxValue) * canvasHeight;
                double x = i * barWidth;
                double y = canvasHeight - barHeight;

                // Создание и добавление прямоугольника, представляющего столбец
                Rectangle rect = new Rectangle
                {
                    Width = barWidth - 1,
                    Height = barHeight,
                    Fill = new SolidColorBrush(Color.FromRgb(65, 105, 225)),
                    Margin = new Thickness(x, y, 0, 0)
                };
                canvas.Children.Add(rect);

                // Добавление текстового блока для значения над столбцом
                TextBlock textBlock = new TextBlock
                {
                    Text = array[i].ToString(),
                    Foreground = Brushes.Black,
                    FontSize = 12,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(x + (barWidth - 20) / 2, y - 20, 0, 0)
                };

                canvas.Children.Add(textBlock);
            }
        }

        private void Bukav ( object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if(!char.IsDigit(e.Text,0))
            {
                e.Handled = true;
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
            {

                bool isOk = int.TryParse(txtSize.Text, out int size);

                Random random = new Random();

                ArrayItems.Clear();
                if (isOk)
                {
                if (size < 39 && size > 0)
                {
                    array = new int[size];

                    for (int i = 0; i < size; i++)
                    {
                        array[i] = random.Next(1, 10);
                        ArrayItems.Add(new ArrayItem(array[i]));
                    }

                    DrawGraph();

                    NumberSequenceAnalyzer analyzer = new NumberSequenceAnalyzer();
                    int countEqualNumbers = analyzer.CountEqualNumbers(array);
                    MessageBox.Show($"Количество чисел, равных предыдущему: {countEqualNumbers}");

                    DrawEqualNumbersGraph();
                }
                else MessageBox.Show("Вы ввели значение больше 39 или 0!");
            }
                else
                    MessageBox.Show("Вы ввели неккоректное значение!");
            }

        private void focus(object sender, RoutedEventArgs e)
        {
            txtSize.Focus();
        }
    }

    public class ArrayItem
        {

            public int Value { get; set; }

            public ArrayItem(int value)
            {

                Value = value;
            }
        }
    }

