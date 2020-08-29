using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateTicTacToe.View
{
    class RoundPictureBox : PictureBox
    {
        public RoundPictureBox()
        {
            this.BackColor = Color.DarkGray;
          
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, this.Width, this.Height);
                Region = new Region(gp);
            }
        }
    }
}

