using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlideShared
{
    public static class TileAtlas
    {
        public static Dictionary<TileType, Texture2D> Textures { get; private set; }

        public static void LoadTextures(ContentManager content)
        {
            Textures = new Dictionary<TileType, Texture2D>();
            Textures.Add(TileType.Plain, content.Load<Texture2D>("Textures/plain"));
            Textures.Add(TileType.Wall,  content.Load<Texture2D>("Textures/wall"));
            Textures.Add(TileType.Stop,  content.Load<Texture2D>("Textures/stop"));
            Textures.Add(TileType.Speed, content.Load<Texture2D>("Textures/speed"));
        }
    }
}
