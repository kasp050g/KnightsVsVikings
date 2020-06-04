using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.ExtensionMethods
{
    public static class ArrayExtend
    {
        public static Cell[,] ToAstarArray(this Cell[,] array)
        {
            Cell[,] result = new Cell[array.GetLength(0), array.GetLength(1)];

            for (int x = 0; x < array.GetLength(0); x++)
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    result[x, y] = array[x, y];
                    result[x, y].GridPos = new Vector2(x, y);
                }

            return result;
        }
    }
}
