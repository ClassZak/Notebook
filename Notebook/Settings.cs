using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Notebook
{
    public class TextBoxSettings
    {
        public System.Windows.Media.FontFamily fontFamily;
        public uint fontScale;
        public System.Windows.Media.Brush foreground;
        public System.Windows.Media.Brush imageBackground;
        public System.Windows.Media.Brush solidBackground;

        public bool IsImageBrush = false;
        public bool IsStretch = true;
        public string imagePath=null;

        const string FileName = "TextBoxSettings.txt";
        public void LoadSettings()
        {
            if(!File.Exists(FileName))
            {
                fontFamily = new System.Windows.Media.FontFamily("Consolas");
                fontScale = 12;
                foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0,0,0));
                solidBackground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255,255,255));

                IsImageBrush = false;
                IsStretch = true;


                FileStream fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.WriteLine(fontFamily.ToString());
                writer.WriteLine(fontScale.ToString());
                writer.WriteLine(foreground.ToString());
                writer.WriteLine(imagePath);
                writer.WriteLine(solidBackground.ToString());
                writer.WriteLine(IsImageBrush);
                writer.WriteLine(IsStretch);
                writer.Close();
            }
            else
            {
                FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                fontFamily = new System.Windows.Media.FontFamily(streamReader.ReadLine());
                fontScale=Convert.ToUInt32(streamReader.ReadLine());
                foreground=
                    new SolidColorBrush
                    (
                        (System.Windows.Media.Color)
                        System.Windows.Media.ColorConverter.ConvertFromString(streamReader.ReadLine()
                    )
                );
                imagePath=streamReader.ReadLine();
                BitmapImage image;
                    

                solidBackground=
                    new SolidColorBrush
                    (
                        (System.Windows.Media.Color)
                        System.Windows.Media.ColorConverter.ConvertFromString(streamReader.ReadLine()
                    )
                );
                IsImageBrush=Convert.ToBoolean(streamReader.ReadLine());
                IsStretch=Convert.ToBoolean(streamReader.ReadLine());






                if (imagePath.Length > 0)
                {
                    try
                    {
                        try
                        {
                            image = new BitmapImage(new Uri(imagePath));
                        }
                        catch (Exception)
                        {
                            imagePath = Path.Combine(Directory.GetCurrentDirectory().ToString(),imagePath);
                            image =new BitmapImage(new Uri(imagePath));
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Неверные настройки!", MessageBoxButton.OK, MessageBoxImage.Error);
                        throw;
                    }
                    
                    
                    ImageBrush imageBrush = new ImageBrush(image);
                    if(!IsStretch)
                    {
                        imageBrush.TileMode = TileMode.Tile;
                        imageBrush.Viewport = new Rect(0, 0, image.Width, image.Height);
                        imageBrush.ViewportUnits = BrushMappingMode.Absolute;
                    }
                    imageBackground = imageBrush;
                }
                else
                {
                    imageBackground = null;
                }
                streamReader.Close();
            }
        }
        public void SaveSettings()
        {
            FileStream fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine(fontFamily.ToString());
            writer.WriteLine(fontScale.ToString());
            writer.WriteLine(foreground.ToString());
            writer.WriteLine(imagePath);
            writer.WriteLine(solidBackground.ToString());
            writer.WriteLine(IsImageBrush);
            writer.WriteLine(IsStretch);
            writer.Close();
        }
        public TextBoxSettings()
        {
            try
            {
                LoadSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Неверные настройки!", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
    }




    public class MusicSettings
    {
        const string FileName = "MusicSettings.txt";


        public bool IsPlaying = false;
        public bool IsPausing = true;
        public string MusicPath;
        public double MusicVolume = 1;
        public TimeSpan MusicPosition = TimeSpan.Zero;
        
        public MusicSettings()
        {
            if(File.Exists(FileName))
            {
                try
                {
                    FileStream fileStream=new FileStream(FileName,FileMode.Open, FileAccess.Read);
                    StreamReader streamReader= new StreamReader(fileStream);
                    IsPlaying=bool.Parse(streamReader.ReadLine());
                    IsPausing=bool.Parse(streamReader.ReadLine());
                    MusicPath=streamReader.ReadLine();
                    MusicVolume=double.Parse(streamReader.ReadLine());
                    MusicPosition= TimeSpan.FromMilliseconds(double.Parse(streamReader.ReadLine()));
                    streamReader.Close();
                }
                catch(Exception ex)
                {
                    CustomMessageBox.Show(ex.Message, "Необработанное исключение!");
                }
            }
            else
            {
                SaveSettings();
            }
        }
        public void SaveSettings()
        {
            if(!IsPlaying)
            {
                MusicPath = string.Empty;
                MusicPosition = TimeSpan.FromMilliseconds(0);
                IsPausing = true;
            }

            FileStream fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(IsPlaying);
            streamWriter.WriteLine(IsPausing);
            streamWriter.WriteLine(MusicPath);
            streamWriter.WriteLine(MusicVolume);
            streamWriter.WriteLine(MusicPosition.TotalMilliseconds);
            streamWriter.Close();
        }
    }
}
