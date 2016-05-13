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
        public const int WIDTH = 1280;
        public const int HEIGHT = 720;

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

            for (int x = 1; x < map.Width - 1; ++x)
            {
                for (int y = 1; y < map.Height - 1; ++y)
                {
                    map.Tiles[x][y].SetTileType(GetRandomEnumValue<TileType>(random));
                    if(map.Tiles[x][y].GetTileType() == TileType.Speed)
                        map.Tiles[x][y].SetDirection(GetRandomEnumValue<Direction>(random));
                }
            }

            map.Tiles[1][1].SetTileType(TileType.Plain);
            return map;
        }

        private TileMap LoadMapFile(string file)
        {
            return new TileMap(this, file);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Batch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>("Fonts/arial");

            TileAtlas.Load(Content, "Textures/tilesheet_complete_2X");

            Map = LoadMapFile("map.txt");
            Components.Add(Map);
            sprites.Add(Map);

            Player = new Player(this);
            Player.SetTopLeftCorner(Map.PlayerStartTile.GetTopLeftCorner());
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
