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
        static Texture2D texture;

        public static void Initialize(Texture2D inTexture)
        {
            texture = inTexture;
        }
        public Ball(Vector2 position)
        {
            
            this.position = position;
        }

        public void Update()
        {
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

        public Boolean hit(float x, float y)
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

        public OperatorBall(Operands value, Vector2 initialPosition)
            : base(initialPosition)
        {
            text = OperandHelper.ConvertOperandToString(value);
        }
    }

    class NumberBall : Ball
    {
        public NumberBall(Int32 value, Vector2 initialPosition)
            : base(initialPosition)
        {
            text = value.ToString();
        }

    }
}
