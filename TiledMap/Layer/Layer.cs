using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMap.Layer
{
    public class MapLayer
    {
        public string Name { get; set; }

        //Phased out
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public float Opacity { get; set; }
        public bool Visable { get; set; }

        public Data DataObject { get; set; }
        public List<Property> Properties { get; set; }
        public List<Tile> Tiles { get; set; }
        // Can contain: properties, data
    }
}
