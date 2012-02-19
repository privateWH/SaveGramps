using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SaveGramps.GameObjects;
using GrandpaBrain;

namespace SaveGramps
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ballTexture;
        Texture2D backgroundTexture;
        List<Ball> balls ;
        SpriteFont arialFont;
        
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Random random1 = new Random();
            Random random2 = new Random();

            balls = new List<Ball>();
            for (int i = 0; i < 5; i++)
            {
                Vector2 position = new Vector2((i % 2 == 0) ? random2.Next(0, 400) : random2.Next(400, 800), 400);
                Ball ball = new NumberBall(
                        random1.Next(1, 10), 
                        position,
                        (position.X > this.graphics.GraphicsDevice.Viewport.Width / 2) ? -1 : 1
                        );
                balls.Add(ball);
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("smallball");
            arialFont = Content.Load<SpriteFont>("Arial");
            backgroundTexture = Content.Load<Texture2D>("background");
            Ball.Initialize(ballTexture);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            Ball hitBall = null;
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                if ((tl.State == TouchLocationState.Pressed)
                       /* || (tl.State == TouchLocationState.Moved)*/)
                {
                    foreach (Ball ball in balls) {
                        if (ball.Hit(tl.Position.X, tl.Position.Y))
                        {
                            hitBall = ball;
                            break;
                        }
                    }
                }
            }
            if (hitBall != null)
            {
                // call AddNumber or AddOperand
                balls.Remove(hitBall);

            }

            // check if answer is correct or

            // TODO check if balls have left the screen

            // TODO: show subtotal
            

            // Update ball locations

            foreach (Ball ball in balls)
            {
                ball.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            foreach(Ball ball in balls) {
                ball.Draw(arialFont, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
