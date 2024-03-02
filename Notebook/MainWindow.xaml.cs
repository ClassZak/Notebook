using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Drawing;



namespace Notebook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    
    

    public partial class MainWindow : Window
    {
        const string MAIN_TITLE = "Блокнот";
        public MainWindow()
        {
            InitializeComponent();

            ImageBrush myBrush = new ImageBrush();
            BitmapImage myBitmap = new BitmapImage(new Uri(Directory.GetCurrentDirectory().ToString()+ @"\resources\Russia.jpg"));
            myBrush.ImageSource = myBitmap;
            myBrush.TileMode = TileMode.Tile;
            myBrush.Viewport = new Rect(0, 0, myBitmap.Width, myBitmap.Height);
            myBrush.ViewportUnits = BrushMappingMode.Absolute;

            this.InputField.Background = myBrush;

            SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(0));

            
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)(openFileDialog.ShowDialog()))
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                this.PathField.Text= fileInfo.FullName;
                this.Title = MAIN_TITLE + " - " + fileInfo.Name;
                this.InputField.Text=File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save a Text File";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, this.InputField.Text);
        }
        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
    }
}
