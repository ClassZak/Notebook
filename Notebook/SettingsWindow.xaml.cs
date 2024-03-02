using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    public partial class SettingsWindow : Window
    {
        
        public SettingsWindow()
        {
            InitializeComponent();
            this.BackgroundDemo.Source= new BitmapImage(new Uri(Directory.GetCurrentDirectory().ToString() + @"\resources\Russia.jpg"));
        }

        private void FontMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsInitialized)
            switch(this.FontMenu.SelectedIndex)
            {
                case 0:
                    this.TextSample.FontFamily = new System.Windows.Media.FontFamily("Consolas");
                    break;
                case 1:
                    this.TextSample.FontFamily = new System.Windows.Media.FontFamily("Impact");
                    break;
                case 2:
                    this.TextSample.FontFamily = new System.Windows.Media.FontFamily("Arial");
                    break;
                case 3:
                    this.TextSample.FontFamily = new System.Windows.Media.FontFamily("Wingdings");
                    break;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(this.IsInitialized)
            {
                TextSample.FontSize = (uint)((Slider)(sender)).Value;
                this.SizeTextBox.Text= ((uint)((Slider)(sender)).Value).ToString();
            }
                
        }
    }
}
