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
    public class C_FollowPath : Component
    {
        Stack<CTile> tiles = new Stack<CTile>();
        public CTile currentTile { get; set; }
        CTile nextTile;
        CUnit unit;
        CMove cMove;
        EFacingDirection direction;
        bool directionCheck = false;

        bool runAstar = false;

        public C_FollowPath(CUnit unit)
        {
            this.unit = unit;
        }

        public override void Awake()
        {
            base.Awake();
            cMove = GameObject.GetComponent<CMove>();
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

        public void GetAstar(CTile goal)
        {
            tiles.Clear();
            tiles = _Astar_Test.Instance.GetAstarWay(currentTile, goal);
            
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

                float xPos = Math.Abs(GameObject.Transform.Position.X - nextTile.GameObject.Transform.Position.X);
                float yPos = Math.Abs(GameObject.Transform.Position.Y - nextTile.GameObject.Transform.Position.Y);

                Console.WriteLine(xPos);
                Console.WriteLine(yPos);

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
            }
            else
            {
                runAstar = false;
                cMove.Velocity = new Vector2(0, 0);
                currentTile.IsUnitOccupied = true;
            }
        }

        //float offSet = 1;
        //float Yposition = nextTile.GameObject.Transform.Position.Y;
        //float Xposition = nextTile.GameObject.Transform.Position.X;

        //if (GameObject.Transform.Position.Y + offSet > Yposition)
        //{
        //    cMove.Velocity = new Vector2(0, -1);
        //}

        //if (GameObject.Transform.Position.Y < Yposition + offSet)
        //{
        //    cMove.Velocity = new Vector2(0, 1);
        //}

        //if (GameObject.Transform.Position.X + offSet > Xposition)
        //{
        //    cMove.Velocity = new Vector2(-1, 0);
        //}

        //if (GameObject.Transform.Position.X < Xposition + offSet)
        //{
        //    cMove.Velocity = new Vector2(1, 0);
        //}

        //if (Vector2.Distance(GameObject.Transform.Position, nextTile.GameObject.Transform.Position) < 20)
        //{
        //    SetNowTile();
        //}






        //if (directionCheck)
        //{
        //    if (nextTile == null)
        //    {
        //        nextTile = tiles.Pop();
        //    }

        //    float xPos = Math.Abs(GameObject.Transform.Position.X - nextTile.GameObject.Transform.Position.X);
        //    float yPos = Math.Abs(GameObject.Transform.Position.Y - nextTile.GameObject.Transform.Position.Y);

        //    Console.WriteLine(xPos);
        //    Console.WriteLine(yPos);

        //    if (GameObject.Transform.Position.X > nextTile.GameObject.Transform.Position.X && xPos> yPos)
        //    {
        //        direction = EFacingDirection.Left;
        //        directionCheck = false;
        //    }
        //    else if (GameObject.Transform.Position.X < nextTile.GameObject.Transform.Position.X && xPos > yPos)
        //    {
        //        direction = EFacingDirection.Rigth;
        //        directionCheck = false;
        //    }
        //    else if (GameObject.Transform.Position.Y < nextTile.GameObject.Transform.Position.Y && xPos < yPos)
        //    {
        //        direction = EFacingDirection.Down;
        //        directionCheck = false;
        //    }
        //    else if (GameObject.Transform.Position.Y > nextTile.GameObject.Transform.Position.Y && xPos < yPos)
        //    {
        //        direction = EFacingDirection.Up;
        //        directionCheck = false;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Error in C_FollowPath");
        //    }
        //}


        //switch (direction)
        //{
        //    case EFacingDirection.Up:
        //        if (GameObject.Transform.Position.Y < nextTile.GameObject.Transform.Position.Y)
        //        {
        //            SetNowTile();
        //        }
        //        else
        //        {
        //            cMove.Velocity = new Vector2(0, -1);
        //        }
        //        break;
        //    case EFacingDirection.Down:
        //        if (GameObject.Transform.Position.Y > nextTile.GameObject.Transform.Position.Y)
        //        {
        //            SetNowTile();
        //        }
        //        else
        //        {
        //            cMove.Velocity = new Vector2(0, 1);
        //        }
        //        break;
        //    case EFacingDirection.Left:
        //        if (GameObject.Transform.Position.X < nextTile.GameObject.Transform.Position.X)
        //        {
        //            SetNowTile();
        //        }
        //        else
        //        {
        //            cMove.Velocity = new Vector2(-1, 0);
        //        }
        //        break;
        //    case EFacingDirection.Rigth:
        //        if (GameObject.Transform.Position.X > nextTile.GameObject.Transform.Position.X)
        //        {
        //            SetNowTile();
        //        }
        //        else
        //        {
        //            cMove.Velocity = new Vector2(1, 0);
        //        }
        //        break;
        //    default:
        //        break;
        //}
    }
}
