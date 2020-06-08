using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.Script.TheGame.Enum;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.AstarComponent
{
    public class CAstar : Component
    {
        private const byte DEFAULT_COST = 10;

        private byte tileSize = Singletons.LevelInformationSingleton.TileSize;

        private Cell startCell = null,
                     currentCell = null,
                     targetCell = null;

        private CUnit owner = null;

        private Cell[,] baseGrid = null;
        private GameObject[,] tileGrid = null;

        private List<Cell> openList = new List<Cell>(),
                           closedList = new List<Cell>(),
                           pathCloneStack = new List<Cell>();

        private Stack<Cell> pathStack = new Stack<Cell>();

        public CAstar(CUnit owner)
        {
            this.owner = owner;
        }

        public override void Update()
        {
            if (Singletons.AstarGlobalSingleton.BaseMapGrid != null && baseGrid == null)
            {
                baseGrid = Singletons.AstarGlobalSingleton.BaseMapGrid.ToAstarMatrix();
                tileGrid = Singletons.AstarGlobalSingleton.TileGrid;
                //baseGrid.Print2DArray();
                //Console.ReadKey();
            }

            if (owner.Target != null)
                if (pathStack.Count == 0)
                    GetPath();
                else
                {
                    //baseGrid.Print2DArray();
                    //Console.ReadKey();
                    FollowPath();
                }
        }

        public void ResetAstar()
        {
            if (currentCell != null)
            owner.GameObject.Transform.Position = new Vector2(currentCell.GridPos.X * tileSize, currentCell.GridPos.Y * tileSize);

            owner.GameObject.GetComponent<CMove>().Velocity = Vector2.Zero;
            //Singletons.AstarGlobalSingleton.GlobalAstarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].ECellType = ECellType.Default;

            currentCell = null;
            targetCell = null;
            startCell = null;

            pathStack.Clear();
            openList.Clear();
            closedList.Clear();

            if (pathCloneStack.Count > 0)
            {
                foreach (Cell cell in pathCloneStack)
                    Singletons.AstarGlobalSingleton.GlobalAstarGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y] = Singletons.AstarGlobalSingleton.BaseMapGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y];

                pathCloneStack.Clear();
            }

            baseGrid = Singletons.AstarGlobalSingleton.BaseMapGrid.ToAstarMatrix();

            baseGrid.Print2DArray();

            //runAstar = false;
            //cMove.Velocity = new Vector2(0, 0);
            //CurrentTile.IsUnitOccupied = true;
            owner.Target = null;
            owner.IsMoving = false;
        }

        private void GetPath()
        {
            startCell = baseGrid[(int)GameObject.Transform.Position.X / tileSize,
                                   (int)GameObject.Transform.Position.Y / tileSize];

            targetCell = baseGrid[(int)owner.Target.Transform.Position.X / tileSize,
                                  (int)owner.Target.Transform.Position.Y / tileSize];

            ModifyNeighbourCells(startCell, null);

            FindPath();
        }

        private void FollowPath()
        {
            //while (pathStack.Count != 0)
            //{
                Vector2 ownerGridPos = new Vector2(owner.GameObject.Transform.Position.X / tileSize,
                                                   owner.GameObject.Transform.Position.Y / tileSize);

            //if (ownerGridPos.ApproximatelyEqual(targetCell.GridPos, 0.05f))
            //    ResetAstar();
            if (ownerGridPos.ApproximatelyEqual(currentCell.GridPos, 0.05f))
            {
                if (pathStack.Count > 0)
                    currentCell = pathStack.Pop();

                tileGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].GetComponent<CTile>().IsUnitOccupied = true;
                Singletons.AstarGlobalSingleton.GlobalAstarGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y].ECellType = ECellType.Default;

                if (currentCell == targetCell)
                    ResetAstar();
            }
            else
                owner.MoveToLocation(new Vector2(currentCell.GridPos.X * tileSize,
                                                 currentCell.GridPos.Y * tileSize));
            //}
        }

        private void FindPath()
        {
            while (true)
            {
                if (openList.Count == 0)
                {
                    targetCell.BlackListed = true;
                    break;
                }
            
                GetNextCell();
            
                if (currentCell.GridPos == targetCell.GridPos)
                    break;
            
                SetNeighbourCells();
            }
            
            ReturnPath();

            pathCloneStack = pathStack.ToList();
            //while (currentCell != targetCell && openList.Count == 0)
            //    GetNextCell();
            //
            //
            //pathCloneStack = pathStack.ToArray();
            //
            //ReturnPath();
        }

        private void GetNextCell()
        {
            //currentCell = openList.First();
            openList.Sort();
            currentCell = openList.First();

            openList.Remove(currentCell);
            closedList.Add(currentCell);
            currentCell.ECellType = ECellType.Closed;

            SetNeighbourCells();
        }

        private void SetNeighbourCells()
        {
            // X
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X + 1, (int)currentCell.GridPos.Y], currentCell); } catch { }
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X - 1, (int)currentCell.GridPos.Y], currentCell); } catch { }

            // Y
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y + 1], currentCell); } catch { }
            try { ModifyNeighbourCells(baseGrid[(int)currentCell.GridPos.X, (int)currentCell.GridPos.Y - 1], currentCell); } catch { }
        }

        private void ModifyNeighbourCells(Cell cell, Cell parent)
        {
            if (baseGrid[(int)cell.GridPos.X, (int)cell.GridPos.Y].ECellType == ECellType.Default)
            {
                if (!closedList.Contains(cell) && !openList.Contains(cell))
                    openList.Add(cell);

                cell.Parent = parent;
                cell.FGH = new FGH(CalculateG(cell), CalculateH(cell.GridPos, targetCell.GridPos));
                cell.ECellType = ECellType.Open;
            }
            else if (openList.Contains(cell) && cell.Parent != null)
            {
                Cell storeParent = cell.Parent;
                cell.Parent = parent;

                FGH comparePrice = new FGH(CalculateG(cell), CalculateH(cell.GridPos, targetCell.GridPos));

                if (comparePrice.F < cell.FGH.F)
                    cell.FGH = comparePrice;
                else
                    cell.Parent = storeParent;
            }
        }

        private void ReturnPath()
        {
            while (true)
            {
                if (currentCell != null)
                {
                    pathStack.Push(currentCell);
                    if (currentCell.Parent == startCell)
                        break;

                    currentCell = currentCell.Parent;
                    currentCell.ECellType = ECellType.Path;
                }
                else
                    break;
            }
        }

        private uint CalculateG(Cell cell)
        {
            if (cell.Parent != null)
                return cell.Parent.FGH.G + DEFAULT_COST;

            return DEFAULT_COST;
        }

        private uint CalculateH(Vector2 pos, Vector2 target)
        {
            return (uint)Math.Abs(Vector2.Distance(pos, target) * 10);
        }

    }
}
