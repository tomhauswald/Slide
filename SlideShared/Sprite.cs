using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SlideShared
{
    public class Sprite : GameComponent
    {
        private Texture2D texture;
        private Rectangle? subTextureRect;
        private Vector2 offset;
        private Point absoluteSize;

        public Vector2 Center { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }
        public bool Visible { get; set; }

        public Sprite(Game game)
        : base(game)
        {
            texture = null;
            subTextureRect = null;
            offset = Vector2.Zero;
            absoluteSize = Point.Zero;

            Center = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0.0f;
            Visible = true;
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
            offset = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
        }

        public void SetSubTextureRect(Rectangle rect)
        {
            subTextureRect = rect;
            offset = new Vector2(rect.Width * 0.5f, rect.Height * 0.5f);
        }

        public void SetAbsoluteSize(Point size)
        {
            absoluteSize = size;
            if(texture != null)
            {
                Scale = new Vector2(
                    (float)size.X / (subTextureRect.HasValue ? subTextureRect.Value.Width : texture.Width),
                    (float)size.Y / (subTextureRect.HasValue ? subTextureRect.Value.Height : texture.Height));
            }
        }

        public Point GetAbsoluteSize()
        {
            return absoluteSize;
        }

        // Batch must be started before draw call.
        public virtual void Draw()
        {
            if (Visible && texture != null)
            {
                ((SlideGame)Game).Batch.Draw(
                    texture: texture,
                    position: Center,
                    sourceRectangle: subTextureRect,
                    origin: offset,
                    rotation: Rotation / 180.0f * (float)Math.PI,
                    scale: Scale);
            }
        }

        public Vector2 GetTopLeftCorner()
        {
            return Center - GetAbsoluteSize().ToVector2() * 0.5f;
        }

        public void SetTopLeftCorner(Vector2 corner)
        {
            Center = corner + GetAbsoluteSize().ToVector2() * 0.5f;
        }
    }
}
