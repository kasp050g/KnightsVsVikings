using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.WorldEditor.UILogic
{
    class TestJmaneKolo
    {

        public void GetTileData(GameObject[,] groundTileGrid,GameObject tile,Vector2 gridPos)
        {
            string com = string.Empty;

            for (int x = -1; x <= 1; x++)//Runs through all neighbours
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0) //Makes sure that we aren't checking our self
                    {
                        //If the value is a watertile
                        if(x + gridPos.X >= 0 && x + gridPos.X <= groundTileGrid.GetLength(0) && y + gridPos.Y >= 0 && y + gridPos.Y <= groundTileGrid.GetLength(1))
                        {
                            if (groundTileGrid[x, y].GetComponent<CTile>().TileType == ETileType.Water)
                            {
                                com += 'W';
                            }
                            else
                            {
                                com += 'E';
                            }
                        }
                        else
                        {
                            com += "E";
                        }
                    }
                }
            }

            //int randomVal = Random.Range(0, 100);

            //if (randomVal < 15)
            //{
            //    tileData.sprite = waterSprites[46];
            //}
            //else if (randomVal >= 15 && randomVal < 35)
            //{

            //    tileData.sprite = waterSprites[48];
            //}
            //else
            //{
            //    tileData.sprite = waterSprites[47];
            //}

            CTile _tile = tile.GetComponent<CTile>();
            CSpriteRenderer _sr = tile.GetComponent<CSpriteRenderer>();

            if (com[1] == 'E' && com[3] == 'E' && com[4] == 'E' && com[6] == 'E')
            {
                
            }
            else if (com[0] == 'W' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
            {
                //Bottom Left
                _tile.TileType = ETileType.WaterGrass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater05;
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'W' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
            {
                //Top Left
                _tile.TileType = ETileType.WaterGrass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater08;
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
            {
                // No Water
                _tile.TileType = ETileType.Grass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.Grass01;
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'W' && com[6] == 'E' && com[7] == 'E')
            {
                // Bottom Rigth
                _tile.TileType = ETileType.WaterGrass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater01;
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'W')
            {
                // Top Rigth
                _tile.TileType = ETileType.WaterGrass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater04;
            }
            //--- to the side
            else if (com[0] == 'E' && com[1] == 'W' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
            {
                // Left
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'E' && com[5] == 'E' && com[6] == 'W' && com[7] == 'E')
            {
                // Rigth
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'W' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
            {
                // Top
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'W' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
            {
                // Bottom
            }
            //--- 2 side
            else if (com[0] == 'E' && com[1] == 'W' && com[2] == 'E' && com[3] == 'W' && com[4] == 'E' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
            {
                // Left Bottom
                _tile.TileType = ETileType.WaterGrass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater15;
            }
            else if (com[0] == 'E' && com[1] == 'W' && com[2] == 'E' && com[3] == 'E' && com[4] == 'W' && com[5] == 'E' && com[6] == 'E' && com[7] == 'E')
            {
                // Left Top
                _tile.TileType = ETileType.WaterGrass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater13;
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'W' && com[4] == 'E' && com[5] == 'E' && com[6] == 'W' && com[7] == 'E')
            {
                // Rigth Bottom
                _tile.TileType = ETileType.WaterGrass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater16;
            }
            else if (com[0] == 'E' && com[1] == 'E' && com[2] == 'E' && com[3] == 'E' && com[4] == 'W' && com[5] == 'E' && com[6] == 'W' && com[7] == 'E')
            {
                // Rigth Top
                _tile.TileType = ETileType.WaterGrass;
                _sr.SpriteSheet = SpriteContainer.Instance.TileSprite.GrassWater14;
            }
            else
            {
                // Error Tile
            }
        }
    }

}
