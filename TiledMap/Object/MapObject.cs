using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMap.Object
{
    /// <summary>
    /// Also called an object in tiled
    /// </summary>
    public class MapObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Rotation { get; set; }
        public int GID { get; set; } // reference to a tile
        public bool Visable { get; set; }
        public bool Ellipse { get; set; } // ends up being a child tag in the xml

        public List<Property> Properties { get; set; }
        public Polygon PolygonObject { get; set; }
        public Polyline PolylineObject { get; set; }
        public Image Img { get; set; }

        // Can contain: ellipse (since 0.9.0), polygon, polyline, image
    }
}
