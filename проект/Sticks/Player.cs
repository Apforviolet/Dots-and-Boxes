using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sticks
{
    class Player
    {
        public Field field;

        public void Stroke(int i,int j, Position position)
        {
            if (position == Position.Left)
            {
                field.Cells[i, j].LeftBorder.IsOpen = true;
            }
            if (position == Position.Right)
            {
                field.Cells[i, j].RightBorder.IsOpen = true;
            }
            if (position == Position.Down)
            {
                field.Cells[i, j].DownBorder.IsOpen = true;
            }
            if (position == Position.Up)
            {
                field.Cells[i, j].UpBorder.IsOpen = true;
            }
        }

    }
}
