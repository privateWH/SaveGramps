using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SaveGramps.GameObjects
{
    class AudioManager
    {
        Song bgMusic;
        SoundEffectInstance ballPopped;

        public void Initialize(ContentManager Content)
        {
            bgMusic = Content.Load<Song>("bgmusic");
            ballPopped = (Content.Load<SoundEffect>("Windows Battery Critical")).CreateInstance();
            ballPopped.Volume = 1.0f;
        }
        public void playBgMusic()
        {
            MediaPlayer.IsRepeating = true;                        
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Play(bgMusic);
                        
        }
        public void playBallPopped()
        {
            
            ballPopped.Play();
        }
    }
}
