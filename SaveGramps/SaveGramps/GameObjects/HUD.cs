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
        public int wakeUpTotal { get; set; }
        Vector2 RUNNING_TOTAL_POSITION = new Vector2(25, 25);
        public const int BOX_WIDTH = 70;
        public const int BOX_HEIGHT = 50;
        Vector2 DESIRED_TOTAL_POSITION = new Vector2(400, 25);
        Vector2 WAKEUP_TOTAL_POSITION = new Vector2(800, 25);
        public Texture2D tx2WakeUpGrandPa;
        public static Texture2D Texture { get; set; }

   
        public void Draw(SpriteFont font, SpriteBatch spriteBatch)
        {
            
            Vector2 runningTotalOrigin = font.MeasureString(runningTotal.ToString()) / 2;
            spriteBatch.DrawString(font, "Round: " + runningTotal.ToString(), RUNNING_TOTAL_POSITION, Color.Tomato, 0, runningTotalOrigin, 1.0f, SpriteEffects.None, 0.5f);

            Vector2 desiredTotalOrigin = font.MeasureString(desiredTotal.ToString()) / 2;
            spriteBatch.DrawString(font, desiredTotal.ToString(), DESIRED_TOTAL_POSITION, Color.Tomato, 0, desiredTotalOrigin, 1.0f, SpriteEffects.None, 0.5f);
            for (int i=wakeUpTotal; i > 0 ; --i){
                spriteBatch.Draw(tx2WakeUpGrandPa, new Rectangle(580 + i * 55, 0, 50, 50),Color.White);
            }
        }

    }
}
