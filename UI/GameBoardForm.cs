using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Logic;

namespace UI
{
    public partial class GameBoardForm : Form
    {
        private bool m_GotPlayersNames = false;
        private Button[,] m_GameBoard;
        private MenuForm m_MenuForm;
        private GameController m_GameController;
        private Label m_Player1Score;
        private Label m_Player2Score;

        public GameBoardForm()
        {
            initializeComponent();
        }

        private void initializeComponent()
        {
            m_MenuForm = new MenuForm();

            if (getValidPlayersNames())
            {
                m_GameController = new GameController(m_MenuForm.BoardSize, m_MenuForm.TextBoxPlayer1Name, m_MenuForm.TextBoxPlayer2Name, m_MenuForm.EnemyType());
                createBoard();
                initialLogicBoard();
            }
        }

        public void initialLogicBoard()
        {
            m_GameController.ResetBoard();
            m_GameController.ResetGameStatus();
        }

        private void clearBoard()
        {
            foreach (Button button in m_GameBoard)
            {
                button.Text = "";
                button.Enabled = true;
            }
        }

        private void button_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            button.Text = m_GameController.GetCurrPlayerSymbol().ToString();
            button.Enabled = false;
            updateHumanMove(button);
            m_GameController.SwitchPlayerTurns();

            if (m_GameController.GetCurrPlayer().EnemyType == eEnemyType.Bot)
            {
                updateComputerMove();
            }

            boldCurrPlayerLable();
        }

        private void boldCurrPlayerLable()
        {
            if (m_GameController.GetCurrPlayer() == m_GameController.GetPlayersArray()[0])
            {
                m_Player1Score.Font = new Font(m_Player1Score.Font, FontStyle.Bold);
                m_Player2Score.Font = new Font(m_Player2Score.Font, FontStyle.Regular);
            }
            else
            {
                m_Player1Score.Font = new Font(m_Player1Score.Font, FontStyle.Regular);
                m_Player2Score.Font = new Font(m_Player2Score.Font, FontStyle.Bold);
            }

            m_Player1Score.Refresh();
            m_Player2Score.Refresh();
        }

        private void updateHumanMove(Button i_button)
        {
            Square chosenSquare = (Square)i_button.Tag;

            m_GameController.FillGameBoardCell(chosenSquare);

            if (IsRoundOver(chosenSquare))
            {
                roundOverOperation();
            }
        }

        private void updateComputerMove()
        {
            Square computerSquare = m_GameController.GetSquareFromBot();

            m_GameController.FillGameBoardCell(computerSquare);

            Button computerButton = getButtonFromSquare(computerSquare);

            computerButton.Text = eSymbol.O.ToString();
            computerButton.Enabled = false;

            if (IsRoundOver(computerSquare))
            {
                roundOverOperation();
            }

            m_GameController.SwitchPlayerTurns();
        }

        private Button getButtonFromSquare(Square square)
        {
            int row = square.Row - 1;
            int column = square.Col - 1;

            return m_GameBoard[row, column];
        }

        private void updateRoundResluts()
        {
            switch (m_GameController.GameRoundStatus)
            {
                case eGameRoundStatus.Win:
                    m_GameController.IncreaseWinnersScore();
                    announceRoundWinner();
                    updateScoreLabel();
                    break;

                case eGameRoundStatus.Tie:
                    announceRoundTie();
                    break;
            }
        }

        private void updateScoreLabel()
        {
            Player[] players = m_GameController.GetPlayersArray();
            string Player1ScoreText = string.Format("{0}: {1}", players[0].Name, players[0].Score);
            string Player2ScoreText = string.Format("{0}: {1}", players[1].Name, players[1].Score);

            m_Player1Score.Text = Player1ScoreText;
            m_Player2Score.Text = Player2ScoreText;
        }

        private void roundOverOperation()
        {
            updateRoundResluts();
        }

        public bool IsRoundOver(Square i_LastSquareEntered)
        {
            return m_GameController.IsRoundOver(i_LastSquareEntered);
        }

        private void announceRoundWinner()
        {
            Player winnerPlayer = m_GameController.GetOtherPlayer();

            DialogResult userAnswerAboutRestart = MessageBox.Show(string.Format(@"The winner is {0}!
Do you want to play another round?", winnerPlayer.Name), "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            handleRestartGame(userAnswerAboutRestart);
        }

        private void handleRestartGame(DialogResult i_UserAnswer)
        {
            if (i_UserAnswer == DialogResult.Yes)
            {
                clearBoard();
                initialLogicBoard();
            }
            else
            {
                this.Close();
            }
        }

        private void announceRoundTie()
        {
            DialogResult userAnswerAboutRestart = MessageBox.Show(string.Format(@"Tie!
Do you want to play another round?"), "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            handleRestartGame(userAnswerAboutRestart);
        }

        private void createBoard()
        {
            int buttonSize = Math.Min(this.ClientSize.Width / m_MenuForm.BoardSize, this.ClientSize.Height / m_MenuForm.BoardSize);
            int boardWidth = buttonSize * m_MenuForm.BoardSize;
            int boardHeight = buttonSize * m_MenuForm.BoardSize;

            this.Size = new Size(buttonSize * m_MenuForm.BoardSize + 100, buttonSize * m_MenuForm.BoardSize + 100);

            m_Player1Score = new Label();
            m_Player1Score.Location = new Point(40, buttonSize * m_MenuForm.BoardSize + 20);
            m_Player2Score = new Label();
            m_Player2Score.Location = new Point(m_Player1Score.Right + 10, buttonSize * m_MenuForm.BoardSize + 20);

            this.Controls.Add(m_Player1Score);
            this.Controls.Add(m_Player2Score);

            m_GameBoard = new Button[m_MenuForm.BoardSize, m_MenuForm.BoardSize];
            this.ClientSize = new Size(boardWidth + 5, boardHeight + m_Player1Score.Height + 20);
            updateScoreLabel();

            for (int i = 0; i < m_MenuForm.BoardSize; i++)
            {
                for (int j = 0; j < m_MenuForm.BoardSize; j++)
                {
                    m_GameBoard[i, j] = new Button();
                    m_GameBoard[i, j].Size = new Size(buttonSize, buttonSize);
                    m_GameBoard[i, j].Location = new Point(j * buttonSize, i * buttonSize);
                    m_GameBoard[i, j].Tag = new Square(i + 1, j + 1);
                    m_GameBoard[i, j].Click += new System.EventHandler(this.button_click);
                    this.Controls.Add(m_GameBoard[i, j]);
                }
            }
        }

        private bool getValidPlayersNames()
        {
            bool isDialogClosed = false;

            do
            {
                m_MenuForm.ShowDialog();

                if (m_MenuForm.DialogResult == DialogResult.OK)
                {
                    if (m_MenuForm.IsValidPlayersNames())
                    {
                        m_GotPlayersNames = true;
                    }
                    else
                    {
                        MessageBox.Show("You inserted an empty name!", "Name Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    m_GotPlayersNames = false;
                    isDialogClosed = true;
                }
            }
            while (!m_GotPlayersNames && !isDialogClosed);

            return m_GotPlayersNames;
        }

        public Form MenuForm
        {
            get
            {
                return m_MenuForm;
            }
        }

        private void gameBoardForm_Load(object sender, EventArgs e)
        {

        }
    }
}