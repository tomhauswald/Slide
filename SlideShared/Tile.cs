using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlideShared
{
    public enum TileType
    {
        Plain,
        Wall,
        Stop,
        Speed
    }

    public class Tile : TileSprite
    {
        public const int SIZE = 40;

        private TileType type;

        public Tile(Game game, int x, int y)
        : base(game)
        {
            SetTileType(TileType.Plain);
            SetTileCount(1, 1);
            SetTilePosition(x, y);
        }

        public TileType GetTileType()
        {
            return type;
        }

        // Automatically updates the tile texture.
        public void SetTileType(TileType type)
        {
            this.type = type;
            Texture = TileAtlas.Textures[type];
        }
    }
}
