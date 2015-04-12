using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMap
{
    public class Tile
    {
        public static uint FLIPPED_HORIZONTALLY_FLAG = 0x80000000;
        public static uint FLIPPED_VERTICALLY_FLAG = 0x40000000;
        public static uint FLIPPED_DIAGONALLY_FLAG = 0x20000000;

        public int GID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool HorizontalFlip { get; set; }
        public bool VerticalFlip { get; set; }
        public bool DiagonalFlip { get; set; }
    }
}
