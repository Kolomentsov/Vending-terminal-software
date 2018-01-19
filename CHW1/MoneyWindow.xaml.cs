using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CHW1.Assets;
using CHW1.Models;

namespace CHW1
{
    /// <summary>
    /// Interaction logic for MoneyWindow.xaml
    /// </summary>
    internal partial class MoneyWindow : Window
    {
        private Helper Helper { get; set; }
        

        internal MoneyWindow(Helper helper)
        {
            InitializeComponent();

            try
            {
                Helper = helper;

                var data = Helper.Data;

                Label.Content = $"Баланс: {data.UserMoney} рублей";

                var mainRows = data.Cashes.Count() / 3;

                var additionalRows = data.Cashes.Count() % 3 > 0 ? 1 : 0;

                var rowCount = mainRows + additionalRows;

                for (var i = 0; i < rowCount; i++)
                {
                    var gridRow = new RowDefinition
                    {
                        Height = new GridLength(80)
                    };

                    DataGrid.RowDefinitions.Add(gridRow);
                }

                var cashes = data.Cashes.ToArray();

                for (var i = 0; i < cashes.Length; i++)
                {
                    var button = new Button
                    {
                        Content = $"{cashes[i].Value} руб.",
                        Margin = new Thickness(5, 5, 5, 5),
                        Tag = cashes[i]
                    };

                    button.Click += Button_Click; ;

                    Grid.SetRow(button, i / 3);
                    Grid.SetColumn(button, i % 3);

                    DataGrid.Children.Add(button);
                }

                Button.Click += GiveChange_Click;
            }
            catch (Exception ex)
            {
                Label.Content = ex.Message;
            }
        }

        private void GiveChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Helper.GiveChange();

                Label.Content = $"Баланс: {Helper.Data.UserMoney} рублей";
            }
            catch (Exception ex)
            {
                Label.Content = ex.Message;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cash = (Cash)((Button)sender).Tag;

                Helper.InsertMoney(cash);

                Label.Content = $"Баланс: {Helper.Data.UserMoney} рублей";
            }
            catch (Exception ex)
            {
                Label.Content = ex.Message;
            }
        }
    }
}
