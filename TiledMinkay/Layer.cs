using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMinkay
{
    public class Layer
    {
        public string Name { get; set; }

        //Phased out
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public float Opacity { get; set; }
        public bool Visable { get; set; }

        // Can contain: properties, data
    }
}
