// #define DDEBUG

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
    enum GameStates
    {
        Start,
        RefreshLevel,
        PlayLevel,
        RoundReward,
        EndLevel
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        static Random gRandom = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ballTexture;
        Texture2D backgroundTexture;
        List<Ball> balls ;
        HUD hud;
        SpriteFont arialFont;
        GameStates gameState = GameStates.RefreshLevel;
        int lvHandler;
        Answer answerInBrain;
        AudioManager audioManager = new AudioManager();
        TimeSpan roundRewardMessageTimeout = new TimeSpan(0, 0, 1);
        TimeSpan accumulateTime = new TimeSpan(0, 0, 0);
        bool drawMessage = false;
        bool winOrLose = false;
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
            DefaultLevel defaultLv = new DefaultLevel();
            lvHandler = Generator.RegisterLevel(defaultLv);
            base.Initialize();
            audioManager.playBgMusic();
            hud = new HUD();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("smallballcolorshadow");
            arialFont = Content.Load<SpriteFont>("Arial");
            backgroundTexture = Content.Load<Texture2D>("background");
            Ball.Initialize(ballTexture);
            HUD.Initialize(new Texture2D(GraphicsDevice, HUD.BOX_WIDTH, HUD.BOX_HEIGHT));
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
            switch (gameState)
            {
                case GameStates.Start:
                    gameState = GameStates.RefreshLevel;
                    break;
                case GameStates.RefreshLevel:
                    {
                        if (drawMessage)
                        {
                            this.accumulateTime += gameTime.ElapsedGameTime;
                            if (this.accumulateTime >= this.roundRewardMessageTimeout)
                            {
                                drawMessage = false;
                                accumulateTime = new TimeSpan(0, 0, 0);
                            }
                        }

                        int maxRightPosition = graphics.GraphicsDevice.Viewport.Width - Ball.Texture.Width;

                        balls = new List<Ball>();

                        // query balls from Grandpa's Brain
                        Response expectedResponse = Generator.GetExpectedResponseByLevel(lvHandler);

                        hud.desiredTotal = expectedResponse.Answer;
                        answerInBrain = new Answer(expectedResponse);

                        int numOfBalls = expectedResponse.Numbers.Count() + expectedResponse.Operands.Count();
                        int viewWidth = graphics.GraphicsDevice.Viewport.Width - Ball.Texture.Width;
                        int sizeOfDivision = viewWidth / numOfBalls;

                        int buffer = sizeOfDivision / 3;
                        List<int> positions = new List<int>();
                        for (int i = 0; i < numOfBalls; i++)
                        {
                            int startPos = i * sizeOfDivision;
                            int xVal = gRandom.Next(startPos + buffer, (startPos + sizeOfDivision) - buffer);
                            positions.Add(xVal);
                        }

                        int xPos = 0;
#if DDEBUG
                        int i = 0;
#endif

                        foreach(var num in expectedResponse.Numbers)
                        {
#if DDEBUG
                            Vector2 position = new Vector2(100 * i, 100); i++;
#else
                            xPos = HelperMethods.GetRandomElement<int>(positions);
                            Vector2 position = new Vector2(xPos, 485);
#endif
                            Ball ball = new NumberBall(
                                    num,
                                    position,
                                    (position.X > this.graphics.GraphicsDevice.Viewport.Width / 2) ? -1 : 1
                                    );
                            balls.Add(ball);
                        }

#if DDEBUG
                        i = 0;
#endif

                        foreach (var op in expectedResponse.Operands)
                        {

#if DDEBUG
                            Vector2 position = new Vector2(100 * i, 300); i++;
#else
                            xPos = HelperMethods.GetRandomElement<int>(positions);
                            Vector2 position = new Vector2(xPos, 485);
#endif
                            Ball ball = new OperatorBall(
                                    op,
                                    position,
                                    (position.X > this.graphics.GraphicsDevice.Viewport.Width / 2) ? -1 : 1
                                    );
                            balls.Add(ball);                            
                        }

                        gameState = GameStates.PlayLevel;
                        break;
                    }
                case GameStates.RoundReward:
                    break;
                case GameStates.PlayLevel:
                    {
                        if (drawMessage)
                        {
                            this.accumulateTime += gameTime.ElapsedGameTime;
                            if (this.accumulateTime >= this.roundRewardMessageTimeout)
                            {
                                drawMessage = false;
                                accumulateTime = new TimeSpan(0, 0, 0);
                            }
                        }

                        foreach (TouchLocation tl in touchCollection)
                        {
                            if ((tl.State == TouchLocationState.Pressed)
                                /* || (tl.State == TouchLocationState.Moved)*/)
                            {
                                foreach (Ball ball in balls)
                                {
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
                            if (hitBall.ballType == BallType.Number)
                            {
                                answerInBrain.AddNumber(int.Parse(hitBall.text));
                            }
                            else if (hitBall.ballType == BallType.Operand)
                            {
                                answerInBrain.AddOperand(OperandHelper.ConvertStringToOperands(hitBall.text));
                            }
                            else
                            {
                                throw new Exception("The hitBall.BallType is not yet implemented");
                            }
                            balls.Remove(hitBall);

                        }

                        // check if answer is correct or
                        string termMsg;
                        TerminateCond cond;
                        if (answerInBrain.ShouldTerminate(out cond, out termMsg))
                        {
                            switch (cond)
                            {
                                case TerminateCond.Normal:
                                    hud.runningTotal = hud.runningTotal + 1;
                                    drawMessage = true;
                                    winOrLose = true;
                                    gameState = GameStates.RefreshLevel;
                                    //gameState = GameStates.RoundReward;
                                    break;
                                case TerminateCond.Impossible: //update to a display this new picture
                                    drawMessage = true;
                                    winOrLose = false;
                                    gameState = GameStates.RefreshLevel;
                                    //gameState = GameStates.RoundReward;
                                    break;
                                case TerminateCond.Timeout:
                                    throw new Exception("Timeout");
                                    gameState = GameStates.RefreshLevel;
                                    break;
                                case TerminateCond.NoTerminate:
                                    throw new Exception("NoTerminate");
                                    break;
                            }
                        }

                        // TODO: end game state when balls left screen
                        // Update ball locations
                        for(int i = balls.Count - 1; i >= 0; i--)
                        {
                            Ball ball = balls[i];
#if DDEBUG
                            //ball.Update(gameTime);
#else
                            ball.Update(gameTime);
#endif

                            // check if ball is off the screen
                            if (((ball.position.X + Ball.Texture.Width) <= 0) || (ball.position.X > graphics.GraphicsDevice.Viewport.Width) ||
                                (ball.position.Y > graphics.GraphicsDevice.Viewport.Height + 10))
                            {
                                balls.RemoveAt(i);
                            }

                        }
                        
                        if (balls.Count == 0)
                        {
                            gameState = GameStates.RefreshLevel;
                        }
                        break;
                    }
                case GameStates.EndLevel:
                    break;
                default:
                    break;
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

            switch(gameState)
            {
                case GameStates.Start:
                {
                    break;
                }
                case GameStates.RefreshLevel:
                {
                    if (drawMessage)
                    {
                        spriteBatch.Begin();
                        if (winOrLose)
                        {
                            spriteBatch.DrawString(arialFont, "You Won!", new Vector2(400, 240), Color.Red);
                        }
                        else
                        {

                            spriteBatch.DrawString(arialFont, "You Lose!", new Vector2(400, 240), Color.Red);
                        }
                        spriteBatch.End();
                    }
                    break;
                }
                case GameStates.RoundReward:
                {
                    break;
                }
                case GameStates.PlayLevel:
                {

                    if (drawMessage)
                    {
                        spriteBatch.Begin();
                        if (winOrLose)
                        {
                            spriteBatch.DrawString(arialFont, "You Won!", new Vector2(400, 240), Color.Red);
                        }
                        else
                        {

                            spriteBatch.DrawString(arialFont, "You Lose!", new Vector2(400, 240), Color.Red);
                        }
                        spriteBatch.End();
                    }

                    spriteBatch.Begin();
                    foreach (Ball ball in balls)
                    {
                        ball.Draw(arialFont, spriteBatch);
                    }
                    hud.Draw(arialFont, spriteBatch);
                    spriteBatch.End();
                    break;
                }
                case GameStates.EndLevel:
                {
                    break;
                }
            }

            base.Draw(gameTime);
        }
    }

    public static class HelperMethods
    {
        private static Random random = new Random();

        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list.Count() == 0)
            {
                throw new Exception("no more elements in list");
            }
            int pos = random.Next(list.Count());
            T result = list[pos];
            list.RemoveAt(pos);
            return result;
        }
    }
}
