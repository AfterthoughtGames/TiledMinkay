using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMinkay
{
    public class TileSet
    {
        public int FirstGrid { get; set; }
        public string Source { get; set; }

        public string Name { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int Spacing { get; set; }
        public int Margin { get; set; }

        public List<Terrain> TerrainTypes { get; set; }
        public List<TileSetTile> Tiles { get; set; }

        //Can contain: tileoffset (since 0.8.0), properties (since 0.8.0), image, terraintypes (since 0.9.0), tile
    }
}
