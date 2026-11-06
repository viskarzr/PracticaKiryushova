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

        public MainWindow()
        {
            InitializeComponent();      
        }

        int[] masFirst;
        int[] masSecond;
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbSize.Text, out int size) || size < 0 || size > 30)
            {
                MessageBox.Show("Ошибка при инициализации массива! Пожалуйста, проверьте введенные данные", "ОШИБКА!!!!!");
                tbSize.Clear();
                return;
            }
            masFirst = DataWork.CreateMas(size);
            dgFirst.ItemsSource = masFirst.ToDataTable().DefaultView;
            GraphRenderer.DrawGraph(csFirst, masFirst, Brushes.Blue, "Исходный массив");

            masSecond = null;
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
            tbSize.Clear();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная программа является заданием на учебную практику по ТРПО.\nВариант №10:\nРазработать программу для работы с одномерным массивом (размера М).\r\n Пользователь вводит «M». Далее, при помощи ГСЧ заполнить матрицу\r\n(есть возможность редактирования значений в ячейках матрицы).\r\nПоменять местами максимальный и минимальный элементы (через классы).\r\nСоздать графики на основании двух массивов и вывести их на экран. ", "Учебная практика");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgFirst_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexColumn = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            masFirst[indexColumn] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
            GraphRenderer.DrawGraph(csFirst, masFirst, Brushes.Blue, "Исходный массив");
        }

        private void btnSwap_Click(object sender, RoutedEventArgs e)
        {
            if (masFirst == null || masFirst.Length == 0)
            {
                MessageBox.Show("Сначала создайте массив!", "Внимание");
                return;
            }
            masSecond = DataWork.SwapMas(masFirst);
            dgSecond.ItemsSource = masSecond.ToDataTable().DefaultView;
            GraphRenderer.DrawGraph(csSecond, masSecond, Brushes.Red, "После замены Min/Max");
        }
    }
}