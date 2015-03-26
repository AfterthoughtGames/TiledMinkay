using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMinkay
{
    public enum OrientationType
    {
        orthogonal, isometric, staggered
    }

    public enum RenderOrderTypes
    {
        RightDown, RightUp, LeftDown, LeftUp
    }

    public class Map
    {
        public string Version { get; set; }
        public OrientationType Orientation { get; set; }
        public RenderOrderTypes RenderType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public string BackgroundColor { get; set; }

        //Can contain: properties, tileset, layer, objectgroup, imagelayer
    }
}
