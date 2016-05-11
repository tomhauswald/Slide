using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlideShared
{
    public class TileSprite : Sprite
    {
        public TileSprite(Game game)
            :base(game)
        {
        }

        public Vector2 GetTilePosition()
        {
            return new Vector2(GetTileX(), GetTileY());
        }

        public void SetTilePosition(int x, int y)
        {
            SetTileX(x);
            SetTileY(y);
        }

        public int GetTileX()
        {
            return (int)((X - (0.5f * Tile.SIZE)) / Tile.SIZE);
        }

        public void SetTileX(int x)
        {
            X = x * Tile.SIZE + 0.5f * GetAbsoluteSize().X;
        }

        public int GetTileY()
        {
            return (int)((Y - (0.5f * Tile.SIZE)) / Tile.SIZE);
        }

        public void SetTileY(int y)
        {
            Y = y * Tile.SIZE + 0.5f * GetAbsoluteSize().Y;
        }

        public void SetTileCount(int xTiles, int yTiles)
        {
            SetAbsoluteSize(xTiles * Tile.SIZE, yTiles * Tile.SIZE);
        }

        public Vector2 GetTileCount()
        {
            return new Vector2(
                (int)(GetAbsoluteSize().X / Tile.SIZE),
                (int)(GetAbsoluteSize().Y / Tile.SIZE));
        }
    }
}
