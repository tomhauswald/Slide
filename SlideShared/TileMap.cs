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

        public TileMap(Game game, int width, int height)
        : base(game)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[Width][];

            for (int x = 0; x < Width; ++x)
            {
                Tiles[x] = new Tile[Height];
                for (int y = 0; y < Height; ++y)
                {
                    Tiles[x][y] = new Tile(game, this, x, y, Direction.Right);
                }
            }
        }

        public Point GetTileCoordinates(Vector2 worldPosition)
        {
            return new Point(
                ((int)(worldPosition.X / Tile.SIZE)),
                ((int)(worldPosition.Y / Tile.SIZE)));
        }

        public Vector2 GetWorldCoordinates(Point tileCoordinates)
        {
            return new Vector2(
                tileCoordinates.X * Tile.SIZE,
                tileCoordinates.Y * Tile.SIZE);
        }

        public Tile GetTileAt(Vector2 worldPosition)
        {
            return GetTileAt(GetTileCoordinates(worldPosition));
        }

        public Tile GetTileAt(Point tileCoordinates)
        {
            return Tiles[tileCoordinates.X][tileCoordinates.Y];
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
