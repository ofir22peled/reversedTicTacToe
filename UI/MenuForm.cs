using System;
using System.Windows.Forms;
using Logic;

namespace UI
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        public string TextBoxPlayer1Name
        {
            get
            {
                return this.textBoxPlayer1Name.Text;
            }
        }

        public string TextBoxPlayer2Name
        {
            get
            {
                return this.textBoxPlayer2Name.Text;
            }
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        public bool IsValidPlayersNames()
        {
            return !(string.IsNullOrWhiteSpace(textBoxPlayer1Name.Text) || string.IsNullOrWhiteSpace(textBoxPlayer2Name.Text));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                textBoxPlayer2Name.Enabled = true;
                textBoxPlayer2Name.Text = "";
            }
            else
            {
                textBoxPlayer2Name.Enabled = false;
            }
        }

        public eEnemyType EnemyType()
        {
            eEnemyType enemyType;

            if (checkBox1.Checked)
            {
                enemyType = eEnemyType.Human;
            }
            else
            {
                enemyType = eEnemyType.Bot;
            }

            return enemyType;
        }

        private void textBoxPlayer1Name_TextChanged(object sender, EventArgs e)
        {
            TextBox Name = sender as TextBox;

            textBoxPlayer1Name.Text = Name.Text;
        }

        private void textBoxPlayer2Name_TextChanged(object sender, EventArgs e)
        {
            TextBox Name = sender as TextBox;

            textBoxPlayer2Name.Text = Name.Text;
        }

        private void nUDRows_ValueChanged(object sender, EventArgs e)
        {
            int rows = (int)nUDRows.Value;

            nUDCols.Value = rows;
        }

        public int BoardSize
        {
            get
            {
                return (int)nUDRows.Value;
            }
        }

        private void nUDCols_ValueChanged(object sender, EventArgs e)
        {
            int cols = (int)nUDCols.Value;

            nUDRows.Value = cols;
        }
    }
}
