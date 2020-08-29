using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Observer
{
    public interface Subscriber
    {
        void Update();
        void UpdateAvailable();
        void setBackground(Color color);
    }
}
