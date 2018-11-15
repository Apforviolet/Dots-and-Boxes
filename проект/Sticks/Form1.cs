using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sticks
{
    public partial class Form1 : Form
    {
        Field mainfield;
        ComputerPlayer computerPlayer;
        GroupBox box;
        CellControlButton[,] cellArray;
        public static bool isYourTurn = true;

        public Form1()
        {
            InitializeComponent();
            Resize += ResizeF;
            NewGame();
        }

        void NewGame()
        {
            
            cellArray = new CellControlButton[6, 6];
            computerPlayer = new ComputerPlayer();
            mainfield = new Field(6);
            AddButtons();
            computerPlayer.field = mainfield;
            mainfield.TakeBorder += ShowPainted;
            computerPlayer.EnemyStroke += ShowPainted;
            mainfield.PlayerMakeStroke += computerPlayer.EnemyMakeStroke;
            computerPlayer.CloseCell += PaintCell;
            mainfield.CloseCell += PaintCell;
            mainfield.OpenBorder(2, 4, Position.Down);
            mainfield.OpenBorder(1, 5, Position.Down);
            mainfield.OpenBorder(1, 4, Position.Down);
            ChangeForm();
        }

        void PaintCell(int i, int j,CellOwner owner)
        {
            if (owner == CellOwner.Enemy)
            {
                cellArray[i, j].BackgroundImage = Properties.Resources.EnemyCellOpen;
                IncreaseEnemyScore(CellOwner.Enemy);
            }
            else
            {
                cellArray[i, j].BackgroundImage = Properties.Resources.AllyCellOpen;
                IncreaseEnemyScore(CellOwner.Ally);
            }
        }
        void IncreaseEnemyScore(CellOwner owner)
        {
            if (owner == CellOwner.Enemy)
            {
                var number = Convert.ToInt32(label4.Text);
                number++;
                label4.Text = number.ToString();
            }
            else
            {
                var number = Convert.ToInt32(label3.Text);
                number++;
                label3.Text = number.ToString();
            }
        }
        void ShowPainted(int i, int j, Position position)
        {
            Thread.Sleep(120);
            if (position == Position.Left)
            {
                cellArray[i, j].LeftBorder.BackgroundImage = Properties.Resources.ActiveVerticalBorder;
                cellArray[i, j].LeftBorder.Enabled = false;
                if (j > 0)
                {
                    cellArray[i, j - 1].RightBorder.BackgroundImage = Properties.Resources.ActiveVerticalBorder;
                    cellArray[i, j - 1].RightBorder.Enabled = false;
                }
            }
            if (position == Position.Right)
            {
                cellArray[i, j].RightBorder.BackgroundImage = Properties.Resources.ActiveVerticalBorder;
                cellArray[i, j].RightBorder.Enabled = false;
                if (j < 5)
                {
                    cellArray[i, j + 1].LeftBorder.BackgroundImage = Properties.Resources.ActiveVerticalBorder;
                    cellArray[i, j + 1].LeftBorder.Enabled = false;
                }
            }
            if (position == Position.Up)
            {
                cellArray[i, j].UpBorder.BackgroundImage = Properties.Resources.ActiveHorizontalBorder;
                cellArray[i, j].UpBorder.Enabled = false;
                if (i > 0)
                {
                    cellArray[i - 1, j].DownBorder.BackgroundImage = Properties.Resources.ActiveHorizontalBorder;
                    cellArray[i - 1, j].DownBorder.Enabled = false;
                }
            }
            if (position == Position.Down)
            {
                cellArray[i, j].DownBorder.BackgroundImage = Properties.Resources.ActiveHorizontalBorder;
                cellArray[i, j].DownBorder.Enabled = false;
                if (i < 5)
                {
                    cellArray[i + 1, j].UpBorder.BackgroundImage = Properties.Resources.ActiveHorizontalBorder;
                    cellArray[i + 1, j].UpBorder.Enabled = false;
                }
            }
        }

        void AddButtons()
        {
            box = new GroupBox();
            box.Location = new Point(50, 250);
            box.Size = new Size(100 * mainfield.Size, 100 * mainfield.Size);
            box.Parent = this;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    cellArray[i, j] = new CellControlButton(mainfield, i, j);
                    cellArray[i, j].LeftBorder = new BorderControlButton(i, j, Position.Left);
                    cellArray[i, j].LeftBorder.PlayerStroke += mainfield.OpenBorder;
                    cellArray[i, j].RightBorder = new BorderControlButton(i, j, Position.Right);
                    cellArray[i, j].RightBorder.PlayerStroke += mainfield.OpenBorder;
                    cellArray[i, j].UpBorder = new BorderControlButton(i, j, Position.Up);
                    cellArray[i, j].UpBorder.PlayerStroke += mainfield.OpenBorder;
                    cellArray[i, j].DownBorder = new BorderControlButton(i, j, Position.Down);
                    cellArray[i, j].DownBorder.PlayerStroke += mainfield.OpenBorder;
                    CellSettings(cellArray[i, j], i, j);
                    BorderSettings(cellArray[i, j].LeftBorder, i, j, Position.Left);
                    BorderSettings(cellArray[i, j].RightBorder, i, j, Position.Right);
                    BorderSettings(cellArray[i, j].UpBorder, i, j, Position.Up);
                    BorderSettings(cellArray[i, j].DownBorder, i, j, Position.Down);
                }
            }
        }
        void CellSettings(CellControlButton cellButton, int i, int j)
        {
            cellButton.Width = 15; cellButton.Height = 15;
            cellButton.Location = new Point(j * 37 + 10, i * 37 + 10);
            cellButton.FlatStyle = FlatStyle.Flat;
            cellButton.BackgroundImage = Properties.Resources.cell;
            cellButton.ForeColor = SystemColors.Control;
            cellButton.Parent = box;
        }
        void BorderSettings(BorderControlButton border, int i, int j, Position position)
        {
            if (position == Position.Left)
            {
                border.Width = 10; border.Height = 35;
                border.Location = new Point(j * 35, i * 35);
                border.BackgroundImage = Properties.Resources.VerticalBorder;
            }
            if (position == Position.Right)
            {
                border.Width = 10; border.Height = 35;
                border.Location = new Point(j * 35 + 35, i * 35);
                border.BackgroundImage = Properties.Resources.VerticalBorder;
            }
            if (position == Position.Up)
            {
                border.Width = 35; border.Height = 10;
                border.Location = new Point(j * 35, i * 35);
                border.BackgroundImage = Properties.Resources.HorizontalBorder;
            }
            if (position == Position.Down)
            {
                border.Width = 35; border.Height = 10;
                border.Location = new Point(j * 35, i * 35 + 35);
                border.BackgroundImage = Properties.Resources.HorizontalBorder;
            }
            border.FlatStyle = FlatStyle.Flat;
            border.ForeColor = SystemColors.Control;
            border.Parent = box;
        }

        void ResizeF(object sender, EventArgs e)
        {
            groupBox1.Location = new Point((Width - groupBox1.Size.Width) / 4, 10);
            box.Location = new Point((Width - box.Size.Width) / 2, 100);
        }

        void ChangeForm()
        {
            Width = box.Width + 200;
            Height = box.Height + 200;
            if (Width <= groupBox1.Width) Width = groupBox1.Width + 40;
        }

        private void NewGame(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
            NewGame();
        }
    }
}
