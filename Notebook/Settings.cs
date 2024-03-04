using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace Notebook
{
    public class Settings
    {
        public System.Windows.Media.FontFamily fontFamily;
        public uint fontScale;
        public System.Windows.Media.Brush foreground;
        public System.Windows.Media.Brush imageBackground;
        public System.Windows.Media.Brush solidBackground;

        public bool IsImageBrush = false;
        public bool IsStretch = false;

        public Settings()
        {
            if(!File.Exists("Settings.txt"))
            {
                fontFamily = new System.Windows.Media.FontFamily("Consolas");
                fontScale = 12;
                foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0,0,0));
                solidBackground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255,255,255));

                IsImageBrush = false;
                IsStretch = false;


                FileStream fileStream = new FileStream("Settings.txt", FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(fontFamily.ToString());
                streamWriter.WriteLine(fontScale.ToString());
                streamWriter.WriteLine(foreground.ToString());
                streamWriter.WriteLine("null");
                streamWriter.WriteLine(solidBackground.ToString());
                streamWriter.WriteLine(IsImageBrush.ToString());
                streamWriter.WriteLine(IsStretch.ToString());
                streamWriter.Close();

            }
            else
            {
                FileStream fileStream = new FileStream("Settings.txt", FileMode.Open, FileAccess.Read);
                StreamWriter streamWriter = new StreamWriter(fileStream);
            }
        }
    }
}
