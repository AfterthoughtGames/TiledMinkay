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
                    tileSetToReturn.Images.Add(LoadImage(currentEle));
                }
                #endregion

                if(currentEle.Name == "tile")
                {
                    // TODO: finish loading tile in map object
                }
            }

            throw new Exception("Not yet implanted");
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
                    // TODO: Finish loading objects in object group
                }
            }
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
                    // TODO: finish loading object group in tileset
                }
            }

            // TODO: finish the rest of this code for tile loading with in a tileset
        }

        public static void SaveTiledTileset(MapTileSet tileSetToSave)
        {
            // TODO: Finish save Tileset to file
            throw new Exception("Not yet implanted");
        }
    }
}
