using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMap
{
    public enum FormatTypes
    {
        none, png, gif, jpg, bmp
    }

    public class Image
    {
        public FormatTypes Format { get; set; }
        public string Source { get; set; }
        public string Trans { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Data DataObject { get; set; }

    }
}
