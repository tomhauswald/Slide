using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlideShared
{
    public class TileMap : Sprite
    {
        public Tile[][] Tiles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public TileMap(Game game, SpriteBatch batch, int width, int height)
        : base(game, batch)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[Width][];

            for (int x = 0; x < Width; ++x)
            {
                Tiles[x] = new Tile[Height];
                for (int y = 0; y < Height; ++y)
                {
                    Tiles[x][y] = new Tile(game, batch, x, y);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    Tiles[x][y].Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            for(int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    Tiles[x][y].Draw();
                }
            }
        }
    }
}
