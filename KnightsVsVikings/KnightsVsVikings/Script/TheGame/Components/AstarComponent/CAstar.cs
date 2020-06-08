using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class CAstar : Component
    {
        Stack<CTile> tiles = new Stack<CTile>();
        public CTile currentTile { get; set; }
        CTile nextTile;
        CUnit unit;
        CMove cMove;
        EFacingDirection direction;
        bool directionCheck = false;
        MasterAstar Astar_Test;
        List<CTile> tileList;

        bool runAstar = false;

        public CAstar(CUnit unit)
        {
            this.unit = unit;
        }

        public override void Awake()
        {
            base.Awake();
            cMove = GameObject.GetComponent<CMove>();
            Astar_Test = new MasterAstar();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            if (runAstar)
            {
                MoveUnit();
            }
        }

        public void GetAstar(CTile goal,TileGrid tileGrid)
        {
            tiles.Clear();

            tileList = new List<CTile>();

            for (int x = 0; x < tileGrid.groundTileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.groundTileGrid.GetLength(1); y++)
                {
                    tileList.Add(tileGrid.groundTileGrid[x, y].GetComponent<CTile>());
                }
            }

            List<CTile> tmp = new List<CTile>(tileList);
            tiles = Astar_Test.GetAstarWay((GameObject.Transform.Position == currentTile.GameObject.Transform.Position ? currentTile  : nextTile), goal, tmp);
            
            directionCheck = true;
            runAstar = true;
        }

        public void MoveUnit()
        {
            if (directionCheck)
            {
                if (nextTile == null)
                {
                    nextTile = tiles.Pop();
                }
                nextTile.IsUnitOccupied = true;

                float xPos = Math.Abs(GameObject.Transform.Position.X - nextTile.GameObject.Transform.Position.X);
                float yPos = Math.Abs(GameObject.Transform.Position.Y - nextTile.GameObject.Transform.Position.Y);

                if (GameObject.Transform.Position.X > nextTile.GameObject.Transform.Position.X && xPos > yPos)
                {
                    direction = EFacingDirection.Left;
                    directionCheck = false;
                }
                else if (GameObject.Transform.Position.X < nextTile.GameObject.Transform.Position.X && xPos > yPos)
                {
                    direction = EFacingDirection.Rigth;
                    directionCheck = false;
                }
                else if (GameObject.Transform.Position.Y < nextTile.GameObject.Transform.Position.Y && xPos < yPos)
                {
                    direction = EFacingDirection.Down;
                    directionCheck = false;
                }
                else if (GameObject.Transform.Position.Y > nextTile.GameObject.Transform.Position.Y && xPos < yPos)
                {
                    direction = EFacingDirection.Up;
                    directionCheck = false;
                }
                else
                {
                    Console.WriteLine("Error in C_FollowPath");
                }
            }

            switch (direction)
            {
                case EFacingDirection.Up:
                    if (GameObject.Transform.Position.Y < nextTile.GameObject.Transform.Position.Y)
                    {
                        SetNowTile();
                    }
                    else
                    {
                        cMove.Velocity = new Vector2(0, -1);
                    }
                    break;
                case EFacingDirection.Down:
                    if (GameObject.Transform.Position.Y > nextTile.GameObject.Transform.Position.Y)
                    {
                        SetNowTile();
                    }
                    else
                    {
                        cMove.Velocity = new Vector2(0, 1);
                    }
                    break;
                case EFacingDirection.Left:
                    if (GameObject.Transform.Position.X < nextTile.GameObject.Transform.Position.X)
                    {
                        SetNowTile();
                    }
                    else
                    {
                        cMove.Velocity = new Vector2(-1, 0);
                    }
                    break;
                case EFacingDirection.Rigth:
                    if (GameObject.Transform.Position.X > nextTile.GameObject.Transform.Position.X)
                    {
                        SetNowTile();
                    }
                    else
                    {
                        cMove.Velocity = new Vector2(1, 0);
                    }
                    break;
                default:
                    break;
            }

        }

        private void SetNowTile()
        {
            GameObject.Transform.Position = nextTile.GameObject.Transform.Position;
            currentTile.IsUnitOccupied = false;
            directionCheck = true;
            currentTile = nextTile;
            
            if (tiles.Count > 0)
            {                
                nextTile = tiles.Pop();

                if (nextTile.IsUnitOccupied == true && tiles.Count > 0)
                {
                    CTile tmp = nextTile;
                    int number = tiles.Count;
                    for (int i = 0; i < number; i++)
                    {
                        tmp = tiles.Pop();
                    }
                    tiles.Clear();
                    List<CTile> tmpL = new List<CTile>(tileList);
                    tiles = Astar_Test.GetAstarWay(currentTile, tmp, tmpL);

                    if(tiles.Count > 0)
                    {
                        nextTile = tiles.Pop();
                        directionCheck = true;
                        runAstar = true;
                    }
                    else
                    {
                        EndAstar();
                    }
                }
                else if(nextTile.IsUnitOccupied == true)
                {
                    EndAstar();
                }                
            }
            else
            {
                EndAstar();
            }
        }

        private void EndAstar()
        {
            nextTile = null;
            runAstar = false;
            cMove.Velocity = new Vector2(0, 0);
            currentTile.IsUnitOccupied = true;
            unit.IsMoving = false;
        }
    }
}
