﻿using Microsoft.Win32;
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
        static public Settings settings;
        public SettingsWindow()
        {
            InitializeComponent();
            LoadColorsForComboBox();
            this.SizeTextBox.Text = this.TextSample.FontSize.ToString();
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

            this.ColorComboBox.SelectedItem = "Black";
            this.BackgroundColorComboBox.SelectedItem = "White";
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

        private void SizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsInitialized)
            {
                try
                {
                    byte fontSize=Convert.ToByte(this.SizeTextBox.Text);
                    if (fontSize >= 2 && fontSize <= 96)
                        this.TextSample.FontSize = fontSize;
                }
                catch(Exception) { }
            }
        }

        private void OpenImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if((bool)openFileDialog.ShowDialog())
            {
                string FullName=openFileDialog.FileName;
                if(FullName.EndsWith(".png") || FullName.EndsWith(".jpg") || FullName.EndsWith(".bmp"))
                {
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage image = new BitmapImage(new Uri(FullName));
                    imageBrush.ImageSource = image;
                    imageBrush.Viewport=new Rect(0,0,image.Width,image.Height);
                    imageBrush.ViewportUnits = BrushMappingMode.Absolute;

                    this.Sketch.Fill= imageBrush;
                    this.PathInfo.Text=FullName;
                }
                else
                    MessageBox.Show("Неверный формат!","Ошибка!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            PaveCheckBox_Checked(null, null);
        }

        private void PaveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.PathInfo.Text!=null)
                if((bool)this.PaveCheckBox.IsChecked)
                {
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage image = new BitmapImage(new Uri(this.PathInfo.Text));
                    imageBrush.ImageSource = image;
                    imageBrush.TileMode = TileMode.Tile;
                    imageBrush.Viewport = new Rect(0, 0, image.Width, image.Height);
                    imageBrush.ViewportUnits = BrushMappingMode.Absolute;

                    this.Sketch.Fill = imageBrush;
                }
                else
                {
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage image = new BitmapImage(new Uri(PathInfo.Text));
                    imageBrush.ImageSource = image;

                    this.Sketch.Fill = imageBrush;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка!",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorString = ((ComboBox)(sender)).SelectedValue.ToString();
            var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorString);
            this.TextSample.Foreground = new SolidColorBrush(color);
        }

        private void BackgroundColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorString = ((ComboBox)(sender)).SelectedValue.ToString();
            var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorString);
            this.TextSample.Background = new SolidColorBrush(color);
        }
    }
}
