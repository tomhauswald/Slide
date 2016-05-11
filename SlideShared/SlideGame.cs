using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SlideShared
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SlideGame : Game
    {
        public const int WIDTH = 800;
        public const int HEIGHT = 480;

        private GraphicsDeviceManager graphics;
        private List<Sprite> sprites;

        public SpriteBatch Batch { get; private set; }
        public SpriteFont Font { get; private set; }
        public Player Player { get; private set; }
        public TileMap Map { get; private set; }

        public SlideGame(bool fullscreen)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = fullscreen;
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;

            bool landscape = (WIDTH >= HEIGHT);
            if (landscape)
            {
                graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            }
            else
            {
                graphics.SupportedOrientations = DisplayOrientation.Portrait;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            sprites = new List<Sprite>();
            base.Initialize();
        }

        public T GetRandomEnumValue<T>(Random random)
        {
            int count = Enum.GetValues(typeof(T)).Length;
            int value = (int)(random.NextDouble() * count);
            return (T)Enum.GetValues(typeof(T)).GetValue(value);
        }

        private TileMap CreateRandomMap(int xTiles, int yTiles)
        {
            var map = new TileMap(this, xTiles, yTiles);
            var random = new Random();
            for (int x = 0; x < map.Width; ++x)
            {
                for (int y = 0; y < map.Height; ++y)
                {
                    map.Tiles[x][y].SetTileType(GetRandomEnumValue<TileType>(random));
                }
            }
            return map;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Batch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Fonts/arial");

            TileAtlas.LoadTextures(Content);

            Map = CreateRandomMap(WIDTH / Tile.SIZE, HEIGHT / Tile.SIZE);
            Components.Add(Map);
            sprites.Add(Map);

            Player = new Player(this);
            Components.Add(Player);
            sprites.Add(Player);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Batch.Begin();
            sprites.ForEach(sprite => sprite.Draw());
            Batch.End();

            base.Draw(gameTime);
        }
    }
}
