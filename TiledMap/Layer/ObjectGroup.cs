using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMap.Object;

namespace TiledMap.Layer
{
    public class ObjectGroup
    {
        // What the heck is this
        public string DrawOrder { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
        public float Opacity { get; set; }
        public bool Visable { get; set; }

        public List<MapObject> Objects { get; set; }
        public List<Property> Properties { get; set; }

        // Can contain: properties, object
    }
}
