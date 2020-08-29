using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTicTacToe.AI;

namespace UltimateTicTacToe.Game
{
    public class Game
    {
        public Cell gameBoard { get; set; }
        public int activePlayer { get; set; }
        public int humanPlayer { get; set; }


        public List<Cell> currPossibleMoves { get; set; }
        public MinMaxAB minmax;

        public Game(int player,int depth)
        {
            gameBoard = new Cell();
            gameBoard.index = 0;
            currPossibleMoves = new List<Cell>();
            gameBoard.CreateBoard();
            for (int i = 0; i < 9; i++)
            {
                gameBoard.childCells[i].CreateBoard();
                for (int j = 0; j < 9; j++)
                {
                    currPossibleMoves.Add(gameBoard.childCells[i].childCells[j]);
                }
            }

            activePlayer = 1;
            humanPlayer = player;
            minmax = new MinMaxAB(depth);
            minmax.currStatus = this;
        }
        public bool IsPlayerTurn()
        {
            return activePlayer == humanPlayer;
        }

        public int playerWinCount { get; set; }
       public int computerWinCount { get; set; }

        public Game(Game originalGame)
        {
            gameBoard = originalGame.gameBoard.Clone();
            playerWinCount = 0;
            computerWinCount = 0;
            currPossibleMoves = new List<Cell>();
            if (originalGame.currPossibleMoves.Count > 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (originalGame.currPossibleMoves.Contains(originalGame.gameBoard.childCells[i].childCells[j]))
                        {
                            currPossibleMoves.Add(gameBoard.childCells[i].childCells[j]);
                        }
                    }
                }
            }

            activePlayer = originalGame.activePlayer;
        }

        public Game Clone()
        {
            Game clone = new Game(this);
            return clone;
        }

        public void Reset()
        {
            gameBoard.Reset();
            currPossibleMoves = new List<Cell>();

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    currPossibleMoves.Add(gameBoard.childCells[i].childCells[j]);
                }
            }
        }

        public void endTurn()
        {
            for(int i = 0; i < 9; i++)
            {
                gameBoard.childCells[i].ResolveChildCell();
            }
            gameBoard.ResolveChildCell();

            activePlayer = activePlayer > 0 ? -1 : 1;

        }

        public void PlayMove(Cell move)
        {
            if( MovePossible(move))
            {
                move.SetAs(activePlayer);

                for (int i = 0; i < 9; i++)
                {
                    gameBoard.childCells[i].ResolveChildCell();
                }
                gameBoard.ResolveChildCell();

                activePlayer = -activePlayer;
                currPossibleMoves = gameBoard.GetPossibleMoves(move.index);
            }
        }

        public int GetBoardState()
        {
            int boardState = 0;

            for(int i = 0; i < 9; i++)
            {
                boardState += gameBoard.childCells[i].cellValue;
            }

            return boardState;
        }

        public bool MovePossible(Cell move)
        {
            if (currPossibleMoves.Contains(move))
                return true;

            return false;
        }

        public Game GenSuccesor(Cell newMove)
        {
            Game succesor = this.Clone();
            int moveIndex = currPossibleMoves.IndexOf(newMove);

            succesor.PlayMove(succesor.currPossibleMoves[moveIndex]);

            return succesor;
        }

        public int GetMoveCount()
        {
            int moveCount = 0;
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (gameBoard.childCells[i].childCells[j].cellValue == -activePlayer)
                        moveCount++;
                }
            }

            return moveCount;
        }
    }
}
