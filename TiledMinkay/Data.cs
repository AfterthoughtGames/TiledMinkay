using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMinkay
{
    public enum EncodingType
    {
        None, Base64, CSV
    }

    public enum CompressionType
    {
        gzip, zlib
    }

    public class Data
    {
        public EncodingType Encoding { get; set; }
        public CompressionType Compression { get; set; }
        List<Tile> Tiles { get; set; }

        //Can contain: tile
    }
}
