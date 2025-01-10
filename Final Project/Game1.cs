using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project
{
    enum Screen
    {
        Menu,
        Game,
        End
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D rectangleTexture;

        Texture2D blackTargetTexture;

        Texture2D greenTargetTexture;

        Texture2D blueTargetTexture;

        Texture2D redTargetTexture;

        Texture2D crosshairTexture;

        Texture2D menuTexture;
        Texture2D backgroundTexture;

        SpriteFont menuFont;

        Screen screen;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            screen = Screen.Menu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            blackTargetTexture = Content.Load<Texture2D>("black-target");
            greenTargetTexture = Content.Load<Texture2D>("green-target");
            blueTargetTexture = Content.Load<Texture2D>("blue-target");
            redTargetTexture = Content.Load<Texture2D>("red-target");
            crosshairTexture = Content.Load<Texture2D>("crosshair");
            menuTexture = Content.Load<Texture2D>("menuBackground");
            backgroundTexture = Content.Load<Texture2D>("shooting-room");
            menuFont = Content.Load<SpriteFont>("menu");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Menu)
            {
                _spriteBatch.Draw(menuTexture, window, Color.White);
                _spriteBatch.Draw(rectangleTexture, new Rectangle(250, 300, 300, 40), Color.DarkBlue);
                _spriteBatch.DrawString(menuFont, "PLAY", new Vector2(365, 305), Color.White);
                _spriteBatch.Draw(rectangleTexture, new Rectangle(250, 360, 300, 40), Color.DarkBlue);
                _spriteBatch.DrawString(menuFont, "INSTRUCTIONS", new Vector2(312, 365), Color.White);
                _spriteBatch.Draw(rectangleTexture, new Rectangle(250, 420, 300, 40), Color.DarkBlue);
                _spriteBatch.DrawString(menuFont, "QUIT", new Vector2(365, 425), Color.White);
            }
            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(backgroundTexture, window, Color.White);
                // _spriteBatch.DrawString(introFont, "TRAFFIC JAM", new Vector2(265, 285), Color.Red);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
