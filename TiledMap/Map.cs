using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMap.Layer;
using TiledMap.TileSet;

namespace TiledMap
{
    public enum OrientationType
    {
        orthogonal, isometric, staggered
    }

    public enum RenderOrderTypes
    {
        RightDown, RightUp, LeftDown, LeftUp
    }

    /// <summary>
    /// 
    /// </summary>
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

        public List<MapTileSet> TileSets { get; set; }
        public List<Property> Properties { get; set; }
        public List<MapLayer> Layers { get; set; }
        public List<ImageLayer> ImageLayers { get; set; }
        public List<ObjectGroup> ObjectGroups { get; set; }
    }
}
