using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sticks
{
    class Field
    {
        public Cell[,] Cells;
        private int size = 6;
        public delegate void Method(int i, int j);
        public event Method PlayerMakeStroke;
        public delegate void MethodShowBorder(int i, int j, Position position);
        public event MethodShowBorder TakeBorder;
        public delegate void MethodOpenCell(int i, int j, CellOwner owner);
        public event MethodOpenCell CloseCell;

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                size = value;
            }
        }

        public Field(int size)
        {
            Size = size;
            Cells = new Cell[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }
            InitializeBorders();
        }

        public void InitializeBorders()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                {
                    if (j < Size - 1)
                        Cells[i, j].RightBorder = Cells[i, j + 1].LeftBorder;
                    if (i < Size - 1)
                        Cells[i, j].DownBorder = Cells[i + 1, j].UpBorder;
                }
        }

        public void CalculateBorders(int i, int j)
        {
            var limits = FinfPermissibleLimits(i, j);
            for (int k = limits.LeftX; k <= limits.RightX; k++)
                for (int n = limits.LeftY; n <= limits.RightY; n++)
                {
                    Cells[k, n].CalculateBordersCount();
                }
        }

        public PermissibleLimits FinfPermissibleLimits(int i, int j)
        {
            var leftY = j - 1;
            var leftX = i - 1;
            var rightY = j + 1;
            var rightX = i + 1;
            if (leftX < 0)
                leftX++;
            if (leftY < 0)
                leftY++;
            if (rightY == Size)
                rightY--;
            if (rightX == Size)
                rightX--;
            return new PermissibleLimits
            {
                LeftX = leftX,
                LeftY = leftY,
                RightX = rightX,
                RightY = rightY
            };
        }
        public void OpenBorder(int i, int j, Position position)
        {
            var continueStroke = false;
            switch (position)
            {
                case Position.Left:
                    Cells[i, j].LeftBorder.IsOpen = true;
                    TakeBorder(i, j, Position.Left);
                    break;
                case Position.Right:
                    Cells[i, j].RightBorder.IsOpen = true;
                    TakeBorder(i, j, Position.Right);
                    break;
                case Position.Up:
                    Cells[i, j].UpBorder.IsOpen = true;
                    TakeBorder(i, j, Position.Up);
                    break;
                case Position.Down:
                    Cells[i, j].DownBorder.IsOpen = true;
                    TakeBorder(i, j, Position.Down);
                    break;
            }
            CalculateBorders(i, j);
            if (Cells[i, j].BordersCount == 4)
            {
                CloseCell(i, j,CellOwner.Ally);
                Cells[i, j].IsOpen = true;
                continueStroke = true;
            }
            if (j > 0 && !Cells[i, j - 1].IsOpen && Cells[i, j - 1].BordersCount == 4)
            {
                CloseCell(i, j - 1, CellOwner.Ally);
                Cells[i, j - 1].IsOpen = true;
                continueStroke = true;
            }
            if (j < 5 && !Cells[i, j + 1].IsOpen && Cells[i, j + 1].BordersCount == 4)
            {
                CloseCell(i, j + 1, CellOwner.Ally);
                Cells[i, j + 1].IsOpen = true;
                continueStroke = true;
            }
            if (i > 0 && !Cells[i - 1, j].IsOpen && Cells[i - 1, j].BordersCount == 4)
            {
                CloseCell(i - 1, j, CellOwner.Ally);
                Cells[i - 1, j].IsOpen = true;
                continueStroke = true;
            }
            if (i < 5 && !Cells[i + 1, j].IsOpen && Cells[i + 1, j].BordersCount == 4)
            {
                CloseCell(i + 1, j, CellOwner.Ally);
                Cells[i + 1, j].IsOpen = true;
                continueStroke = true;
            }
            if (!continueStroke)
            {
                PlayerMakeStroke?.Invoke(i, j);
                return;
            }
        }
    }
}
