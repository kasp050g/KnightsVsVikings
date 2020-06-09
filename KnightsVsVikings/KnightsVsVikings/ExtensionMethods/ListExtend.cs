using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Enum;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.ExtensionMethods
{
    public static class ListExtend
    {

        /// <summary>
        /// Converts a multidimensional array of Cells to a List of Cells.
        /// </summary>
        /// <param name="matrix">Multidimensional array to convert.</param>
        /// <returns></returns>
        public static List<Cell> ToAstarList(this Cell[,] matrix)
        {
            List<Cell> result = new List<Cell>();

            for (int x = 0; x < matrix.GetLength(0); x++)
                for (int y = 0; y < matrix.GetLength(1); y++)
                    result.Add(new Cell(matrix[x, y].ECellType, matrix[x, y].GridPos));

            return result;
        }

        ///// <summary>
        ///// Converts a multidimensional array of GameObjects to a List of Cells.
        ///// </summary>
        ///// <param name="matrix">Multidimensional array to convert.</param>
        ///// <returns></returns>
        //public static List<Cell> ToList(GameObject[,] matrix)
        //{
        //    List<Cell> result = new List<Cell>();
        //
        //    for (int x = 0; x < matrix.GetLength(0); x++)
        //        for (int y = 0; y < matrix.GetLength(1); y++)
        //            switch (matrix[x, y].GetComponent<CTile>().TileType)
        //            {
        //                case ETileType.Grass:
        //                    result.Add(new Cell(ECellType.Default, new Vector2(x, y)));
        //                    break;
        //
        //                default:
        //                    result.Add(new Cell(ECellType.Invalid, new Vector2(x, y)));
        //                    break;
        //            }
        //
        //    return result;
        //}
    }
}
