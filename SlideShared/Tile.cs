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

        public Tile(Game game, TileMap map, int x, int y, Direction direction)
        : base(game)
        {
            this.map = map;
            SetDirection(direction);
            SetTileType(TileType.Plain);
            SetAbsoluteSize(new Point(SIZE));
            SetTopLeftCorner(map.GetWorldCoordinates(new Point(x, y)));
            X = x;
            Y = y;
        }

        public TileType GetTileType()
        {
            return type;
        }

        // Automatically updates the tile texture.
        public void SetTileType(TileType type)
        {
            this.type = type;
            SetTexture(TileAtlas.Textures[type]);
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
                    Rotation = 0.0f;
                    break;

                case Direction.Left:
                    Rotation = 180.0f;
                    break;

                case Direction.Up:
                    Rotation = 90.0f;
                    break;

                case Direction.Down:
                    Rotation = 270.0f;
                    break;
            }
        }
    }
}
