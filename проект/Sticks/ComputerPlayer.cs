using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sticks
{
    class ComputerPlayer
    {
        public delegate void MethodCont(int i, int j, Position position);
        public event MethodCont EnemyStroke;
        public delegate void MethodOpenCell(int i, int j, CellOwner owner);
        public event MethodOpenCell CloseCell;
        public Field field;

        public void EnemyMakeStroke(int i, int j)
        {
            if (field.Cells[i, j].BordersCount == 3)
            {
                FindCellWithThreeBorders();
                return;
            }
            if (field.Cells[i, j].BordersCount <= 1)
            {
                TryPutTrueSide(i, j);
                return;
            }
            if (field.Cells[i, j].BordersCount == 2)
            {
                FindCellWithThreeBorders();
            }

        }

        public void TryPutTrueSide(int l, int m)
        {
            var limits = field.FinfPermissibleLimits(l, m);
            for (int k = limits.LeftX; k <= limits.RightX; k++)
            {
                for (int n = limits.LeftY; n <= limits.RightY; n++)
                {
                    if (field.Cells[k, n].BordersCount == 3)
                        TryPutLastSide(k, n);
                }
            }

            if (field.Cells[l, limits.LeftY].BordersCount <= 1 && field.Cells[l, m].LeftBorder.IsOpen != true)
            {
                field.Cells[l, m].LeftBorder.IsOpen = true;
                EnemyStroke?.Invoke(l, m, Position.Left);
                field.Cells[l, m].IsOpen = true;
            }
            else if (field.Cells[limits.LeftX, m].BordersCount <= 1 && field.Cells[l, m].UpBorder.IsOpen != true)
            {
                field.Cells[l, m].UpBorder.IsOpen = true;
                EnemyStroke?.Invoke(l, m, Position.Up);
                field.Cells[l, m].IsOpen = true;
            }
            else if (field.Cells[l, limits.RightY].BordersCount <= 1 && field.Cells[l, m].RightBorder.IsOpen != true)
            {
                field.Cells[l, m].RightBorder.IsOpen = true;
                EnemyStroke?.Invoke(l, m, Position.Right);
                field.Cells[l, m].IsOpen = true;
            }
            else if (field.Cells[limits.RightX, m].BordersCount <= 1 && field.Cells[l, m].DownBorder.IsOpen != true)
            {
                field.Cells[l, m].DownBorder.IsOpen = true;
                EnemyStroke?.Invoke(l, m, Position.Down);
                field.Cells[l, m].IsOpen = true;
            }
            else
            {
                TryFindRightCell(l, m);
            }
            CalculateBorders(l, m);
        }

        public void TryFindRightCell(int rowIndex, int ColumnIndex)
        {
            if (ColumnIndex < 5)
                ColumnIndex++;
            for (int i = 0; i < field.Size; i++)
            {
                for (int j = ColumnIndex; j < field.Size; j++)
                {
                    if (field.Cells[i, j].BordersCount <= 1)
                    {
                        TryPutTrueSide(i, j);
                        return;
                    }
                }
            }
            PutInAnyPlace();
        }
        public void FindCellWithThreeBorders()
        {
            var isFind = false;
            for (int i = 0; i < field.Size; i++)
            {
                for (int j = 0; j < field.Size; j++)
                {
                    if (field.Cells[i, j].BordersCount == 3)
                    {
                        TryPutLastSide(i, j);
                        isFind = true;
                    }
                }
            }
            if (!isFind)
                TryFindRightCell(0,0);
        }

        void PutInAnyPlace()
        {
            for (int i = 0; i < field.Size; i++)
            {
                for (int j = 0; j < field.Size; j++)
                {
                    if (field.Cells[i, j].BordersCount == 2)
                    {
                        if (field.Cells[i, j].DownBorder.IsOpen == false)
                        {
                            field.Cells[i, j].DownBorder.IsOpen = true;
                            EnemyStroke?.Invoke(i, j, Position.Down);
                            CalculateBorders(i, j);
                            return;
                        }
                        if (field.Cells[i, j].UpBorder.IsOpen == false)
                        {
                            field.Cells[i, j].UpBorder.IsOpen = true;
                            EnemyStroke?.Invoke(i, j, Position.Up);
                            CalculateBorders(i, j);
                            return;
                        }
                        if (field.Cells[i, j].LeftBorder.IsOpen == false)
                        {
                            field.Cells[i, j].LeftBorder.IsOpen = true;
                            EnemyStroke?.Invoke(i, j, Position.Left);
                            CalculateBorders(i, j);
                            return;
                        }
                        if (field.Cells[i, j].RightBorder.IsOpen == false)
                        {
                            field.Cells[i, j].RightBorder.IsOpen = true;
                            EnemyStroke?.Invoke(i, j, Position.Right);
                            CalculateBorders(i, j);
                            return;
                        }
                    }
                }
            }
        }

        public void TryPutLastSide(int i, int j)
        {
            if (field.Cells[i, j].DownBorder.IsOpen == false)
            {
                field.Cells[i, j].DownBorder.IsOpen = true;
                EnemyStroke?.Invoke(i, j, Position.Down);
            }
            else if (field.Cells[i, j].UpBorder.IsOpen == false)
            {
                field.Cells[i, j].UpBorder.IsOpen = true;
                EnemyStroke?.Invoke(i, j, Position.Up);
            }
            else if (field.Cells[i, j].RightBorder.IsOpen == false)
            {
                field.Cells[i, j].RightBorder.IsOpen = true;
                EnemyStroke?.Invoke(i, j, Position.Right);
            }
            else if (field.Cells[i, j].LeftBorder.IsOpen == false)
            {
                field.Cells[i, j].LeftBorder.IsOpen = true;
                EnemyStroke?.Invoke(i, j, Position.Left);
            }
            field.Cells[i, j].IsOpen = true;
            CloseCell(i, j,CellOwner.Enemy);
            CalculateBorders(i, j);
            FindCellWithThreeBorders();
        }
        public void CalculateBorders(int i, int j)
        {
            var limits = field.FinfPermissibleLimits(i, j);
            for (int k = limits.LeftX; k <= limits.RightX; k++)
                for (int n = limits.LeftY; n <= limits.RightY; n++)
                {
                    field.Cells[k, n].CalculateBordersCount();
                }
        }
    }
}
