using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlideShared
{
    public class TileSprite : Sprite
    {
        public TileSprite(Game game, SpriteBatch batch)
            :base(game, batch)
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
            return (int)((X - (0.5f * SlideGame.TILESIZE)) / SlideGame.TILESIZE);
        }

        public void SetTileX(int x)
        {
            X = x * SlideGame.TILESIZE + 0.5f * GetAbsoluteSize().X;
        }

        public int GetTileY()
        {
            return (int)((Y - (0.5f * SlideGame.TILESIZE)) / SlideGame.TILESIZE);
        }

        public void SetTileY(int y)
        {
            Y = y * SlideGame.TILESIZE + 0.5f * GetAbsoluteSize().Y;
        }

        public void SetTileCount(int xTiles, int yTiles)
        {
            SetAbsoluteSize(xTiles * SlideGame.TILESIZE, yTiles * SlideGame.TILESIZE);
        }

        public Vector2 GetTileCount()
        {
            return new Vector2(
                (int)(GetAbsoluteSize().X / SlideGame.TILESIZE),
                (int)(GetAbsoluteSize().Y / SlideGame.TILESIZE));
        }
    }
}
