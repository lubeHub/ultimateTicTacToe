using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTicTacToe.Game;

namespace UltimateTicTacToe.AI
{
    public class RandomAI
    {
        Random randValue = null;
        public Game.Game currStatus { get; set; }

        public RandomAI()
        {
            randValue = new Random();
        }

        public void GetTurn()
        {
            currStatus.PlayMove(currStatus.currPossibleMoves[randValue.Next(0, currStatus.currPossibleMoves.Count)]);
        }
    }
}
