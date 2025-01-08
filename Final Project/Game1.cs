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

        Texture2D blackTargetTexture;

        Texture2D greenTargetTexture;

        Texture2D blueTargetTexture;

        Texture2D redTargetTexture;

        Texture2D crosshairTexture;

        Texture2D menuTexture;
        Texture2D backgroundTexture;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            blackTargetTexture = Content.Load<Texture2D>("black-target");
            greenTargetTexture = Content.Load<Texture2D>("green-target");
            blueTargetTexture = Content.Load<Texture2D>("blue-target");
            redTargetTexture = Content.Load<Texture2D>("red-target");
            crosshairTexture = Content.Load<Texture2D>("crosshair");
            menuTexture = Content.Load<Texture2D>("menuBackground");
            backgroundTexture = Content.Load<Texture2D>("shooting-room");
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

            base.Draw(gameTime);
        }
    }
}
