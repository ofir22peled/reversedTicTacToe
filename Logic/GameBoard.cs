using System;
using System.Linq;
using System.Collections.Generic;

namespace Logic
{
    public enum eSymbol
    {
        Empty,
        X,
        O
    }

    public class GameBoard
    {
        private readonly eSymbol[,] r_Board;
        private readonly int r_Size;
        private int m_Length;
        private int m_NumOccupiedSquars;
        private Square m_SquareEntered;

        public GameBoard(int i_BoardLength)
        {
            m_Length = i_BoardLength;
            r_Size = m_Length * m_Length;
            r_Board = new eSymbol[i_BoardLength, i_BoardLength];
            m_SquareEntered = new Square();
        }

        public eSymbol[,] Board
        {
            get
            {
                return r_Board;
            }
        }

        public int Length
        {
            get
            {
                return m_Length;
            }
            set
            {
                m_Length = value;
            }

        }

        public int Size
        {
            get
            {
                return r_Size;
            }
        }

        public int NumOccupiedSquars
        {
            get
            {
                return m_NumOccupiedSquars;
            }
            set
            {
                m_NumOccupiedSquars = value;
            }

        }

        public bool IsCellEmpty(Square i_SquareToCheck)
        {
            return r_Board[i_SquareToCheck.Row - 1, i_SquareToCheck.Col - 1] == eSymbol.Empty;
        }

        public bool IsBoardFull()
        {
            bool isBoardFull;
            
            if (m_NumOccupiedSquars == r_Size)
            {
                isBoardFull = true;
            }
            else
            {
                isBoardFull = false;
            }

            return isBoardFull;
        }

        public bool IsCellInRange(Square i_SquareToCheck)
        {
            return IsRowInRange(i_SquareToCheck.Row) && IsColInRange(i_SquareToCheck.Col);
        }

        public bool IsColInRange(int i_Col)
        {
            return i_Col > 0 && i_Col <= m_Length;
        }

        public bool IsRowInRange(int i_Row)
        {
            return i_Row > 0 && i_Row <= m_Length;
        }

        public bool CheckColSeries(eSymbol i_Symbol, Square i_SquareEntered)
        {
            bool isColSerie = true;

            foreach (int col in Enumerable.Range(0, m_Length))
            {
                if (r_Board[i_SquareEntered.Row - 1, col] != i_Symbol)
                {
                    isColSerie = false;
                    break;
                }
            }

            return isColSerie;
        }

        public bool CheckRowSeries(eSymbol i_Symbol, Square i_SquareEntered)
        {
            bool isRowSerie = true;

            foreach (int row in Enumerable.Range(0, m_Length))
            {
                if (r_Board[row, i_SquareEntered.Col - 1] != i_Symbol)
                {
                    isRowSerie = false;
                    break;
                }
            }

            return isRowSerie;
        }

        public bool CheckLeftDiagonalSeries(eSymbol i_Symbol, Square i_SquareEntered)
        {
            bool isDiagonalSerie = true;

            if (i_SquareEntered.Row == i_SquareEntered.Col)
            {
                foreach (int leftDiagonalIndex in Enumerable.Range(0, m_Length))
                {
                    if (r_Board[leftDiagonalIndex, leftDiagonalIndex] != i_Symbol)
                    {
                        isDiagonalSerie = false;
                        break;
                    }
                }
            }
            else
            {
                isDiagonalSerie = false;
            }

            return isDiagonalSerie;
        }

        public bool CheckRightDiagonalSeries(eSymbol i_Symbol)
        {
            bool isDiagonalSerie = true;

            if (m_SquareEntered.Row == Length - m_SquareEntered.Col + 1)
            {
                foreach (int rightDiagonalIndex in Enumerable.Range(0, m_Length))
                {
                    if (r_Board[rightDiagonalIndex, Length - rightDiagonalIndex - 1] != i_Symbol)
                    {
                        isDiagonalSerie = false;
                        break;
                    }
                }
            }
            else
            {
                isDiagonalSerie = false;
            }

            return isDiagonalSerie;
        }

        public bool CheckGameSeries(eSymbol i_Symbol, Square i_SquareEntered)
        {
            return CheckRowSeries(i_Symbol, i_SquareEntered) || CheckColSeries(i_Symbol, i_SquareEntered) ||
                CheckLeftDiagonalSeries(i_Symbol, i_SquareEntered) || CheckRightDiagonalSeries(i_Symbol);
        }

        public void ResetBoard()
        {
            foreach (int row in Enumerable.Range(0, m_Length))
            {
                foreach (int col in Enumerable.Range(0, m_Length))
                {
                    r_Board[row, col] = eSymbol.Empty;
                }
            }

            m_NumOccupiedSquars = 0;
        }

        public void FillGameBoardCell(Square i_Square, eSymbol i_Symbol)
        {
            r_Board[i_Square.Row - 1, i_Square.Col - 1] = i_Symbol;
            m_NumOccupiedSquars++;
            m_SquareEntered = i_Square;
        }
    }
}