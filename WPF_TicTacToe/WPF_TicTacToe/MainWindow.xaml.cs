using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://www.youtube.com/watch?v=mnTyiUAHuVk
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        // Holds the current results of cells in the active game
        private MarkType[] mResults;

        // True if it is player 1's turn (X) or 2's player turn (O)
        private bool mPlayer1Turn;

        // True if the game has ended
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        // Start a anew game and clear all values back to the start 
        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new MarkType[9];

            for (int i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure Player 1 starts the game
            mPlayer1Turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change content, background, foreground to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;

        }

        private void Btn_Click(object sender, RoutedEventArgs e)  // button = sender, e = event of the click
        {
            // Start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // cast the sender to a button
            var button = (Button)sender;   // ...explicit casting...

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based which player turn is it
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change cross to red
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // Toggle the players turns
            mPlayer1Turn ^= true;
            //if (mPlayer1Turn)
            //    mPlayer1Turn = false;
            //else mPlayer1Turn = true;

            //Check for a winner
            CheckForWinner();

        }

        private void CheckForWinner()
        {
            #region Horizontal Wins

            // -- Row 0 wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                // Highlight winning cells
                Btn0_0.Background = Btn1_0.Background = Btn2_0.Background = Brushes.Yellow;
            }

            // -- Row 1 wins
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                // Highlight winning cells
                Btn0_1.Background = Btn1_1.Background = Btn2_1.Background = Brushes.Yellow;
            }

            // -- Row 2 wins
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                // Highlight winning cells
                Btn0_2.Background = Btn1_2.Background = Btn2_2.Background = Brushes.Yellow;
            }

            #endregion


            #region Vertical Wins

            // -- Column 0 wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                // Highlight winning cells
                Btn0_0.Background = Btn0_1.Background = Btn0_2.Background = Brushes.Yellow;
            }

            // -- Column 1 wins
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                // Highlight winning cells
                Btn1_0.Background = Btn1_1.Background = Btn1_2.Background = Brushes.Yellow;
            }

            // -- Column 2 wins
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                // Highlight winning cells
                Btn2_0.Background = Btn2_1.Background = Btn2_2.Background = Brushes.Yellow;
            }

            #endregion


            #region Diagonal Wins

            // -- Top Left Diagonal wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                // Highlight winning cells
                Btn0_0.Background = Btn1_1.Background = Btn2_2.Background = Brushes.Yellow;
            }

            // -- Top Right Diagonal wins
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                // Highlight winning cells
                Btn2_0.Background = Btn1_1.Background = Btn0_2.Background = Brushes.Yellow;
            }

            #endregion


            #region No Winner

            // Check for no winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Game ends
                mGameEnded = true;

                // Turn all cells gray
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Gray;
                });
            }

            #endregion
        }
    }
}
