using MainSystemFramework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class CPlayAudio : Component
    {
        public CPlayAudio()
        {

        }

        #region Methods 
        /// <summary>
        /// Play a song
        /// </summary>
        /// <param name="name">Name of song</param>
        /// <param name="volume">Volume of song</param>
        public void PlaySong(string name, float volume)
        {
            MediaPlayer.Stop();
            Song tmp = AudioContainer.Instance.Songs[name];

            MediaPlayer.Play(tmp);
            MediaPlayer.Volume = volume;
            MediaPlayer.IsRepeating = true;
        }
        /// <summary>
        /// Stop a song
        /// </summary>
        public void StopSong()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Play a soundEffect
        /// </summary>
        /// <param name="name">Name of soundEffect</param>
        /// <param name="volume">Volume of soundEffect</param>
        public void PlaySoundEffect(string name, float volume)
        {
            SoundEffect tmp = AudioContainer.Instance.SoundEffects[name];
            tmp.Play(volume: volume, pitch: 0.0f, pan: 0.0f);
        }
        #endregion
    }
}
