using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AppCor
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const int N_ROWS = 10, N_COLS = 8;
        int counter = 0;

        public ArrayModel array;
        public MainPage()
        {
            this.InitializeComponent();


            for (int i = 0; i < N_ROWS; i++)
            {
                numsGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < N_COLS; i++)
            {
                numsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            array = new ArrayModel(N_COLS * N_ROWS);
            MakeArrayView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            array = new ArrayModel(N_COLS * N_ROWS);
            MakeArrayView();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (array == null)
            {
                MessageBox("Массив не задан!");
                return;
            }
            numsGrid.ChildrenTransitions = null;
            if (counter == 0)
            {
                array.SortArray();

            }
            else if (counter % 2 == 1)
            {
                try
                {
                    int num = Convert.ToInt32(numberField.Text);
                    array.findValue = new ColoredNumber(num);
                    numberField.IsReadOnly = true;
                }
                catch (System.FormatException)
                {
                    MessageBox("Число не введено!");
                    counter = 0;
                    return;
                }
                array.GetNums()[array.GetMidIndex()].color = array.GetNums()[array.GetMidIndex()].midColor;
            }
            else if (counter % 2 == 0)
            {
                try
                {
                    bool res = array.BinaryRecolor();

                    if (res)
                    {
                        counter = 0;
                        numberField.IsReadOnly = false;
                        MessageBox("Поиск окончен!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox(ex.Message);
                    counter = 0;
                    numberField.IsReadOnly = false;
                    return;
                }
            }

            MakeArrayView();
            counter++;
        }

        private async void MessageBox(string msg)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = msg;
            dialog.PrimaryButtonText = "Ok";
            await dialog.ShowAsync();
        }

        private void MakeArrayView()
        {
            if (array == null)
            {
                return;
            }

            numsGrid.Children.Clear();
            for (int i = 0; i < N_ROWS; i++)
            {
                for (int j = 0; j < N_COLS; j++)
                {
                    var number = array.GetNums()[i * N_COLS + j];
                    var colorStr = number.color;
                    var r = (byte)System.Convert.ToUInt32(colorStr.Substring(0, 2), 16);
                    var g = (byte)System.Convert.ToUInt32(colorStr.Substring(2, 2), 16);
                    var b = (byte)System.Convert.ToUInt32(colorStr.Substring(4, 2), 16);
                    Button btn = new Button()
                    {
                        Content = $"{number.value}",
                        Margin = new Thickness(2),
                        Width = 100,
                        Height = 100,
                        Background = new SolidColorBrush(Color.FromArgb(255, r, g, b))
                    };
                    numsGrid.Children.Add(btn);
                    Grid.SetColumn(btn, j);
                    Grid.SetRow(btn, i);
                }
            }
        }
    }
}
