using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project
{
    enum Screen
    {
        Menu,
        Instructions,
        Game,
        End
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D rectangleTexture;
        Rectangle playRect;
        Rectangle instructRect;
        Rectangle quitRect;
        Rectangle backRect;

        Rectangle titleRect;

        Texture2D blackTargetTexture;

        Texture2D greenTargetTexture;

        Texture2D blueTargetTexture;

        Texture2D redTargetTexture;

        Texture2D crosshairTexture;
        Rectangle crosshairRect;

        Texture2D menuTexture;
        Texture2D backgroundTexture;

        SpriteFont menuFont;

        MouseState mouseState;

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
            playRect = new Rectangle(250, 300, 300, 40);
            instructRect = new Rectangle(250, 360, 300, 40);
            quitRect = new Rectangle(250, 420, 300, 40);
            backRect = new Rectangle(250, 555, 300, 40);
            titleRect = new Rectangle(200, 75, 388, 80);
            crosshairRect = new Rectangle(0, 0, 50, 50);

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
            mouseState = Mouse.GetState();
            crosshairRect.Location = mouseState.Position;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (screen == Screen.Menu)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (playRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Game;
                    }
                    else if (instructRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Instructions;
                    }
                    else if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (quitRect.Contains(mouseState.Position))
                            Exit();
                    }
                }
            }
            else if (screen == Screen.Instructions)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (backRect.Contains(mouseState.Position))
                        screen = Screen.Menu;
            }
        
            else if(screen == Screen.Game)
            {
                IsMouseVisible = false;
            }
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
                _spriteBatch.Draw(rectangleTexture, titleRect, Color.Black);
                _spriteBatch.DrawString(menuFont, "SHOOTING", new Vector2(330, 80), Color.Red);
                _spriteBatch.DrawString(menuFont, "GALLERY", new Vector2(337, 120), Color.Red);
                _spriteBatch.Draw(rectangleTexture, playRect, Color.DarkBlue);
                _spriteBatch.DrawString(menuFont, "PLAY", new Vector2(365, 305), Color.White);
                _spriteBatch.Draw(rectangleTexture, instructRect, Color.DarkBlue);
                _spriteBatch.DrawString(menuFont, "INSTRUCTIONS", new Vector2(312, 365), Color.White);
                _spriteBatch.Draw(rectangleTexture, quitRect, Color.DarkBlue);
                _spriteBatch.DrawString(menuFont, "QUIT", new Vector2(365, 425), Color.White);
                
            }
            else if (screen == Screen.Instructions)
            {
                _spriteBatch.Draw(menuTexture, window, Color.White);
                _spriteBatch.DrawString(menuFont, "Shoot targets by clicking on them to gain points.", new Vector2(175, 325), Color.DarkBlue);
                _spriteBatch.DrawString(menuFont, "Black = 1 point", new Vector2(325, 365), Color.White);
                _spriteBatch.DrawString(menuFont, "Green = 2 points", new Vector2(325, 405), Color.White);
                _spriteBatch.DrawString(menuFont, "Blue = 3 points", new Vector2(325, 445), Color.White);
                _spriteBatch.DrawString(menuFont, "Red = 4 points", new Vector2(325, 485), Color.White);
                _spriteBatch.Draw(rectangleTexture, backRect, Color.DarkBlue);
                _spriteBatch.DrawString(menuFont, "BACK", new Vector2(365, 560), Color.White);
            }
            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(backgroundTexture, window, Color.White);
                _spriteBatch.Draw(crosshairTexture, crosshairRect, Color.Red);
                // _spriteBatch.DrawString(introFont, "TRAFFIC JAM", new Vector2(265, 285), Color.Red);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
