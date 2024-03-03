using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        public Settings()
        {
            fontFamily = new System.Windows.Media.FontFamily("Consolas");
            fontScale = 12;
            foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0,0,0));
            solidBackground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255,255,255));

            IsImageBrush = false;
        }
    }
}
