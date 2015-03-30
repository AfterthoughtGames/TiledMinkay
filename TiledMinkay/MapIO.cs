using System;
using TiledMap;
using TiledMap.TileSet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace TiledMinkay
{
    // Load / Save Maps and TileSets
    public class MapIO
    {
        public static Map LoadTiledMap(string fileNameAndPath)
        {
            // You are probably wondering why so much stuff crammed in to one function
            // https://msdn.microsoft.com/en-us/library/ms973852.aspx
            // the more static calls we make the slower the operation will take
            // while in most cases nested function calls are ok. however we are
            // talking about loading maps for video games in a managed environment.
            // The less calls we can make the better. So I have traded nested calls
            // for regions that only effect readability and not code speed.

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

                // TODO: Finish all of the map loading routines
            }

            throw new Exception("Not yet implanted");
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
                    Image tempImg = new Image();
                    tempImg.Source = currentEle.GetAttribute("source");
                    tempImg.Width = Convert.ToInt32(currentEle.GetAttribute("width"));
                    tempImg.Height = Convert.ToInt32(currentEle.GetAttribute("height"));

                    if(currentEle.HasChildNodes)
                    {
                        foreach(XmlElement imgInnerNode in currentEle.ChildNodes)
                        {
                            if(imgInnerNode.Name == "data")
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

                    tileSetToReturn.Images.Add(tempImg);
                }
                #endregion

                if(currentEle.Name == "tile")
                {
                    TileSetTile tempTile = new TileSetTile();
                    tempTile.ID = Convert.ToInt32(currentEle.GetAttribute("id"));
                    // TODO: finish the rest of this code for tile loading with in a tileset
                }
            }

            throw new Exception("Not yet implanted");
        }

        public static void SaveTiledTileset(MapTileSet tileSetToSave)
        {
            // TODO: Finish save Tileset to file
            throw new Exception("Not yet implanted");
        }
    }
}
