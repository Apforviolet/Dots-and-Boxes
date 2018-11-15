using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sticks
{
    class Cell
    {
        public bool IsOpen;
        public int BordersCount;
        public Border LeftBorder;
        public Border RightBorder;
        public Border UpBorder;
        public Border DownBorder;

        public Cell()
        {
            LeftBorder = new Border();
            RightBorder = new Border();
            UpBorder = new Border();
            DownBorder = new Border();
        }
        public void CalculateBordersCount()
        {
            BordersCount = 0;
            if (UpBorder.IsOpen == true)
                BordersCount++;
            if (RightBorder.IsOpen == true)
                BordersCount++;
            if (DownBorder.IsOpen == true)
                BordersCount++;
            if (LeftBorder.IsOpen == true)
                BordersCount++;
        }
    }
}
