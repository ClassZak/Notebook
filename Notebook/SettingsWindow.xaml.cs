using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static Settings settings;

        public SettingsWindow()
        {
            InitializeComponent();
            LoadColorsForComboBox();
            EnableSettings();
            this.SizeTextBox.Text = this.Sketch.FontSize.ToString();
            this.ImageRadio.IsChecked = settings.IsImageBrush;
        }



        private void EnableSettings()
        {
            
            this.Sketch.Foreground=settings.foreground;
            this.Sketch.FontFamily=settings.fontFamily;
            this.Sketch.FontSize=settings.fontScale;
            int i = 0;
            foreach(var f in this.FontMenu.Items)
            {
                
                string fstr = (f.ToString()).Substring(f.ToString().IndexOf(':')+2);
                string setfstr = settings.fontFamily.ToString();
                if(setfstr == fstr)
                {
                    this.FontMenu.SelectedIndex = i;
                    break;
                }
                ++i;
            }

            if(settings.IsImageBrush)
            {
                this.ImageRadio.IsChecked=true;
                this.Sketch.Background=settings.imageBackground;
            }
            else
            {
                this.SolidBrusnRadio.IsChecked = true;
                this.Sketch.Background=settings.solidBackground;
            }
            this.PaveCheckBox.IsChecked = settings.IsStretch;
        }




        private void LoadColorsForComboBox()
        {
            foreach (var prop in typeof(System.Windows.Media.Colors).GetProperties())
            {
                System.Windows.Media.Color color = 
                    (System.Windows.Media.Color)prop.GetValue(null, null);
                ColorComboBox.Items.Add
                    (prop.Name);
                BackgroundColorComboBox.Items.Add
                    (prop.Name);
            }
        }

        private void FontMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsInitialized)
            {
                switch(this.FontMenu.SelectedIndex)
                {
                    case 0:
                        this.Sketch.FontFamily = new System.Windows.Media.FontFamily("Consolas");
                        break;
                    case 1:
                        this.Sketch.FontFamily = new System.Windows.Media.FontFamily("Impact");
                        break;
                    case 2:
                        this.Sketch.FontFamily = new System.Windows.Media.FontFamily("Arial");
                        break;
                    case 3:
                        this.Sketch.FontFamily = new System.Windows.Media.FontFamily("Wingdings");
                        break;
                }
                settings.fontFamily = this.Sketch.FontFamily;
            }
            
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(this.IsInitialized)
            {
                Sketch.FontSize = (uint)((Slider)(sender)).Value;
                this.SizeTextBox.Text= ((uint)((Slider)(sender)).Value).ToString();
                settings.fontScale = (uint)Sketch.FontSize;
            }
        }

        private void SizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsInitialized)
            {
                try
                {
                    byte fontSize=Convert.ToByte(this.SizeTextBox.Text);
                    if (fontSize >= 2 && fontSize <= 96)
                    {
                        this.Sketch.FontSize = fontSize;
                        settings.fontScale=fontSize;
                    }
                        
                }
                catch(Exception) { }
            }
        }



        private void OpenImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            
            bool succesSelected = false;
            while (!succesSelected)
            if ((bool)openFileDialog.ShowDialog())
            {
                string FullName = openFileDialog.FileName;
                if (FullName.EndsWith(".png") || FullName.EndsWith(".jpg") || FullName.EndsWith(".bmp"))
                {
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage image = new BitmapImage(new Uri(FullName));
                    if ((bool)this.PaveCheckBox.IsChecked)
                    {
                        imageBrush.ImageSource = image;
                        imageBrush.TileMode = TileMode.Tile;
                        imageBrush.Viewport = new Rect(0, 0, image.Width, image.Height);
                        imageBrush.ViewportUnits = BrushMappingMode.Absolute;
                    }
                    else
                    {
                        imageBrush.ImageSource = image;
                    }
                    this.Sketch.Background = imageBrush;
                    settings.imageBackground = this.Sketch.Background;

                    this.Sketch.Background = imageBrush;
                    this.PathInfo.Text = FullName;
                    succesSelected = true;
                }
                else
                    MessageBox.Show("Неверный формат!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.SolidBrusnRadio.IsChecked = true;
                succesSelected = true;
                return;
            }

                
            PaveCheckBox_Checked(null, null);
        }

        private void PaveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!settings.IsImageBrush)
                return;
            //OpenImageButton_Click(OpenImageButton, null);
            settings.IsImageBrush = true;
            this.ImageRadio.IsChecked = true;
            try
            {
                if (this.PathInfo.Text!=null)
                {
                    if(settings.imageBackground==null)
                    {
                        if((bool)this.PaveCheckBox.IsChecked)
                        {
                            ImageBrush imageBrush = new ImageBrush();
                            BitmapImage image = new BitmapImage(new Uri(this.PathInfo.Text));
                            imageBrush.ImageSource = image;
                            imageBrush.TileMode = TileMode.Tile;
                            imageBrush.Viewport = new Rect(0, 0, image.Width, image.Height);
                            imageBrush.ViewportUnits = BrushMappingMode.Absolute;

                            this.Sketch.Background = imageBrush;
                        }
                        else
                        {
                            ImageBrush imageBrush = new ImageBrush();
                            BitmapImage image = new BitmapImage(new Uri(PathInfo.Text));
                            imageBrush.ImageSource = image;

                            this.Sketch.Background = imageBrush;
                        }
                    }
                    else
                    {
                        if ((bool)this.PaveCheckBox.IsChecked)
                        {
                            ImageBrush imageBrush = (ImageBrush)settings.imageBackground;
                            imageBrush.TileMode=TileMode.Tile;
                            imageBrush.Viewport=new Rect(0,0,imageBrush.ImageSource.Width,imageBrush.ImageSource.Height);
                            imageBrush.ViewportUnits= BrushMappingMode.Absolute;

                            this.Sketch.Background = imageBrush;
                        }
                        else
                        {
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = ((ImageBrush)(settings.imageBackground)).ImageSource;
                            
                            this.Sketch.Background = imageBrush;
                        }
                    }
                    settings.IsStretch = (bool)this.PaveCheckBox.IsChecked;
                    settings.imageBackground = this.Sketch.Background;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка!",MessageBoxButton.OK, MessageBoxImage.Error);
                OpenImageButton_Click(OpenImageButton, null);
            }
        }


        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorString = ((ComboBox)(sender)).SelectedValue.ToString();
            var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorString);
            this.Sketch.Foreground= new SolidColorBrush(color);
            if(settings!=null)
            settings.foreground= new SolidColorBrush(color);
        }
        private void BackgroundColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorString = ((ComboBox)(sender)).SelectedValue.ToString();
            settings.IsImageBrush = false;
            var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorString);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            if (settings != null)
                settings.solidBackground = solidColorBrush;


            this.SolidBrusnRadio.IsChecked = true;
            this.Sketch.Background = solidColorBrush;
        }



        private void RadioButtonImageBrush_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
            {
                settings.IsImageBrush = true;
                if (settings.imageBackground == null)
                    OpenImageButton_Click(null, null);
                else
                    this.Sketch.Background= settings.imageBackground;
            }
        }
        private void RadioButtonSolidBrush_Checked(object sender, RoutedEventArgs e)
        {
            if(this.IsInitialized)
            {
                settings.IsImageBrush = false;
                if (this.Sketch != null)
                this.Sketch.Background=settings.solidBackground;
            }
                
        }





        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            settings.IsImageBrush=(bool)this.ImageRadio.IsChecked;
        }
    }
}
