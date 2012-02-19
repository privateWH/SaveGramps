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
        public static Texture2D Texture { get; set; }
        public Vector2 initialVelocity;

        public static void Initialize(Texture2D _texture)
        {
            Texture = _texture;
        }

        public Ball(Vector2 position, int xVelocityMultiplier)
        {
            Random random = new Random();
            double x = random.NextDouble();
            double y = random.NextDouble();
            this.position = position;
            this.xVelocityMultiplier = xVelocityMultiplier;
            this.initialVelocity = new Vector2((float)(x * 3.5), 9 + (float)(y * 3.5));
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
            Vector2 FontOrigin = font.MeasureString(text) / 2;
            spriteBatch.Draw(Texture, this.position, Color.White);
            float offsetX = Texture.Width / 2 + position.X;
            float offsetY = Texture.Height / 2 + position.Y;
            Vector2 fontCenter = new Vector2(offsetX, offsetY);
            spriteBatch.DrawString(font, text, fontCenter, Color.Tomato, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        public Boolean Hit(float x, float y)
        {
            if (x > position.X && x < (position.X + Texture.Width) &&
                y > position.Y && y < (position.Y + Texture.Height))
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
