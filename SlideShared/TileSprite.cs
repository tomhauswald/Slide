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
            setTilePosition(0, 0);
            SetAbsoluteSize(SlideGame.TILESIZE, SlideGame.TILESIZE);
        }

        public void setTilePosition(int x, int y)
        {
            setTileX(x);
            setTileY(y);
        }

        public void setTileX(int x)
        {
            X = SlideGame.TILESIZE * (x + 0.5f);
        }

        public void setTileY(int y)
        {
            Y = SlideGame.TILESIZE * (y + 0.5f);
        }
    }
}
