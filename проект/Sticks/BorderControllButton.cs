using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sticks
{
    class BorderControllButton : Button
    {
        public int i, j;
        public string direction;

        public BorderControllButton(int i, int j, string direction)
        {
            this.i = i;
            this.j = j;
            this.direction = direction;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (direction == "LeftBorder")
                {
                    Field.Cells[i, j].LeftBorder.IsOpen = true;
                    Field.Cells[i, j].BordersCount++;
                    BackgroundImage = Properties.Resources.ActiveVerticalBorder;
                }
                if (direction == "RightBorder")
                {
                    Field.Cells[i, j].RightBorder.IsOpen = true;
                    Field.Cells[i, j].BordersCount++;
                    BackgroundImage = Properties.Resources.ActiveVerticalBorder;
                }
                if (direction == "DownBorder")
                {
                    Field.Cells[i, j].DownBorder.IsOpen = true;
                    Field.Cells[i, j].BordersCount++;
                    BackgroundImage = Properties.Resources.ActiveHorizontalBorder;
                }
                if (direction == "UpBorder")
                {
                    Field.Cells[i, j].UpBorder.IsOpen = true;
                    Field.Cells[i, j].BordersCount++;
                    BackgroundImage = Properties.Resources.ActiveHorizontalBorder;
                }
            }
        }
    }
}
