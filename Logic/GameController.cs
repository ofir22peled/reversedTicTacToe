using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Linq;


namespace Logic
{
    public enum eGameRoundStatus
    {
        None,
        Win,
        Tie,
        Quit
    }

    public class GameController
    {
        private readonly GameBoard r_GameBoard;
        private readonly PlayerManager r_PlayerManager;
        private eGameRoundStatus m_GameRoundStatus = 0;

        public GameController(int i_BoardSize, string i_Player1Name, string i_Player2Name, eEnemyType i_EnemyType)
        {
            r_GameBoard = new GameBoard(i_BoardSize);

            string[] playersNames = new string[2];
            playersNames[0] = i_Player1Name;
            playersNames[1] = "Computer";

            if (i_EnemyType == eEnemyType.Human)
            {
                playersNames[1] = i_Player2Name;
            }

            r_PlayerManager = new PlayerManager(playersNames, i_EnemyType);
        }

        public eGameRoundStatus GameRoundStatus
        {
            get
            {
                return m_GameRoundStatus;
            }
            set
            {
                m_GameRoundStatus = value;
            }
        }

        public Player GetCurrPlayer()
        {
            return r_PlayerManager.GetCurrentPlayer();
        }

        public Player GetOtherPlayer()
        {
            return r_PlayerManager.GetOtherPlayer();
        }

        public eEnemyType GetCurrPlayerType()
        {
            return r_PlayerManager.GetCurrentPlayer().EnemyType;
        }

        public eSymbol GetCurrPlayerSymbol()
        {
            return r_PlayerManager.GetCurrentPlayer().Symbol;
        }

        public int GetBoardSize()
        {
            return r_GameBoard.Length;
        }

        public eSymbol[,] GetGameBoard()
        {
            return r_GameBoard.Board;
        }

        public bool IsRoundTie()
        {
            bool isTie = false;

            if (r_GameBoard.IsBoardFull())
            {
                m_GameRoundStatus = eGameRoundStatus.Tie;
                isTie = true;
            }

            return isTie;
        }

        public bool CheckForWinner(Square i_LastSquareEntered)
        {
            bool isWinner = false;

            if (r_GameBoard.CheckGameSeries(GetCurrPlayerSymbol(), i_LastSquareEntered))
            {
                m_GameRoundStatus = eGameRoundStatus.Win;
                isWinner = true;
            }

            return isWinner;
        }

        public bool IsStatusQuitting()
        {
            return (GetGameRoundStatus() == eGameRoundStatus.Quit);
        }

        public bool IsRoundOver(Square i_LastSquareEntered)
        {
            return IsStatusQuitting() || CheckForWinner(i_LastSquareEntered) || IsRoundTie();
        }

        public void ResetBoard()
        {
            r_GameBoard.ResetBoard();
        }

        public void ResetGameStatus()
        {
            m_GameRoundStatus = eGameRoundStatus.None;
        }

        public void SwitchPlayerTurns()
        {
            r_PlayerManager.SwitchPlayerTurns();
        }

        public void IncreaseWinnersScore()
        {
            r_PlayerManager.GetOtherPlayer().Score++;
        }

        public Player[] GetPlayersArray()
        {
            return r_PlayerManager.GetPlayersArray;
        }

        public void FillGameBoardCell(Square i_SquareToFill)
        {
            r_GameBoard.FillGameBoardCell(i_SquareToFill, GetCurrPlayerSymbol());
        }

        public void EmptyGameBoardCell(Square i_SquareToFill)
        {
            r_GameBoard.FillGameBoardCell(i_SquareToFill, eSymbol.Empty);
        }

        public bool IsCellAvailable(Square i_SquareToCheck)
        {
            return r_GameBoard.IsCellEmpty(i_SquareToCheck);
        }

        public Square GetSquareFromBot()
        {
            return getRandomSquare();
        }

        private Square getRandomSquare()
        {
            List<Square> availableSquares = new List<Square>();

            for (int row = 1; row <= r_GameBoard.Length; row++)
            {
                for (int col = 1; col <= r_GameBoard.Length; col++)
                {
                    Square square = new Square(row, col);

                    if (r_GameBoard.IsCellEmpty(square))
                    {
                        availableSquares.Add(square);
                    }
                }
            }

            Random random = new Random();
            int index = random.Next(availableSquares.Count);

            return availableSquares[index];
        }

        public eGameRoundStatus GetGameRoundStatus()
        {
            return m_GameRoundStatus;
        }
    }
}