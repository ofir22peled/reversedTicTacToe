using System;

namespace Logic
{
    public enum eEnemyType
    {
        Bot,
        Human
    }

    internal class PlayerManager
    {
        private readonly Player[] r_Players;
        private int m_PlayerTurn = 0;
        private const int k_NumOfPlayers = 2;

        public PlayerManager(string[] i_PlayersNames, eEnemyType i_EnemyType)
        {
            r_Players = new Player[k_NumOfPlayers];
            r_Players[0] = new Player(i_PlayersNames[0], eSymbol.X, eEnemyType.Human);
            r_Players[1] = new Player(i_PlayersNames[1], eSymbol.O, i_EnemyType);
        }

        public Player[] GetPlayersArray
        {
            get
            {
                return r_Players;
            }
        }

        public Player GetCurrentPlayer()
        {
            return r_Players[m_PlayerTurn];
        }

        public Player GetOtherPlayer()
        {
            return r_Players[1 - m_PlayerTurn];
        }

        public Player SwitchPlayerTurns()
        {
            m_PlayerTurn = 1 - m_PlayerTurn;

            return r_Players[m_PlayerTurn];
        }
    }
}
