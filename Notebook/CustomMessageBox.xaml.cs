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
using System.Windows.Shapes;

namespace Notebook
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public static MessageBoxResult Show(string message, string caption)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(message, caption);
            customMessageBox.ShowDialog();
            return customMessageBox.MessageBoxResult;
        }
        public static MessageBoxResult Show(string message="",string caption="", MessageBoxButton messageBoxButton=MessageBoxButton.OK)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(message, caption, messageBoxButton);
            customMessageBox.ShowDialog();
            return customMessageBox.MessageBoxResult;
        }




        public MessageBoxResult MessageBoxResult = MessageBoxResult.None;
        public bool dialogResult = false;
        bool YesNoMode = false;
        public CustomMessageBox(string message = "", string caption = "", MessageBoxButton messageBoxButton = MessageBoxButton.OK)
        {
            InitializeComponent();
            EnableText(message, caption);
            switch (messageBoxButton)
            {
                case MessageBoxButton.OK:
                    {
                        this.YesButton.Content = "OK";
                        this.NoButton.IsEnabled = this.CancelButton.IsEnabled = false;
                        this.NoButton.Visibility=this.CancelButton.Visibility=Visibility.Collapsed;

                        Grid.SetColumnSpan(this.YesButton, 6);
                        YesNoMode=false;
                        break;
                    }
                case MessageBoxButton.YesNo:
                    {
                        this.CancelButton.IsEnabled = false;
                        this.CancelButton.Visibility=Visibility.Collapsed;
                        this.YesButton.Width = this.NoButton.Width = 150;

                        Grid.SetColumnSpan(this.YesButton, 3);
                        Grid.SetColumn(this.NoButton, 3);
                        Grid.SetColumnSpan(this.NoButton, 3);
                        YesNoMode = true;
                        break;
                    }
                case MessageBoxButton.YesNoCancel:
                    YesNoMode = true;
                    break;
                case MessageBoxButton.OKCancel:
                    {
                        this.YesButton.Content = "OK";
                        this.NoButton.IsEnabled = false;
                        this.NoButton.Visibility = Visibility.Collapsed;
                        this.YesButton.Width = this.CancelButton.Width=150;

                        Grid.SetColumnSpan(this.YesButton, 3);
                        Grid.SetColumn(this.CancelButton, 3);
                        Grid.SetColumnSpan(this.CancelButton, 3);
                        YesNoMode = false;
                        break;
                    }
            }
        }




        private void EnableText(string message, string caption)
        {
            this.MessageTextBlock.Text = message;
            this.Title = caption;
        }
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            dialogResult = true;
            this.MessageBoxResult = (YesNoMode) ? MessageBoxResult.Yes : MessageBoxResult.OK;
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            dialogResult = true;
            this.MessageBoxResult = (YesNoMode) ? MessageBoxResult.No : MessageBoxResult.OK;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            dialogResult = false;
            this.MessageBoxResult = MessageBoxResult.Cancel;
        }
    }
}
