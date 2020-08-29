using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UltimateTicTacToe.AI
{
    public class MinMaxAB
    {
        public Game.Game currStatus { get; set; }
        public int depth { get; set; }

        public MinMaxAB(int depth)
        {
            if (depth > 0)
                this.depth = depth;
            else
                this.depth = 3;
        }

        private int EvalFunc(Game.Game gameStatus)
        {
            int evaulation = 0;

            evaulation = (100 * gameStatus.gameBoard.cellValue) + gameStatus.GetBoardState();

            return evaulation;
        }

        private Tuple<int,int> AlphaBetaSearch(Game.Game gameStatus, Tuple<int, int> alpha, Tuple<int, int> beta, int algDepth)
        {
            if (algDepth == 0 || gameStatus.gameBoard.resolved)
                return Tuple.Create(EvalFunc(gameStatus), -1);

            if(gameStatus.activePlayer == 1)
            {
                Tuple<int, int> ans = Tuple.Create(-1000, -1);
                for(int i = 0; i < gameStatus.currPossibleMoves.Count; i++)
                {
                    Game.Game newStatus = gameStatus.GenSuccesor(gameStatus.currPossibleMoves[i]);
                    Tuple<int, int> res = Tuple.Create(AlphaBetaSearch(newStatus, alpha, beta, algDepth - 1).Item1, i);
                    ans = res.Item1 > ans.Item1 ? res : ans;

                    alpha = alpha.Item1 > ans.Item1 ? alpha : ans;
                    if (alpha.Item1 >= beta.Item1)
                    {
                        break;
                    }
                }

                return alpha;
            }
            else
            {
                Tuple<int, int> ans = Tuple.Create(1000, -1);
                for (int i = 0; i < gameStatus.currPossibleMoves.Count; i++)
                {
                    Game.Game newStatus = gameStatus.GenSuccesor(gameStatus.currPossibleMoves[i]);
                    Tuple<int, int> res = Tuple.Create(AlphaBetaSearch(newStatus, alpha, beta, algDepth - 1).Item1, i);
                    ans = res.Item1 < ans.Item1 ? res : ans;

                    beta = beta.Item1 < ans.Item1 ? beta : ans;
                    if (alpha.Item1 >= beta.Item1)
                    {
                        break;
                    }
                }

                return beta;
            }
        }

        public void PlayRandomMove()
        {
            Random r = new Random();
            currStatus.PlayMove(currStatus.currPossibleMoves[r.Next(0, currStatus.currPossibleMoves.Count)]);
        }

        public int GetTurn()
        {
            Tuple<int, int> alpha = Tuple.Create(-1000, -1);
            Tuple<int, int> beta = Tuple.Create(1000, -1);

            if( currStatus.GetMoveCount() < 5)
            {
                Random r = new Random();
                return r.Next(0, currStatus.currPossibleMoves.Count);
            }

            int moveIndex = AlphaBetaSearch(currStatus, alpha, beta, depth).Item2;

            if(moveIndex > -1)
            {
                return moveIndex;
            }
            else
            {
                Random r = new Random();
                return r.Next(0, currStatus.currPossibleMoves.Count);
            }
        }
    }
}
