using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

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

        Random generator = new Random();

        int points, randX, randY;

        Texture2D rectangleTexture;
        Rectangle playRect;
        Rectangle instructRect;
        Rectangle quitRect;
        Rectangle backRect;

        Rectangle titleRect;

        Texture2D blackTargetTexture;
        Rectangle blackTargetRect;
        Vector2 blackTargetSpeed;

        Texture2D greenTargetTexture;
        Rectangle greenTargetRect;
        Vector2 greenTargetSpeed;

        Texture2D blueTargetTexture;
        Rectangle blueTargetRect;
        Vector2 blueTargetSpeed;

        Texture2D redTargetTexture;
        Rectangle redTargetRect;
        Vector2 redTargetSpeed;

        Texture2D crosshairTexture;
        Rectangle crosshairRect;

        Texture2D menuTexture;
        Texture2D backgroundTexture;

        SpriteFont menuFont;
        SpriteFont pointFont;

        SoundEffect gunshot;

        MouseState mouseState;
        MouseState prevMouseState;

        float seconds;
        bool timeUp;

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
            points = 0;
            randX = generator.Next(0, 701);
            randY = generator.Next(0, 451);

            playRect = new Rectangle(250, 300, 300, 40);
            instructRect = new Rectangle(250, 360, 300, 40);
            quitRect = new Rectangle(250, 420, 300, 40);
            backRect = new Rectangle(250, 555, 300, 40);
            titleRect = new Rectangle(200, 75, 388, 80);
            crosshairRect = new Rectangle(0, 0, 50, 50);

            blackTargetRect = new Rectangle(100, randY, 100, 150);
            blackTargetSpeed = new Vector2(3, 3);

            greenTargetRect = new Rectangle(randX, 10, 100, 150);
            greenTargetSpeed = new Vector2(4, 4);

            blueTargetRect = new Rectangle(700, randY, 100, 150);
            blueTargetSpeed = new Vector2(-5, 5);

            redTargetRect = new Rectangle(randX, 450, 100, 150);
            redTargetSpeed = new Vector2(10, -10);

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            seconds = 0f;
            timeUp = false;

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
            pointFont = Content.Load<SpriteFont>("point");
            gunshot = Content.Load<SoundEffect>("gunshot");
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
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

            else if (screen == Screen.Game)
            {
                IsMouseVisible = false;
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    gunshot.Play();
                    if (blackTargetRect.Contains(mouseState.Position))
                    {
                        points += 1;
                        randY = generator.Next(0, 451);
                        blackTargetRect = new Rectangle(100, randY, 100, 150);
                        blackTargetSpeed = new Vector2(3, 3);
                    }
                    if (greenTargetRect.Contains(mouseState.Position))
                    {
                        points += 2;
                        randX = generator.Next(0, 701);
                        greenTargetRect = new Rectangle(randX, 10, 100, 150);
                        greenTargetSpeed = new Vector2(4, 4);
                    }
                    if (blueTargetRect.Contains(mouseState.Position))
                    {
                        points += 3;
                        randY = generator.Next(0, 451);
                        blueTargetRect = new Rectangle(700, randY, 100, 150);
                        blueTargetSpeed = new Vector2(-5, 5);
                    }
                    if (redTargetRect.Contains(mouseState.Position))
                    {
                        points += 4;
                        randX = generator.Next(0, 701);
                        redTargetRect = new Rectangle(randX, 450, 100, 150);
                        redTargetSpeed = new Vector2(10, -10);
                    }
                    if (seconds > 30)
                    {
                        seconds = 0f;
                        timeUp = true;
                    }
                }
                if (blackTargetRect.Left < 0)
                {
                    randY = generator.Next(0, 451);
                    blackTargetRect = new Rectangle(100, randY, 100, 150);
                    blackTargetSpeed = new Vector2(3, 3);
                }
                if (greenTargetRect.Top < 0)
                {
                    randX = generator.Next(0, 701);
                    greenTargetRect = new Rectangle(randX, 10, 100, 150);
                    greenTargetSpeed = new Vector2(4, 4);
                }
                if (blueTargetRect.Right > window.Width || blueTargetRect.Top < 0)
                {
                    randY = generator.Next(0, 451);
                    blueTargetRect = new Rectangle(700, randY, 100, 150);
                    blueTargetSpeed = new Vector2(-5, 5);
                }
                if (redTargetRect.Left < 0 || redTargetRect.Bottom > window.Height)
                {
                    randX = generator.Next(0, 701);
                    redTargetRect = new Rectangle(randX, 450, 100, 150);
                    redTargetSpeed = new Vector2(10, -10);
                }

                blackTargetRect.X += (int)blackTargetSpeed.X;
                if (blackTargetRect.Right > window.Width)
                {
                    blackTargetSpeed.X *= -1;
                }
                greenTargetRect.Y += (int)greenTargetSpeed.Y;
                if (greenTargetRect.Bottom > window.Height)
                {
                    greenTargetSpeed.Y *= -1;
                }
                blueTargetRect.X += (int)blueTargetSpeed.X;
                if (blueTargetRect.Left < 0)
                {
                    blueTargetSpeed.X *= -1;
                }
                blueTargetRect.Y += (int)blueTargetSpeed.Y;
                if (blueTargetRect.Bottom > window.Height)
                {
                    blueTargetSpeed.Y *= -1;
                }
                redTargetRect.X += (int)redTargetSpeed.X;
                if (redTargetRect.Right > window.Width)
                {
                    redTargetSpeed.X *= -1;
                }
                redTargetRect.Y += (int)redTargetSpeed.Y;
                if (redTargetRect.Top < 0)
                {
                    redTargetSpeed.Y *= -1;
                }
                if (seconds > 15)
                    screen = Screen.End;
            }
            else if (screen == Screen.Game)
            {
                IsMouseVisible = true;
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
                _spriteBatch.Draw(redTargetTexture, redTargetRect, Color.White);
                _spriteBatch.Draw(blueTargetTexture, blueTargetRect, Color.White);
                _spriteBatch.Draw(greenTargetTexture, greenTargetRect, Color.White);
                _spriteBatch.Draw(blackTargetTexture, blackTargetRect, Color.White);
                _spriteBatch.DrawString(pointFont, "POINTS: " + points, new Vector2(337, 260), Color.Black);
                _spriteBatch.DrawString(pointFont, (15 - seconds).ToString("00.0"), new Vector2(380, 300), Color.Black);
                _spriteBatch.Draw(crosshairTexture, crosshairRect, Color.Red);
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.DrawString(pointFont, "Time's Up!", new Vector2(337, 60), Color.Black);
                if (points > 29)
                    _spriteBatch.DrawString(pointFont, "Absolute perfection!", new Vector2(277, 260), Color.Black);
                else if (points > 19)
                {
                    _spriteBatch.DrawString(pointFont, "Great job, but you haven't reached your limit", new Vector2(5, 260), Color.Black);
                    _spriteBatch.DrawString(pointFont, "yet!", new Vector2(363, 300), Color.Black);
                }
                else if (points > 9)
                    _spriteBatch.DrawString(pointFont, "Not bad, keep trying!", new Vector2(267, 260), Color.Black);
                else if (points > -1)
                    _spriteBatch.DrawString(pointFont, "Ah well, better luck next time!", new Vector2(207, 260), Color.Black);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
