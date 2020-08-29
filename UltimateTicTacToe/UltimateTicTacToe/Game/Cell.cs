using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTicTacToe.Observer;

namespace UltimateTicTacToe.Game
{
    public class Cell : Subject
    {
        public bool resolved { get; set; }
        public int cellValue { get; set; }

        public List<Cell> childCells { get; }

        public int index { get; set; }

        public Cell()
        {
            resolved = false;
            cellValue = 0;
            childCells = new List<Cell>();
        }

        public Cell(Cell originalCell)
        {
            this.resolved = originalCell.resolved;
            cellValue = originalCell.cellValue;
            index = originalCell.index;
            childCells = new List<Cell>();

            if (originalCell.childCells.Count > 0)
            {
                for (int i = 0; i < originalCell.childCells.Count; i++)
                {
                    childCells.Add(originalCell.childCells[i].Clone());
                }
            }
        }

        public List<Cell> GetChildCells()
        {
            return childCells;
        }

        public void CreateBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                Cell newCell = new Cell();
                newCell.index = i;
                childCells.Add(newCell);
            }
        }

        public void Reset()
        {
            if(childCells.Count > 0)
                for(int i = 0; i < childCells.Count; i++)
                    childCells[i].Reset();

            cellValue = 0;
            resolved = false;
            UpdateAll();
        }

        public bool ResolveChildCell()
        {
            if (resolved)
                return resolved;
            if(childCells.Count == 0)
            {
                return resolved;
            }
            for(int i = 0; i < 9; i += 3)
            {
                if( childCells[i].cellValue == childCells[i + 1].cellValue && childCells[i].cellValue == childCells[i + 2].cellValue)
                {
                    if (childCells[i].cellValue != 0)
                    {
                        resolved = true;
                        if (childCells.Count != 0)
                        {
                            for (int j = 0; j < 9; j++)
                                childCells[j].resolved = true;
                        }
                        cellValue = childCells[i].cellValue;

                        return resolved;
                    }
                }
            }

            for(int i = 0; i < 3; i++)
            {
                if(childCells[i].cellValue == childCells[i + 3].cellValue && childCells[i].cellValue == childCells[i + 6].cellValue)
                {
                    if (childCells[i].cellValue != 0)
                    {
                        resolved = true;
                        if (childCells.Count != 0)
                        {
                            for (int j = 0; j < 9; j++)
                                childCells[j].resolved = true;
                        }
                        cellValue = childCells[i].cellValue;

                        return resolved;
                    }
                }
            }
            
            if(childCells[0].cellValue == childCells[4].cellValue && childCells[0].cellValue == childCells[8].cellValue)
            {
                if (childCells[0].cellValue != 0)
                {
                    resolved = true;
                    if( childCells.Count != 0)
                    {
                        for (int i = 0; i < 9; i++)
                            childCells[i].resolved = true;
                    }
                    cellValue = childCells[4].cellValue;

                    return resolved;
                }
            }
            else if (childCells[2].cellValue == childCells[4].cellValue && childCells[2].cellValue == childCells[6].cellValue)
            {
                if (childCells[2].cellValue != 0)
                {
                    resolved = true;
                    if (childCells.Count != 0)
                    {
                        for (int i = 0; i < 9; i++)
                            childCells[i].resolved = true;
                    }
                    cellValue = childCells[4].cellValue;

                    return resolved;
                }
            }

            return resolved;
        }

        public void SetAs(int activePlayer)
        {
            cellValue = activePlayer;
            resolved = true;
            UpdateAll();
        }

        public List<Cell> GetPossibleMoves(int lastClickIndex)
        {
            List<Cell> possibleMoves = new List<Cell>();

            if( !childCells[lastClickIndex].resolved )
            {
                for(int i = 0; i < 9; i++)
                {
                    if( !childCells[lastClickIndex].childCells[i].resolved )
                    {
                        possibleMoves.Add(childCells[lastClickIndex].childCells[i]);
                    }
                }
            }
            else
            {
                for( int i = 0; i < 9; i++ )
                {
                    for( int j = 0; j < 9; j++ )
                    {
                        if( !childCells[i].childCells[j].resolved )
                        {
                            possibleMoves.Add(childCells[i].childCells[j]);
                        }
                    }
                }
            }

            foreach(Cell cell in possibleMoves)
            {
                cell.UpdateAllMoves();
            }

            return possibleMoves;
        }

        public Cell Clone()
        {
            Cell clone = new Cell(this);
            return clone;
        }



    }
}
