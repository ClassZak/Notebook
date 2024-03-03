using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Notebook
{
    public struct Settings
    {
        public class FontSettings
        {
            public System.Windows.Media.FontFamily fontFamily;
            public uint FontScale;
        }
        public class ImageSetting
        {
            public TileMode tileMode = TileMode.None;
            public string ImageFullName = null;

            public bool ImageDrawing = false;
            public SolidBrush brush = null;
        }
        static public FontSettings fontSettings;
        static public ImageSetting imageSetting;

    }
}
