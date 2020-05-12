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
            // Songs
            //AddSongs(content.Load<Song>("Audio/Adventure/Song/rpgSong"), "AdventureSong");

            // Sound Effects
            //AddSoundEffects(content.Load<SoundEffect>("Audio/Adventure/Sound/GunOutOfAmmoSound"), "GunOutOfAmmoSound");
        }

        private  void AddSongs(Song song, string name)
        {
            Songs.Add(name, song);
        }
        private  void AddSoundEffects(SoundEffect soundEffect, string name)
        {
            SoundEffects.Add(name, soundEffect);
        }
    }
}
