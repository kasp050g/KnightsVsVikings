using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public static class Time
    {
        public static float DeltaTime { get; private set; }
        public static float TotalTime { get; private set; }

        public static void Update(GameTime gameTime)
        {
            Time.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Time.TotalTime = (float)gameTime.TotalGameTime.TotalSeconds;
        }
    }
}
