using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Enum;
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

            //Cell[,] result = (Cell[,])array.Clone();

            for (int x = 0; x < array.GetLength(0); x++)
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    switch (array[x, y].ECellType)
                    {
                        case ECellType.Invalid:
                            result[x, y] = new Cell(ECellType.Invalid);
                            break;

                        case ECellType.Attackable:
                            result[x, y] = new Cell(ECellType.Attackable);
                            break;

                        default:
                            result[x, y] = new Cell(ECellType.Default);
                            break;
                    }
                    result[x, y].GridPos = new Vector2(x, y);
                }

            return result;
        }

        public static void Print2DArray(this Cell[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write((int)matrix[i, j].ECellType + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
