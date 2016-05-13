using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlideShared
{
    public static class TileAtlas
    {
        private const int SOURCE_TILESIZE = 128;
        private static Texture2D tilesheet;
        private static Dictionary<TileType, Rectangle> subTextureRects;

        public static void Load(ContentManager content, string tilesheetName)
        {
            tilesheet = content.Load<Texture2D>(tilesheetName);
            subTextureRects = new Dictionary<TileType, Rectangle>();
            subTextureRects.Add(TileType.Plain, GetTileRect(11, 0));
            subTextureRects.Add(TileType.Wall, GetTileRect(6, 13));
            subTextureRects.Add(TileType.Stop, GetTileRect(12, 0));
            subTextureRects.Add(TileType.Speed, GetTileRect(19, 1));
        }

        public static Texture2D GetTilesheetTexture()
        {
            return tilesheet;
        }

        public static Rectangle GetTileRect(TileType tiletype)
        {
            return subTextureRects[tiletype];
        }

        public static Rectangle GetTileRect(int xIndex, int yIndex)
        {
            return new Rectangle(xIndex * SOURCE_TILESIZE, yIndex * SOURCE_TILESIZE, SOURCE_TILESIZE, SOURCE_TILESIZE);
        }
    }
}
