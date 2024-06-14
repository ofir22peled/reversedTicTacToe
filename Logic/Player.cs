using System;

namespace Logic
{
    public class Player
    {
        private readonly string r_Name;
        private readonly eSymbol r_Symbol;
        private readonly eEnemyType r_EnemyType;
        private int m_Score = 0;

        public Player(string i_Name, eSymbol i_Symbol, eEnemyType i_Type)
        {
            r_Name = i_Name;
            r_Symbol = i_Symbol;
            r_EnemyType = i_Type;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public eSymbol Symbol
        {
            get
            {
                return r_Symbol;
            }
        }

        public eEnemyType EnemyType
        {
            get
            {
                return r_EnemyType;
            }
        }
    }
}