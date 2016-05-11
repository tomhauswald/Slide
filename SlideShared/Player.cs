using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlideShared
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Player : TileSprite
    {
        public bool Sliding { get; private set; }
        public Vector2 Velocity { get; private set; }
        public float WalkVelocity { get; private set; }
        public float SlideVelocity { get; private set; }

        private Direction slideDirection;
        private TileMap map;

        public Player(Game game)
            :base(game)
        {
            Texture = game.Content.Load<Texture2D>("Textures/player");
            SetTileCount(1, 1);
            SetTilePosition(1, 1);

            Sliding = false;
            WalkVelocity = Tile.SIZE;
            SlideVelocity = 2.0f * WalkVelocity;
            Velocity = Vector2.Zero;

            slideDirection = Direction.Down;
            map = ((SlideGame)game).Map;
        }

        public override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var tile = map.Tiles[GetTileX()][GetTileY()];

            // Currently not sliding.
            if (!Sliding)
            {
                if (keyboard.IsKeyDown(Keys.W))
                {
                    Velocity = new Vector2(0.0f, -WalkVelocity);
                }
                else if (keyboard.IsKeyDown(Keys.A))
                {
                    Velocity = new Vector2(-WalkVelocity, 0.0f);
                }
                else if (keyboard.IsKeyDown(Keys.S))
                {
                    Velocity = new Vector2(0.0f, WalkVelocity);
                }
                else if (keyboard.IsKeyDown(Keys.D))
                {
                    Velocity = new Vector2(WalkVelocity, 0.0f);
                }
                else
                {
                    Velocity = Vector2.Zero;
                }

                switch (tile.GetTileType())
                {
                    case TileType.Speed:
                        Slide(Direction.Right);
                        break;
                    default: break;
                }
            }

            // Currently sliding.
            else
            {
                switch(tile.GetTileType())
                {
                    case TileType.Stop:
                        Sliding = false;
                        break;
                    default: break;
                }
            }

            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public void Slide(Direction dir)
        {
            slideDirection = dir;

            switch (slideDirection)
            {
                case Direction.Up:
                    Velocity = new Vector2(0.0f, -SlideVelocity);
                    break;

                case Direction.Left:
                    Velocity = new Vector2(-SlideVelocity, 0.0f);
                    break;

                case Direction.Right:
                    Velocity = new Vector2(SlideVelocity, 0.0f);
                    break;

                case Direction.Down:
                    Velocity = new Vector2(0.0f, SlideVelocity);
                    break;
            }

            Sliding = true;
        }
    }
}
