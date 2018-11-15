using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sticks
{
    class CellControlButton : Button
    {
        Field field;
        public int i, j;
        public BorderControlButton LeftBorder;
        public BorderControlButton RightBorder;
        public BorderControlButton UpBorder;
        public BorderControlButton DownBorder;

        public CellControlButton(Field field,int i,int j)
        {
            this.field = field;
            this.i = i;
            this.j = j;
        }
    }
}
