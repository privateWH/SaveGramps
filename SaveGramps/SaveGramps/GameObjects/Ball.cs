using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GrandpaBrain;

namespace SaveGramps.GameObjects
{
    class Ball
    {
        public Vector2 position;
        public String text;
        public double time;
        public int xVelocityMultiplier;
        static Texture2D texture;
        public Vector2 initialVelocity;

        public static void Initialize(Texture2D _texture)
        {
            texture = _texture;
        }
        public Ball(Vector2 position, int xVelocityMultiplier)
        {
            Random random = new Random();
            this.position = position;
            this.xVelocityMultiplier = xVelocityMultiplier;
            this.initialVelocity = new Vector2(random.Next(0,3), random.Next(9,12));
        }

        public void Update(GameTime gameTime)
        {
            Console.WriteLine("time: " + gameTime.ElapsedGameTime.TotalSeconds);
            this.position.Y = this.position.Y + (float)(-1 * initialVelocity.Y * time + 7.8 * time * time / 2);// + Ball.viewPort.Height - texture.Height;
            this.position.X = this.position.X + (float)(xVelocityMultiplier * initialVelocity.X * time);
            this.time = this.time + gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteFont font, SpriteBatch spriteBatch)
        {
            // TODO: Increase font size
            Vector2 FontOrigin = font.MeasureString(text) / 2;
            spriteBatch.Draw(texture, this.position, Color.White);
            float offsetX = texture.Width / 2 + position.X;
            float offsetY = texture.Height / 2 + position.Y;
            Vector2 fontCenter = new Vector2(offsetX, offsetY);
            spriteBatch.DrawString(font, text, fontCenter, Color.Tomato, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        public Boolean Hit(float x, float y)
        {
            if (x > position.X && x < (position.X + texture.Width) &&
                y > position.Y && y < (position.Y + texture.Height))
            {
                // TODO: Show some animation?
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class OperatorBall : Ball
    {

        public OperatorBall(Operands value, Vector2 initialPosition, int xVelocityMultiplier)
            : base(initialPosition, xVelocityMultiplier)
        {
            text = OperandHelper.ConvertOperandToString(value);
        }
    }

    class NumberBall : Ball
    {
        public NumberBall(Int32 value, Vector2 initialPosition, int xVelocityMultiplier)
            : base(initialPosition, xVelocityMultiplier)
        {
            text = value.ToString();
        }

    }
}
