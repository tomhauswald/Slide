using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SlideShared
{
    public class Sprite : GameComponent
    {
        protected Vector2 center { get; private set; }

        // Texture.
        public Texture2D Texture { get; set; }
        public Rectangle? SubTextureRect { get; set; }

        // Position.
        public Vector2 Position { get; set; }
        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        // Scale factors.
        public Vector2 Scale { get; set; }
        public float HorizontalScale { get { return Scale.X; } set { Scale = new Vector2(value, Scale.Y); } }
        public float VerticalScale { get { return Scale.Y; } set { Scale = new Vector2(Scale.X, value); } }

        // Rotation.
        public float Rotation { get; set; }

        // Show / hide sprite.
        public bool Visible { get; set; }

        public Sprite(Game game)
        : base(game)
        {
            Position = Vector2.Zero;
            Rotation = 0.0f;
            Scale = Vector2.One;
            Visible = true;
        }

        public override void Update(GameTime gameTime)
        {
            // Recalculate center point.
            if (Texture != null)
            {
                center = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
            }
            else
            {
                center = Vector2.Zero;
            }

            base.Update(gameTime);
        }

        public Vector2 GetAbsoluteSize()
        {
            return new Vector2(Texture.Width * HorizontalScale, Texture.Height * VerticalScale);
        }

        public void SetAbsoluteSize(float width, float height)
        {
            HorizontalScale = width / Texture.Width;
            VerticalScale = height / Texture.Height;
        }

        // Batch must be started before draw call.
        public virtual void Draw()
        {
            if (Visible && Texture != null)
            {
                ((SlideGame)Game).Batch.Draw(
                    texture: Texture,
                    position: Position,
                    sourceRectangle: SubTextureRect,
                    origin: center,
                    rotation: Rotation / 180.0f * 2.0f * (float)Math.PI,
                    scale: Scale);
            }
        }
    }
}
