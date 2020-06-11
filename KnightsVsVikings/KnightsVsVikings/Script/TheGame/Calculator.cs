using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame
{
    // Lucas
    public static class Calculator
    {
        public static bool ApproximatelyEqual(this float a, float b, float threshold)
        {
            return Math.Abs(a - b) < threshold;
        }

        public static bool ApproximatelyEqual(this Vector2 a, Vector2 b, float threshold)
        {
            return Math.Abs(a.X - b.X) < threshold && Math.Abs(a.Y - b.Y) < threshold;
        }
    }
}
