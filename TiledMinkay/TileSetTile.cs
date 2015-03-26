using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMinkay
{
    public class TileSetTile
    {
        public int ID { get; set; }
        public int TerrainTopLeft { get; set; }
        public int TerrainTopRight { get; set; }
        public int TerrainBottemLeft { get; set; }
        public int TerrainBottemRight { get; set; }
        public float Probability { get; set; }
        public Image TileImage { get; set; }

        //Can contain: properties, image (since 0.9.0), objectgroup (since 0.10.0)
    }
}
