using System;
using TiledMap;
using TiledMap.TileSet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using TiledMap.Layer;
using TiledMap.Object;
using System.IO;
using System.IO.Compression;

namespace TiledMinkay
{
    // Load / Save Maps and TileSets
    public class MapIO
    {
        public static Map LoadTiledMap(string fileNameAndPath)
        {
            XmlDocument docObj = new XmlDocument();
            docObj.Load(fileNameAndPath);

            XmlElement mapRoot = docObj["map"];

            #region MapValues
            Map mapToReturn = new Map();
            mapToReturn.Version = mapRoot.GetAttribute("version");
            
            switch(mapRoot.GetAttribute("orientation"))
            {
                case "orthogonal":
                    mapToReturn.Orientation = OrientationType.orthogonal;
                    break;
                case "isometric":
                    mapToReturn.Orientation = OrientationType.isometric;
                    break;
                case "staggered":
                    mapToReturn.Orientation = OrientationType.staggered;
                    break;
                default:
                    throw new Exception("Unsupported Map Orientation Type: " + mapRoot.GetAttribute("orientation"));
            }

            //RightDown, RightUp, LeftDown, LeftUp
            switch(mapRoot.GetAttribute("renderorder"))
            {
                case "right-down":
                    mapToReturn.RenderType = RenderOrderTypes.RightDown;
                    break;
                case "right-up":
                    mapToReturn.RenderType = RenderOrderTypes.RightUp;
                    break;
                case "left-down":
                    mapToReturn.RenderType = RenderOrderTypes.LeftDown;
                    break;
                case "left-up":
                    mapToReturn.RenderType = RenderOrderTypes.LeftUp;
                    break;
                default:
                    throw new Exception("Unsupported Map Render Order: " + mapRoot.GetAttribute("renderorder"));
            }

            mapToReturn.Width = Convert.ToInt32(mapRoot.GetAttribute("width"));
            mapToReturn.Height = Convert.ToInt32(mapRoot.GetAttribute("height"));
            mapToReturn.TileWidth = Convert.ToInt32(mapRoot.GetAttribute("tilewidth"));
            mapToReturn.TileHeight = Convert.ToInt32(mapRoot.GetAttribute("tileheight"));

            // optional attributes
            if (mapRoot.HasAttribute("backgroundcolor"))
            {
                mapToReturn.BackgroundColor = mapRoot.GetAttribute("backgroundcolor");
            }
            #endregion

            foreach(XmlElement currentChildElement in mapRoot.ChildNodes)
            {
                if(currentChildElement.Name == "tileset")
                {
                    mapToReturn.TileSets.Add(LoadTiledTileset(currentChildElement));

                }

                if(currentChildElement.Name == "properties")
                {
                    mapToReturn.Properties = LoadProperties(currentChildElement);
                }

                if(currentChildElement.Name == "objectgroup")
                {
                    mapToReturn.ObjectGroups.Add(LoadObjectGroup(currentChildElement));
                }

                if(currentChildElement.Name == "imagelayer")
                {
                    mapToReturn.ImageLayers.Add(LoadImageLayer(currentChildElement));
                }

            }

            return mapToReturn;
        }

        public static void SaveTiledMap(Map mapToSave)
        {
            // TODO: Finish save Tiled Map
            throw new Exception("Not yet implanted");
        }

        public static MapTileSet LoadTiledTileset(string fileAndPath)
        {
            // TODO: Finish load Tiled Tileset from file

            throw new Exception("Not yet implanted");
        }

        public static MapTileSet LoadTiledTileset(XmlElement tileSetElement)
        {
            MapTileSet tileSetToReturn = new MapTileSet();
            tileSetToReturn.FirstGrid = Convert.ToInt32(tileSetElement.GetAttribute("firstgid"));
            tileSetToReturn.Name = tileSetElement.GetAttribute("name");
            tileSetToReturn.TileWidth = Convert.ToInt32(tileSetElement.GetAttribute("tilewidth"));
            tileSetToReturn.TileHeight = Convert.ToInt32(tileSetElement.GetAttribute("tileheight"));

            foreach(XmlElement currentEle in tileSetElement.ChildNodes)
            {
                #region Image
                if (currentEle.Name == "image")
                {
                    tileSetToReturn.Images.Add(LoadImage(currentEle));
                }
                #endregion

                if(currentEle.Name == "tile")
                {
                    tileSetToReturn.Tiles.Add(LoadTileSetTile(currentEle));
                }

                if(currentEle.Name == " tileoffset")
                {
                    tileSetToReturn.Offset = LoadOffSet(currentEle);
                }

                if(currentEle.Name == "properties")
                {
                    tileSetToReturn.Properties = LoadProperties(currentEle);
                }

                if(currentEle.Name == "image")
                {
                    tileSetToReturn.Images.Add(LoadImage(currentEle));
                }

                if(currentEle.Name == "terraintypes")
                {
                    tileSetToReturn.TerrainTypes = LoadTerrainTypes(currentEle);
                }
            }

            return tileSetToReturn;
        }

        private static ImageLayer LoadImageLayer(XmlElement imageLayerEle)
        {
            ImageLayer layerToReturn = new ImageLayer();
            layerToReturn.Name = imageLayerEle.GetAttribute("name");
            layerToReturn.X = Convert.ToInt32(imageLayerEle.GetAttribute("x"));
            layerToReturn.Y = Convert.ToInt32(imageLayerEle.GetAttribute("y"));
            layerToReturn.Width = Convert.ToInt32(imageLayerEle.GetAttribute("width"));
            layerToReturn.Height = Convert.ToInt32(imageLayerEle.GetAttribute("height"));
            layerToReturn.Opacity = float.Parse(imageLayerEle.GetAttribute("opacity"));
            layerToReturn.Visable = Convert.ToBoolean(imageLayerEle.GetAttribute("visible"));

            foreach(XmlElement currentEle in imageLayerEle)
            {
                if(currentEle.Name == "properties")
                {
                    layerToReturn.Properties = LoadProperties(currentEle);
                }

                if(currentEle.Name == "image")
                {
                    layerToReturn.Img = LoadImage(currentEle);
                }
            }

            return layerToReturn;
        }

        private static MapLayer LoadLayer(XmlElement mapLayerEle)
        {
            MapLayer layer = new MapLayer();
            layer.Name = mapLayerEle.GetAttribute("name");
            layer.X = Convert.ToInt32(mapLayerEle.GetAttribute("x"));
            layer.Y = Convert.ToInt32(mapLayerEle.GetAttribute("y"));
            layer.Width = Convert.ToInt32(mapLayerEle.GetAttribute("width"));
            layer.Height = Convert.ToInt32(mapLayerEle.GetAttribute("height"));
            layer.Opacity = float.Parse(mapLayerEle.GetAttribute("opacity"));
            layer.Visable = Convert.ToBoolean(mapLayerEle.GetAttribute("visible"));

            foreach(XmlElement currentEle in mapLayerEle.ChildNodes)
            {
                if (currentEle.Name == "properties")
                {
                    layer.Properties = LoadProperties(currentEle);
                }

                if(currentEle.Name == "data")
                {
                    layer.DataObject = LoadDataObject(currentEle);

                    // decode data
                    #region Base64Decode
                    if (layer.DataObject.Encoding == EncodingType.Base64)
                    {
                        byte[] dataByte = Convert.FromBase64String(layer.DataObject.Value);
                        Stream tileStream = new MemoryStream(dataByte, false);

                        if(layer.DataObject.Compression == CompressionType.gzip)
                        {
                            tileStream = new GZipStream(tileStream, CompressionMode.Decompress, false);
                        }
                        else
                        {
                            tileStream = new Ionic.Zlib.ZlibStream(tileStream, Ionic.Zlib.CompressionMode.Decompress, false);
                        }

                        using (BinaryReader reader = new BinaryReader(tileStream))
                        {
                            for(int yCord = 0; yCord < layer.Height; yCord++)
                            {
                                for (int xCord = 0; xCord < layer.Width; xCord++)
                                {
                                    UInt32 tileData = reader.ReadUInt32();

                                    Tile tempTile = new Tile();

                                    if((tileData & Tile.FLIPPED_HORIZONTALLY_FLAG) != 0)
                                    {
                                        tempTile.HorizontalFlip = true;
                                    }
                                    else
                                    {
                                        tempTile.HorizontalFlip = false;
                                    }

                                    if((tileData & Tile.FLIPPED_VERTICALLY_FLAG) != 0)
                                    {
                                        tempTile.VerticalFlip = true;
                                    }
                                    else
                                    {
                                        tempTile.VerticalFlip = false;
                                    }

                                    if((tileData & Tile.FLIPPED_DIAGONALLY_FLAG) != 0)
                                    {
                                        tempTile.DiagonalFlip = true;
                                    }
                                    else
                                    {
                                        tempTile.DiagonalFlip = false;
                                    }

                                    tileData &= ~(Tile.FLIPPED_HORIZONTALLY_FLAG |Tile.FLIPPED_VERTICALLY_FLAG | Tile.FLIPPED_DIAGONALLY_FLAG);

                                    tempTile.X = xCord;
                                    tempTile.Y = yCord;
                                    tempTile.GID = (int)tileData;

                                    layer.Tiles.Add(tempTile);
                                }
                            }
                        }
                    }
                    #endregion
                    else if(layer.DataObject.Encoding == EncodingType.CSV)
                    {
                        string[] tiles = layer.DataObject.Value.Split(',');
                        for(int elementCount =0; elementCount < tiles.Length; elementCount++)
                        {
                            layer.Tiles.Add(new Tile { GID = int.Parse(tiles[elementCount]), X = elementCount % layer.Width, Y = elementCount / layer.Width });
                        }
                    }
                    else
                    {
                        int eleCount = 0;
                        foreach(XmlElement currentInnerEle in currentEle.ChildNodes)
                        {
                            layer.Tiles.Add(new Tile { GID = Convert.ToInt32(currentInnerEle.GetAttribute("gid")), X = eleCount % layer.Width, Y = eleCount / layer.Width });
                            eleCount++;
                        }
                    }

                }

                
            }

            return layer;
        }

        private static Data LoadDataObject(XmlElement dataEle)
        {
            Data dataToReturn = new Data();
            
            switch(dataEle.GetAttribute("encoding"))
            {
                case "base64":
                    dataToReturn.Encoding = EncodingType.Base64;
                    break;
                case "csv":
                    dataToReturn.Encoding = EncodingType.CSV;
                    break;
            }

            switch(dataEle.GetAttribute("compression"))
            {
                case "gzip":
                    dataToReturn.Compression = CompressionType.gzip;
                    break;
                case "zlib":
                    dataToReturn.Compression = CompressionType.zlib;
                    break;
            }

            dataToReturn.Value = dataEle.Value;

            return dataToReturn;
        }

        private static List<Terrain> LoadTerrainTypes(XmlElement terrListEle)
        {
            List<Terrain> tempTerr = new List<Terrain>();
            
            foreach(XmlElement currentEle in terrListEle.ChildNodes)
            {
                Terrain newTerr = new Terrain();
                newTerr.Name = currentEle.GetAttribute("name");
                newTerr.TileID = Convert.ToInt32(currentEle.GetAttribute("tile"));

                if(currentEle.HasChildNodes)
                {
                    foreach(XmlElement innerCurrentEle in currentEle.ChildNodes)
                    {
                        if(innerCurrentEle.Name == "properties")
                        {
                            newTerr.Properties = LoadProperties(innerCurrentEle);
                        }
                    }
                }

                tempTerr.Add(newTerr);
            }

            return tempTerr;
        }

        private static TileOffset LoadOffSet(XmlElement currentEle)
        {
            TileOffset offset = new TileOffset();
            offset.X = Convert.ToInt32(currentEle.GetAttribute("x"));
            offset.Y = Convert.ToInt32(currentEle.GetAttribute("y"));
            return offset;
        }

        private static Image LoadImage(XmlElement imageElement)
        {
            Image tempImg = new Image();
            tempImg.Source = imageElement.GetAttribute("source");
            tempImg.Width = Convert.ToInt32(imageElement.GetAttribute("width"));
            tempImg.Height = Convert.ToInt32(imageElement.GetAttribute("height"));

            if (imageElement.HasChildNodes)
            {
                foreach (XmlElement imgInnerNode in imageElement.ChildNodes)
                {
                    if (imgInnerNode.Name == "data")
                    {
                        tempImg.DataObject = new Data();

                        //  None, Base64, CSV
                        switch (imgInnerNode.GetAttribute("encoding"))
                        {
                            case "base64":
                                tempImg.DataObject.Encoding = EncodingType.Base64;
                                break;
                            case "csv":
                                tempImg.DataObject.Encoding = EncodingType.CSV;
                                break;
                            default:
                                throw new Exception("unrconized data encoding in titleset data: " + imgInnerNode.GetAttribute("encoding"));
                        }
                    }
                }
            }
            else
            {
                tempImg.DataObject = null;
            }

            return tempImg;
        }

        private static List<Property> LoadProperties(XmlElement propElement)
        {
            List<Property> tempProps = new List<Property>();

            foreach(XmlElement currentElement in propElement.ChildNodes)
            {
                Property tempProp = new Property();
                tempProp.Name = currentElement.GetAttribute("name");
                tempProp.Value = currentElement.GetAttribute("value");
                tempProps.Add(tempProp);
            }

            return tempProps;
        }

        private static ObjectGroup LoadObjectGroup(XmlElement objGroupElement)
        {
            ObjectGroup tempObjG = new ObjectGroup();

            tempObjG.Name = objGroupElement.GetAttribute("name");
            tempObjG.Color = objGroupElement.GetAttribute("color");
            tempObjG.X = Convert.ToInt32(objGroupElement.GetAttribute("x"));
            tempObjG.Y = Convert.ToInt32(objGroupElement.GetAttribute("y"));
            tempObjG.Width = Convert.ToInt32(objGroupElement.GetAttribute("width"));
            tempObjG.Height = Convert.ToInt32(objGroupElement.GetAttribute("height"));
            tempObjG.Opacity = float.Parse(objGroupElement.GetAttribute("opacity"));
            
            switch(objGroupElement.GetAttribute("visible"))
            {
                case "0":
                    tempObjG.Visable = false;
                    break;
                case "1":
                    tempObjG.Visable = true;
                    break;
                default:
                    throw new Exception("invalid value for visibility of an object group");
            }

            foreach(XmlElement currentElement in objGroupElement.ChildNodes)
            {
                if(currentElement.Name == "properties")
                {
                    tempObjG.Properties = LoadProperties(currentElement);
                }

                if(currentElement.Name == "object")
                {
                    tempObjG.Objects.Add(LoadObject(currentElement));

                }
            }

            return tempObjG;
        }

        private static TileSetTile LoadTileSetTile(XmlElement tileElement)
        {
            TileSetTile tempTile = new TileSetTile();
            tempTile.ID = Convert.ToInt32(tileElement.GetAttribute("id"));

            if (tileElement.HasAttribute("terrain"))
            {
                string preseprate = tileElement.GetAttribute("terrain");
                string[] terrainAtt = preseprate.Split(',');
                tempTile.TerrainTopLeft = Convert.ToInt32(terrainAtt[0]);
                tempTile.TerrainTopRight = Convert.ToInt32(terrainAtt[1]);
                tempTile.TerrainBottemLeft = Convert.ToInt32(terrainAtt[2]);
                tempTile.TerrainBottemRight = Convert.ToInt32(terrainAtt[3]);
            }

            if (tileElement.HasAttribute("probability"))
            {
                tempTile.Probability = float.Parse(tileElement.GetAttribute("probability"));
            }

            foreach(XmlElement currentElemet in tileElement.ChildNodes)
            {
                // HACK: only loading one image
                if(currentElemet.Name == "image")
                {
                    tempTile.TileImage = LoadImage(currentElemet);
                }

                if(currentElemet.Name == "properties")
                {
                    tempTile.Prperties = LoadProperties(currentElemet);
                }

                if(currentElemet.Name == "objectgroup")
                {
                    tempTile.ObjectGroupObject = LoadObjectGroup(currentElemet);
                }

            }

            return tempTile;
        }

        private static MapObject LoadObject(XmlElement objectElement)
        {
            MapObject tempObj = new MapObject();
            tempObj.Ellipse = false;
            tempObj.ID = Convert.ToInt32(objectElement.GetAttribute("id"));
            tempObj.Name = objectElement.GetAttribute("name");
            tempObj.Type = objectElement.GetAttribute("type");
            tempObj.X = Convert.ToInt32(objectElement.GetAttribute("x"));
            tempObj.Y = Convert.ToInt32(objectElement.GetAttribute("y"));
            tempObj.Width = Convert.ToInt32(objectElement.GetAttribute("width"));
            tempObj.Height = Convert.ToInt32(objectElement.GetAttribute("height"));
            tempObj.Visable = Convert.ToBoolean(objectElement.GetAttribute("visible"));

            if (objectElement.HasAttribute("gid"))
            {
                tempObj.GID = Convert.ToInt32(objectElement.GetAttribute("gid"));
            }

            foreach(XmlElement currentEle in objectElement)
            {
                switch (currentEle.Name)
                {
                    case "ellipse":
                        tempObj.Ellipse = true;
                        break;
                    case "polygon":
                        tempObj.PolygonObject = LoadPolygon(currentEle);
                        break;
                    case "polyline":
                        tempObj.PolylineObject = LoadPolyline(currentEle);
                        break;
                    case "image":
                        tempObj.Img = LoadImage(currentEle);
                        break;
                    default:
                        break;
                }

                
            }

            return tempObj;
        }

        private static Polygon LoadPolygon(XmlElement polyEle)
        {
            Polygon tempPloy = new Polygon();
            tempPloy.Points = new List<Point>();
            string[] points = polyEle.GetAttribute("polygon").Split(' ');
            foreach(string currentRawPointSet in points)
            {
                string[] pointSet = currentRawPointSet.Split(',');
                tempPloy.Points.Add(new Point { X = Convert.ToInt32(pointSet[0]), Y = Convert.ToInt32(pointSet[1]) });
            }

            return tempPloy;
        }

        private static Polyline LoadPolyline(XmlElement lineEle)
        {
            Polyline line = new Polyline();
            line.Points = new List<Point>();
            string[] points = lineEle.GetAttribute("polygon").Split(' ');
            foreach (string currentRawPointSet in points)
            {
                string[] pointSet = currentRawPointSet.Split(',');
                line.Points.Add(new Point { X = Convert.ToInt32(pointSet[0]), Y = Convert.ToInt32(pointSet[1]) });
            }

            return line;
        }

        public static void SaveTiledTileset(MapTileSet tileSetToSave)
        {
            // TODO: Finish save Tileset to file
            throw new Exception("Not yet implanted");
        }

        
    }
}
