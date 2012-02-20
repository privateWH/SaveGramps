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
        Vector2 RUNNING_TOTAL_POSITION = new Vector2(15, 15);
        public const int BOX_WIDTH = 70;
        public const int BOX_HEIGHT = 50;
        Vector2 DESIRED_TOTAL_POSITION = new Vector2(400, 25);
        Vector2 WAKEUP_TOTAL_POSITION = new Vector2(800, 25);
        public Texture2D tx2WakeUpGrandPa;
        public static Texture2D Texture { get; set; }
        private TimeSpan typeGetText = new TimeSpan(0, 0,2);
        private TimeSpan typingGetText = new TimeSpan();

        public void Draw(SpriteFont font, SpriteBatch spriteBatch, GameTime gtime)
        {
            Vector2 desiredTotalOrigin = font.MeasureString(desiredTotal.ToString()) / 2;
            spriteBatch.DrawString(font, desiredTotal.ToString(), new Vector2(402, 27), Color.Black, 0, desiredTotalOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(font, desiredTotal.ToString(), DESIRED_TOTAL_POSITION, Color.White, 0, desiredTotalOrigin, 1.0f, SpriteEffects.None, 0.5f);
            
            //typingGetText += gtime.ElapsedGameTime;
            //if (typingGetText > typeGetText) typingGetText = new TimeSpan(0, 0, 0);
            //int numCharToShow = ((int)(((double)desiredTotal.ToString().Length) * (typingGetText.TotalMilliseconds / typeGetText.TotalMilliseconds)));
            //string strToDraw = desiredTotal.ToString().Substring(0, numCharToShow);
            //spriteBatch.DrawString(font, strToDraw, DESIRED_TOTAL_POSITION, Color.White, 0, desiredTotalOrigin, 1.0f, SpriteEffects.None, 0.5f);


            for (int i=wakeUpTotal; i > 0 ; --i){
                spriteBatch.Draw(tx2WakeUpGrandPa, new Rectangle(580 + i * 45, 0, 40, 40),Color.White);
            }

            //spriteBatch.DrawString(font, strToDraw, new Vector2(270,-1), Color.White);
        }

        public void DrawRoundTotal(SpriteFont font, SpriteBatch spriteBatch)
        {
            Vector2 runningTotalOrigin = font.MeasureString(runningTotal.ToString()) / 2;
            spriteBatch.DrawString(font, "LV:" + runningTotal.ToString(), new Vector2(2, -3), Color.Black);
            spriteBatch.DrawString(font, "LV:" + runningTotal.ToString(), new Vector2(0, -5), Color.White);
            //spriteBatch.DrawString(font, "Lv:" + runningTotal.ToString(), RUNNING_TOTAL_POSITION, Color.White, 0, runningTotalOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

    }
}
