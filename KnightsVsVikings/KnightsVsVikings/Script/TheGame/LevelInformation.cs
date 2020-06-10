using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame
{
    public class LevelInformation
    {
        public byte TileSize { get; } = 64;
        public GameObject[,] BuildingTileGrid { get; set; }
        public GameObject[,] ResourcesTileGrid { get; set; }

        public List<GameObject> GetBuildingTiles()
        {
            return MatrixToList(BuildingTileGrid);
        }

        public List<GameObject> GetResourceTiles()
        {
            return MatrixToList(ResourcesTileGrid);
        }

        private List<GameObject> MatrixToList(GameObject[,] matrix)
        {
            List<GameObject> result = new List<GameObject>();

            for (int x = 0; x < matrix.GetLength(0); x++)
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[x, y] != null)
                        result.Add(matrix[x, y]);
                }

            return result;
        }
    }
}
