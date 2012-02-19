using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GrandpaBrain;

namespace SaveGramps.GameObjects
{
    class HUD
    {
        public int runningTotal { get; set; }
        public int desiredTotal { get; set; }
        private const Vector2 RUNNING_TOTAL_POSITION = new Vector2(0, 0);
        public static const int BOX_WIDTH = 70;
        public static const int BOX_HEIGHT = 50;
        private const Vector2 DESIRED_TOTAL_POSITION = new Vector2(400, 0);

        public static Texture2D Texture { get; set; }

        public static void Initialize(Texture2D _texture)
        {
            Texture = _texture;
        }

        
        public void Draw(SpriteFont font, SpriteBatch spriteBatch)
        {
            
            Vector2 runningTotalOrigin = font.MeasureString(runningTotal.ToString()) / 2;
            spriteBatch.DrawString(font, runningTotal.ToString(), RUNNING_TOTAL_POSITION, Color.Tomato, 0, runningTotalOrigin, 1.0f, SpriteEffects.None, 0.5f);

            Vector2 desiredTotalOrigin = font.MeasureString(desiredTotal.ToString()) / 2;
            spriteBatch.DrawString(font, desiredTotal.ToString(), RUNNING_TOTAL_POSITION, Color.Tomato, 0, desiredTotalOrigin, 1.0f, SpriteEffects.None, 0.5f);

        }

    }
}
