using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public const int TILESIZE = 40;

        GraphicsDeviceManager graphics;
        SpriteBatch batch;
        SpriteFont font;
        List<Sprite> sprites;
        TileSprite player;

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

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Fonts/arial");

            player = new TileSprite(this, batch);
            player.Texture = Content.Load<Texture2D>("Textures/player");
            player.setTilePosition(1, 1);
            Components.Add(player);
            sprites.Add(player);
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

            batch.Begin();
            sprites.ForEach(sprite => sprite.Draw());
            batch.End();

            base.Draw(gameTime);
        }
    }
}
