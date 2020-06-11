using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    // Lucas
    public class AstarGlobal
    {
        public Cell[,] BaseMapGrid { get; private set; } = null;
        public GameObject[,] TileGrid { get; private set; } = null;

        public void InitializeGrids(GameObject[,] tileGrid)
        {
            BaseMapGrid = GameObjectMatrixToCellMatrix(tileGrid);
            TileGrid = tileGrid;
        }

        private Cell[,] GameObjectMatrixToCellMatrix(GameObject[,] matrix)
        {
            Cell[,] result = new Cell[matrix.GetLength(0), matrix.GetLength(1)];

            for (int x = 0; x < matrix.GetLength(0); x++)
                for (int y = 0; y < matrix.GetLength(1); y++)
                    switch(matrix[x, y].GetComponent<CTile>().TileType)
                    {
                        case ETileType.Grass:
                            result[x, y] = new Cell(Enum.ECellType.Default);
                            break;

                        default:
                            result[x, y] = new Cell(Enum.ECellType.Invalid);
                            break;
                    }

            return result;
        }
    }
}
