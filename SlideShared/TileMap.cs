using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SlideShared
{
    public class TileMap : Sprite
    {
        public Tile[][] Tiles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile PlayerStartTile { get; private set; }

        public TileMap(Game game, int width, int height)
        : base(game)
        {
            Width = width;
            Height = height;
            CreateTileArray(Width, Height);
            PlayerStartTile = Tiles[0][0];
        }

        public TileMap(Game game, string filename)
        :base(game)
        {
            using (var reader = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                int row = 0;
                string line;

                var meta = reader.ReadLine().Split(',');
                Width  = int.Parse(meta[0]);
                Height = int.Parse(meta[1]);
                CreateTileArray(Width, Height);

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    for (int i = 0; i < line.Length; ++i)
                    {
                        switch (line[i])
                        {
                            case 'W':
                                Tiles[i][row].SetTileType(TileType.Wall);
                                break;
                            case 'S':
                                Tiles[i][row].SetTileType(TileType.Stop);
                                break;
                            case 'P':
                                Tiles[i][row].SetTileType(TileType.Plain);
                                break;
                            case 'R':
                                Tiles[i][row].SetTileType(TileType.Speed);
                                Tiles[i][row].SetDirection(Direction.Right);
                                break;
                            case 'L':
                                Tiles[i][row].SetTileType(TileType.Speed);
                                Tiles[i][row].SetDirection(Direction.Left);
                                break;
                            case 'U':
                                Tiles[i][row].SetTileType(TileType.Speed);
                                Tiles[i][row].SetDirection(Direction.Up);
                                break;
                            case 'D':
                                Tiles[i][row].SetTileType(TileType.Speed);
                                Tiles[i][row].SetDirection(Direction.Down);
                                break;
                            case 'i':
                                Tiles[i][row].SetTileType(TileType.Plain);
                                PlayerStartTile = Tiles[i][row];
                                break;
                            default:
                                Console.Error.WriteLine("Unsupported tile code: " + line[i] + "!");
                                break;
                        }
                    }
                    ++row;
                }
            }
        }

        private void CreateTileArray(int width, int height)
        {
            Tiles = new Tile[width][];

            for (int x = 0; x < width; ++x)
            {
                Tiles[x] = new Tile[height];
                for (int y = 0; y < height; ++y)
                {
                    Tiles[x][y] = new Tile(Game, this, x, y);
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
