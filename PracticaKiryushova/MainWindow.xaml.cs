using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticaKiryushova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int[] _masFirst;
        private int[] _masSecond;

        private readonly Random _rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            btnSwap.Click += btnSwap_Click;
        }

        int[] masFirst;
        int[] masSecond;
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbSize.Text, out int size) || size < 0 || size > 30)
            {
                MessageBox.Show("Ошибка при инициализации массива! Пожалуйста, проверьте введенные данные (дуры)", "ОШИБКА!!!!!");
                tbSize.Clear();
                return;
            }

            _masFirst = new int[size];
            for (int i = 0; i < size; i++)
            {
                _masFirst[i] = _rnd.Next(-20, 30);
            }
            dgFirst.ItemsSource = _masFirst.ToDataTable().DefaultView;

            DrawGraph(csFirst, _masFirst, Brushes.Blue, "Исходный массив");

            _masSecond = null;
            dgSecond.ItemsSource = null;
            csSecond.Children.Clear();
        }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dgFirst.ItemsSource = null;
            masFirst = null;
            dgSecond.ItemsSource = null;
            masSecond = null;
            csFirst.Children.Clear();
            csSecond.Children.Clear();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная программа является заданием на учебную практику по ТРПО.\nВариант №10:\nРазработать программу для работы с одномерным массивом (размера М).\r\n Пользователь вводит «M». Далее, при помощи ГСЧ заполнить матрицу\r\n(естьвозможность редактирования значений в ячейках матрицы).\r\nПоменять местами максимальный и минимальный элементы (через классы).\r\nСоздать графики на основании двух массивов и вывести их на экран. ", "Учебная практика");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgFirst_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexColumn = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            masFirst[indexColumn] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
        }

        private void btnSwap_Click(object sender, RoutedEventArgs e)
        {
            if (_masFirst == null || _masFirst.Length == 0)
            {
                MessageBox.Show("Сначала создайте массив!", "Внимание");
                return;
            }

            _masSecond = (int[])_masFirst.Clone();
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < _masSecond.Length; i++)
            {
                if (_masSecond[i] < _masSecond[minIndex]) minIndex = i;
                if (_masSecond[i] > _masSecond[maxIndex]) maxIndex = i;
            }
            int temp = _masSecond[minIndex];
            _masSecond[minIndex] = _masSecond[maxIndex];
            _masSecond[maxIndex] = temp;
            dgSecond.ItemsSource = _masSecond.ToDataTable().DefaultView;
            DrawGraph(csSecond, _masSecond, Brushes.Red, "После замены Min/Max");
        }
    

      private void DrawGraph(Canvas canvas, int[] data, Brush color, string title)
        {
            canvas.Children.Clear();

            if (data == null || data.Length < 2) return;

            double width = canvas.ActualWidth;
            double height = canvas.ActualHeight;

            if (width <= 0) width = 479;
            if (height <= 0) height = 270;

            double marginLeft = 40;
            double marginBottom = 30;
            double marginTop = 20;
            double marginRight = 20;

            double plotWidth = width - marginLeft - marginRight;
            double plotHeight = height - marginBottom - marginTop;

            double minVal = data.Min();
            double maxVal = data.Max();
            double range = (maxVal - minVal == 0) ? 1 : (maxVal - minVal);


            var axisBrush = Brushes.Black;
            canvas.Children.Add(new Line
            {
                X1 = marginLeft,
                Y1 = height - marginBottom,
                X2 = marginLeft,
                Y2 = marginTop,
                Stroke = axisBrush,
                StrokeThickness = 2
            });

            canvas.Children.Add(new Line
            {
                X1 = marginLeft,
                Y1 = height - marginBottom,
                X2 = width - marginRight,
                Y2 = height - marginBottom,
                Stroke = axisBrush,
                StrokeThickness = 2
            });

            int steps = 5;
            double stepValue = range / steps;
            for (int i = 0; i <= steps; i++)
            {
                double currentVal = minVal + (i * stepValue);
                double percent = (currentVal - minVal) / range;
                double yPos = (height - marginBottom) - (percent * plotHeight);

                canvas.Children.Add(new Line
                {
                    X1 = marginLeft - 5,
                    Y1 = yPos,
                    X2 = marginLeft,
                    Y2 = yPos,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1
                });


                var label = new TextBlock
                {
                    Text = currentVal.ToString("F1"),
                    FontSize = 10,
                    Foreground = Brushes.Black
                };
                Canvas.SetLeft(label, marginLeft - 35);
                Canvas.SetTop(label, yPos - 6);
                canvas.Children.Add(label);
            }

            var points = new PointCollection();
            for (int i = 0; i < data.Length; i++)
            {
                double x = marginLeft + (i * plotWidth) / (data.Length - 1);
                double percentY = (data[i] - minVal) / range;
                double y = (height - marginBottom) - (percentY * plotHeight);

                points.Add(new Point(x, y));
            }

            canvas.Children.Add(new Polyline
            {
                Points = points,
                Stroke = color,
                StrokeThickness = 2,
                SnapsToDevicePixels = true
            });

            var titleText = new TextBlock
            {
                Text = title,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.DarkBlue
            };
            Canvas.SetLeft(titleText, marginLeft);
            Canvas.SetTop(titleText, 0);
            canvas.Children.Add(titleText);
        }
    }
}