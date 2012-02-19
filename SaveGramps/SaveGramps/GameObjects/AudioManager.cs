using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SaveGramps.GameObjects
{
    class AudioManager
    {
        public void playBgMusic()
        {
            //PlaySound("bgmusic.mp3");
        }
        private void PlaySound(string path)
        {

                if (!string.IsNullOrEmpty(path))
                {
                    using (var stream = TitleContainer.OpenStream(path))
                    {
                        if (stream != null)
                        {
                            var effect = SoundEffect.FromStream(stream);
                            FrameworkDispatcher.Update();
                            effect.Play();
                        }
                    }
                }


        }
    }
}
