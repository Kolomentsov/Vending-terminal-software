using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CHW1.Assets;
using CHW1.Models;

namespace CHW1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Helper _helper = new Helper();

        private readonly MoneyWindow _moneyWindow = null;

        public MainWindow()
        {
            InitializeComponent();

            Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            Title = "VM";

            Closing += MainWindow_Closing;

           

            try
            {
                _helper.InitApp();

                _moneyWindow = new MoneyWindow(_helper);

                _moneyWindow.Show();

                var data = _helper.Data;

                var mainRows = data.Items.Count() / 3;

                var additionalRows = data.Items.Count() % 3 > 0 ? 1 : 0;

                var rowCount = mainRows + additionalRows;

                for (var i = 0; i < rowCount; i++)
                {
                    var gridRow = new RowDefinition
                    {
                        Height = new GridLength(80)
                    };

                    DataGrid.RowDefinitions.Add(gridRow);
                }

                var items = data.Items.ToArray();

                for (var i = 0; i < items.Length; i++)
                {
                    var button = new Button
                    {
                        Content = $"{items[i].Name}{Environment.NewLine}{items[i].Price} руб.",
                        Margin = new Thickness(5, 5, 5, 5),
                        Tag = items[i]
                    };

                    button.Click += Button_Click;

                    Grid.SetRow(button, i / 3);
                    Grid.SetColumn(button, i % 3);

                    DataGrid.Children.Add(button);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
       

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _moneyWindow.Close();

            _helper.SaveDatum();
        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Label.Content = e.Exception.Message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = (Item)((Button)sender).Tag;

                _helper.ChooseItem(item);

                _moneyWindow.Label.Content = $"Баланс: {_helper.Data.UserMoney} рублей";
            }
            catch (Exception ex)
            {
                Label.Content = ex.Message;
            }
        }

        private void HandleError(Exception ex)
        {
            Label.Content = ex.Message;
        }
       

    }
}
