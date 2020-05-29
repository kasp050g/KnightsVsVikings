using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public static class Helper
    {
        public static Random random = new Random();

        /// <summary>
        /// Calculate a random number between the minValue and the maxValue.
        /// </summary>
        /// <param name="minValue">minValue</param>
        /// <param name="maxValue">maxValue</param>
        /// <returns></returns>
        public static int GetRandomValue(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Calculate a random number between 0 and the value.
        /// </summary>
        /// <param name="value">maxValue</param>
        /// <returns></returns>
        public static int GetRandomValue(int value)
        {
            return GetRandomValue(0, value);
        }

        /// <summary>
        /// Calculate Sine 
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float Sin(float degrees)
        {
            return (float)Math.Sin(degrees / 180f * Math.PI);
        }

        /// <summary>
        /// Calculate cosine 
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float Cos(float degrees)
        {
            return (float)Math.Cos(degrees / 180f * Math.PI);
        }

        public static float CalculateAngleBetweenPositions(Vector2 fromPosition, Vector2 toPosition)
        {
            // Calculate position distance in Vector2
            Vector2 vectorTowardsToVector = toPosition - fromPosition;

            // Distance from toPosition.
            float distance = vectorTowardsToVector.Length();

            // Only do calculate if distance is greater than 0.
            if (distance > 0)
            {
                float dot = Vector2.Dot
                (
                    // Vector point right.
                    new Vector2(1, 0),
                    // Vector pointing towards destination.
                    Vector2.Normalize(vectorTowardsToVector)
                );

                // Calculate degrees.
                float degrees = MathHelper.ToDegrees((float)Math.Acos(dot));

                // TODO : donno what to write here
                if (vectorTowardsToVector.Y < 0)
                {
                    degrees = 360 - degrees;
                }

                return degrees;
            }
            else
            {
                return 0;
            }
        }

        public static void UpdateOrigin(GameObject go,Texture2D sprite, EOriginPosition originPositionEnum)
        {
            // --- Top ---

            // top left
            if (EOriginPosition.TopLeft == originPositionEnum)
            {
                go.Transform.Origin = new Vector2(0, 0);
            }
            // top mid
            else if (EOriginPosition.TopMid == originPositionEnum)
            {
                go.Transform.Origin = new Vector2((float)sprite.Width / 2f, 0);
            }
            // top rigth
            else if (EOriginPosition.TopRight == originPositionEnum)
            {
                go.Transform.Origin = new Vector2((float)sprite.Width, 0);
            }

            // --- Mid ---

            // mid left
            else if (EOriginPosition.MidLeft == originPositionEnum)
            {
                go.Transform.Origin = new Vector2(0, (float)sprite.Height / 2f);
            }
            // mid 
            else if (EOriginPosition.Mid == originPositionEnum)
            {
                go.Transform.Origin = new Vector2((float)sprite.Width / 2f, (float)sprite.Height / 2f);
            }
            // mid rigth
            else if (EOriginPosition.MidRight == originPositionEnum)
            {
                go.Transform.Origin = new Vector2((float)sprite.Width, (float)sprite.Height / 2f);
            }

            // --- Bottom ---

            // bottom left
            else if (EOriginPosition.BottomLeft == originPositionEnum)
            {
                go.Transform.Origin = new Vector2(0, (float)sprite.Height);
            }
            // bottom mid
            else if (EOriginPosition.BottomMid == originPositionEnum)
            {
                go.Transform.Origin = new Vector2((float)sprite.Width / 2f, (float)sprite.Height);
            }
            // bottom rigth
            else if (EOriginPosition.BottomRight == originPositionEnum)
            {
                go.Transform.Origin = new Vector2((float)sprite.Width, (float)sprite.Height);
            }

            //Console.WriteLine("Error: in |Class: Helper|Method: UpdateOrigin| dint work as it neeed to");
            //go.Transform.Origin = new Vector2(0,0);
        }

        public static void UpdateOriginText(string text, Rectangle rectangle,SpriteFont spriteFont,Vector2 Scale, EOriginPosition originPositionEnum,ref Vector2 newPositon)
        {
            // --- Top ---

            // top left
            if (EOriginPosition.TopLeft == originPositionEnum)
            {
                var x = rectangle.X;
                var y = rectangle.Y;
                newPositon = new Vector2(x, y);
            }
            // top mid
            else if (EOriginPosition.TopMid == originPositionEnum)
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (spriteFont.MeasureString(text).X / 2) * Scale.X;
                var y = rectangle.Y;
                newPositon = new Vector2(x, y);
            }
            // top rigth
            else if (EOriginPosition.TopRight == originPositionEnum)
            {
                var x = (rectangle.X + (rectangle.Width / 1)) - (spriteFont.MeasureString(text).X / 1) * Scale.X;
                var y = rectangle.Y;
                newPositon = new Vector2(x, y);
            }

            // --- Mid ---

            // mid left
            else if (EOriginPosition.MidLeft == originPositionEnum)
            {
                var x = rectangle.X;
                var y = (rectangle.Y + (rectangle.Height / 2)) - (spriteFont.MeasureString(text).Y / 2) * Scale.Y;
                newPositon = new Vector2(x, y);
            }
            // mid 
            else if (EOriginPosition.Mid == originPositionEnum)
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (spriteFont.MeasureString(text).X / 2) * Scale.X;
                var y = (rectangle.Y + (rectangle.Height / 2)) - (spriteFont.MeasureString(text).Y / 2) * Scale.Y;
                newPositon = new Vector2(x, y);
            }
            // mid rigth
            else if (EOriginPosition.MidRight == originPositionEnum)
            {
                var x = (rectangle.X + (rectangle.Width / 1)) - (spriteFont.MeasureString(text).X / 1) * Scale.X;
                var y = (rectangle.Y + (rectangle.Height / 2)) - (spriteFont.MeasureString(text).Y / 2) * Scale.Y;
                newPositon = new Vector2(x, y);
            }

            // --- Bottom ---

            // bottom left
            else if (EOriginPosition.BottomLeft == originPositionEnum)
            {
                var x = rectangle.X;
                var y = (rectangle.Y + (rectangle.Height / 1)) - (spriteFont.MeasureString(text).Y / 1) * Scale.Y;
                newPositon = new Vector2(x, y);
            }
            // bottom mid
            else if (EOriginPosition.BottomMid == originPositionEnum)
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (spriteFont.MeasureString(text).X / 2) * Scale.X;
                var y = (rectangle.Y + (rectangle.Height / 1)) - (spriteFont.MeasureString(text).Y / 1) * Scale.Y;
                newPositon = new Vector2(x, y);
            }
            // bottom rigth
            else if (EOriginPosition.BottomRight == originPositionEnum)
            {
                var x = (rectangle.X + (rectangle.Width / 1)) - (spriteFont.MeasureString(text).X / 1) * Scale.X;
                var y = (rectangle.Y + (rectangle.Height / 1)) - (spriteFont.MeasureString(text).Y / 1) * Scale.Y;
                newPositon = new Vector2(x, y);
            }

            //Console.WriteLine("Error: in |Class: Helper|Method: UpdateOrigin| dint work as it neeed to");
            //go.Transform.Origin = new Vector2(0,0);
        }
    }
}
