using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateTicTacToe.Observer;
using UltimateTicTacToe.Game;

namespace UltimateTicTacToe.View
{
    public partial class CellView : PictureBox, Subscriber
    {
        public Cell model { get; set; }

        public CellView()
        {
            InitializeComponent();
        }

        void Subscriber.Update()
        {
            Image = new Bitmap(Width, Height);

            Graphics g = Graphics.FromImage(Image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if( model.cellValue == 1 )
            {
                Pen pen = new Pen(Color.FromArgb(43, 139, 215));
                pen.Width = 8;
                g.DrawLine(pen, 5, 5, Width - 5, Height - 5);
                g.DrawLine(pen, 5, Height - 5, Width - 5, 5);
            }
            else if( model.cellValue == -1 )
            {
                Pen pen = new Pen(Color.FromArgb(212, 60, 50));
                pen.Width = 8;
                g.DrawEllipse(pen, 5, 5, Width - 10, Height - 10);
            }
            g.Dispose();
            Refresh();
        }

        public void UpdateAvailable()
        {
            BackColor = Color.FromArgb(211,207,51);
            Refresh();
        }

        public void setBackground(Color color)
        {
            BackColor = color;
            Refresh();
        }
        public void DrawCross()
        {
            Image = new Bitmap(Width, Height);

            Graphics g = Graphics.FromImage(Image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
          
                Pen pen = new Pen(Color.FromArgb(43,139,215));
                pen.Width = 8;
                g.DrawLine(pen, 5, 5, Width - 5, Height - 5);
                g.DrawLine(pen, 5, Height - 5, Width - 5, 5);
            g.Dispose();
            Refresh();

        }
        public void DrawCircle()
        {
            Image = new Bitmap(Width, Height);

            Graphics g = Graphics.FromImage(Image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen pen = new Pen(Color.FromArgb(212,60,50));
            pen.Width = 8;
            g.DrawEllipse(pen, 5, 5, Width - 10, Height - 10);
            g.Dispose();
            Refresh();
        }
    }
}
