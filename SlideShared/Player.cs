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

    public enum PlayerMovementState
    {
        Idle,
        Walk,
        Slide
    }

    public class Player : Sprite
    {
        public const float WALKSPEED = Tile.SIZE;
        public const float SLIDESPEED = 2.0f * WALKSPEED;

        private Vector2 velocity;
        private Direction direction;
        private TileMap map;
        private Tile targetTile;

        public PlayerMovementState MovementState { get; private set; }
        
        public Player(Game game)
            :base(game)
        {
            SetTexture(game.Content.Load<Texture2D>("Textures/red"));
            SetSubTextureRect(new Rectangle(0, 0, 32, 32));
            SetAbsoluteSize(new Point(Tile.SIZE));
            SetTopLeftCorner(Vector2.Zero);
            
            velocity = Vector2.Zero;
            direction = Direction.Down;
            map = ((SlideGame)game).Map;

            MovementState = PlayerMovementState.Idle;
        }

        public override void Update(GameTime gameTime)
        {
            // Console.WriteLine(GetTopLeftCorner().ToString());

            var keyboard = Keyboard.GetState();

            var absoluteSize = GetAbsoluteSize();
            var feetPosition = GetTopLeftCorner() + new Vector2(absoluteSize.X * 0.5f, absoluteSize.Y * 0.9f);
            var tile = map.GetTileAt(feetPosition);

            // Currently not sliding.
            switch (MovementState)
            {
                case PlayerMovementState.Idle:
                    if (tile.GetTileType() == TileType.Speed)
                    {
                        Slide(tile.GetDirection());
                    }
                    else
                    {
                        if (keyboard.IsKeyDown(Keys.W) && tile.Y > 0)
                        { Walk(Direction.Up); }
                        else if (keyboard.IsKeyDown(Keys.A) && tile.X > 0)
                        { Walk(Direction.Left); }
                        else if (keyboard.IsKeyDown(Keys.S) && tile.Y < map.Height - 1)
                        { Walk(Direction.Down); }
                        else if (keyboard.IsKeyDown(Keys.D) && tile.X < map.Width - 1)
                        { Walk(Direction.Right); }
                    }
                    break;

                case PlayerMovementState.Walk:
                    if(Vector2.Distance(GetTopLeftCorner(), targetTile.GetTopLeftCorner()) <= 1.0f)
                    {
                        SetTopLeftCorner(targetTile.GetTopLeftCorner());
                        Stop();
                    }
                    break;

                case PlayerMovementState.Slide:
                    if (Vector2.Distance(GetTopLeftCorner(), tile.GetTopLeftCorner()) <= 1.0f)
                    {
                        if (tile.GetTileType() == TileType.Stop)
                        {
                            Stop();
                        }
                    }
                    break;
            }

            Center += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public void Stop()
        {
            MovementState = PlayerMovementState.Idle;
            velocity = Vector2.Zero;
        }

        public void Walk(Direction direction)
        {
            this.direction = direction;
            switch (direction)
            {
                case Direction.Left:
                    velocity = new Vector2(-WALKSPEED, 0.0f);
                    targetTile = map.GetTileAt(GetTopLeftCorner() + new Vector2(-Tile.SIZE, 0.0f));
                    break;

                case Direction.Down:
                    velocity = new Vector2(0.0f, WALKSPEED);
                    targetTile = map.GetTileAt(GetTopLeftCorner() + new Vector2(0.0f, Tile.SIZE));
                    break;

                case Direction.Right:
                    velocity = new Vector2(WALKSPEED, 0.0f);
                    targetTile = map.GetTileAt(GetTopLeftCorner() + new Vector2(Tile.SIZE, 0.0f));
                    break;

                case Direction.Up:
                    velocity = new Vector2(0.0f, -WALKSPEED);
                    targetTile = map.GetTileAt(GetTopLeftCorner() + new Vector2(0.0f, -Tile.SIZE));
                    break;
            }

            MovementState = PlayerMovementState.Walk;
        }

        public void Slide(Direction direction)
        {
            this.direction = direction;
            switch (direction)
            {
                case Direction.Up:
                    velocity = new Vector2(0.0f, -SLIDESPEED);
                    break;

                case Direction.Left:
                    velocity = new Vector2(-SLIDESPEED, 0.0f);
                    break;

                case Direction.Right:
                    velocity = new Vector2(SLIDESPEED, 0.0f);
                    break;

                case Direction.Down:
                    velocity = new Vector2(0.0f, SLIDESPEED);
                    break;
            }

            MovementState = PlayerMovementState.Slide;
        }
    }
}
