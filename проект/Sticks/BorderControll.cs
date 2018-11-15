using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sticks
{
    class BorderControlButton : Button
    {
        public int i;
        public int j;
        public Position position;
        public delegate void MethodCont(int i, int j, Position position);
        public event MethodCont PlayerStroke;
        public BorderControlButton(int i, int j, Position position)
        {
            this.i = i;
            this.j = j;
            this.position = position;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                switch (position)
                {
                    case Position.Left:
                        BackgroundImage = Properties.Resources.ActiveVerticalBorder;
                        PlayerStroke?.Invoke(i, j, Position.Left);
                        break;
                    case Position.Right:
                        BackgroundImage = Properties.Resources.ActiveVerticalBorder;
                        PlayerStroke?.Invoke(i, j, Position.Right);
                        break;
                    case Position.Up:
                        BackgroundImage = Properties.Resources.ActiveHorizontalBorder;
                        PlayerStroke?.Invoke(i, j, Position.Up);
                        break;
                    case Position.Down:
                        BackgroundImage = Properties.Resources.ActiveHorizontalBorder;
                        PlayerStroke?.Invoke(i, j, Position.Down);
                        break;
                }
            }
        }
    }
}
