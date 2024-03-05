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
        public static Settings settings = new Settings();

        public MainWindow()
        {
            InitializeComponent();
            UpdateElementSettings();
        }

        void UpdateElementSettings()
        {
            this.InputField.FontSize=settings.fontScale;
            this.InputField.Foreground=settings.foreground;
            this.InputField.FontFamily=settings.fontFamily;
            if (settings.IsImageBrush)
                this.InputField.Background = settings.imageBackground;
            else
                this.InputField.Background = settings.solidBackground;
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
            SettingsWindow.settings = settings;
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
            settings = SettingsWindow.settings;

            UpdateElementSettings();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            settings.SaveSettings();
        }
    }
}
