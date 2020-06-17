using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    // Kasper  Fly
    public class AudioContainer
    {
        #region Singleton
        private static AudioContainer instance;
        public static AudioContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioContainer();
                }
                return instance;
            }
        }
        #endregion

        private Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        private  Dictionary<string, Song> songs = new Dictionary<string, Song>();

        public  Dictionary<string, SoundEffect> SoundEffects { get => soundEffects; private set => soundEffects = value; }
        public  Dictionary<string, Song> Songs { get => songs; private set => songs = value; }

        public  void LoadContent(ContentManager content)
        {
            //Songs
            AddSongs(content.Load<Song>("Audio/song/song1"), "song1");
            AddSongs(content.Load<Song>("Audio/song/song2"), "song2");
            AddSongs(content.Load<Song>("Audio/song/song3"), "song3");

            //Sound Effects
            AddSoundEffects(content.Load<SoundEffect>("Audio/UI/buttonClick"), "buttonClick");
            AddSoundEffects(content.Load<SoundEffect>("Audio/UI/buttonHoring"), "buttonHoring");
        }

        private  void AddSongs(Song song, string name)
        {
            Songs.Add(name, song);
        }
        private  void AddSoundEffects(SoundEffect soundEffect, string name)
        {
            SoundEffects.Add(name, soundEffect);
        }

        /// <summary>
        /// Play a song
        /// </summary>
        /// <param name="name">Name of song</param>
        /// <param name="volume">Volume of song</param>
        public void PlaySong(string name, float volume)
        {
            MediaPlayer.Stop();
            Song tmp = Songs[name];

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
            SoundEffect tmp = SoundEffects[name];
            tmp.Play(volume: volume, pitch: 0.0f, pan: 0.0f);
        }
    }
}
