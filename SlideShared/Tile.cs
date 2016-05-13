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

    public class Tile : Sprite
    {
        public const int SIZE = 64;

        private TileMap map;
        private TileType type;
        private Direction direction;

        public int X { get; private set; }
        public int Y { get; private set; }

        public Tile(Game game, TileMap map, int x, int y)
        : base(game)
        {
            this.map = map;
            SetDirection(Direction.Down);
            SetTileType(TileType.Plain);
            SetTopLeftCorner(map.GetWorldCoordinates(new Point(x, y)));
            X = x;
            Y = y;
        }

        public Tile GetNeighbour(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return Y > 0 ? map.Tiles[X][Y - 1] : null;
                case Direction.Left: return X > 0 ? map.Tiles[X - 1][Y] : null;
                case Direction.Down: return Y < map.Height - 1 ? map.Tiles[X][Y + 1] : null;
                case Direction.Right: return X < map.Width - 1 ? map.Tiles[X + 1][Y] : null;
                default: return null;
            }
        }

        public TileType GetTileType()
        {
            return type;
        }

        // Automatically updates the tile texture.
        public void SetTileType(TileType type)
        {
            this.type = type;
            SetTexture(TileAtlas.GetTilesheetTexture());
            SetSubTextureRect(TileAtlas.GetTileRect(type));
            SetAbsoluteSize(new Point(SIZE));
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public void SetDirection(Direction direction)
        {
            this.direction = direction;
            switch (direction)
            {
                case Direction.Right:
                    Rotation = 90.0f;
                    break;

                case Direction.Left:
                    Rotation = 270.0f;
                    break;

                case Direction.Up:
                    Rotation = 0.0f;
                    break;

                case Direction.Down:
                    Rotation = 180.0f;
                    break;
            }
        }
    }
}
