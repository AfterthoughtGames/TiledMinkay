using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMap
{
    public class Terrain
    {
        public string Name { get; set; }
        public int TileID { get; set; }

        public List<Property> Properties { get; set; }

        // Can contain: properties
    }
}
