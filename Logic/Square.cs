using System;

namespace Logic
{
    public struct Square
    {
        private int m_Row;
        private int m_Col;

        public Square(int i_Row = 0, int i_Col = 0)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }
            set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }
            set
            {
                m_Col = value;
            }
        }
    }
}
