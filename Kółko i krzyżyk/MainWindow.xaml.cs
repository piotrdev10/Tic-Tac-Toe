using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kółko_i_krzyżyk
{
    public partial class MainWindow : Window
    {
        private string currentPlayer = "X";
        private int moves = 0;

        public MainWindow()
        {
            InitializeComponent();
            StatusText.Text = "Tura gracza: X";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;

            if (clicked.Content != null) return;

            clicked.Content = currentPlayer;
            moves++;

            if (CheckWin())
            {
                StatusText.Text = $"Gracz {currentPlayer} wygrał!";
                DisableAllButtons();
            }
            else if (moves == 9)
            {
                StatusText.Text = "Remis!";
            }
            else
            {
                currentPlayer = currentPlayer == "X" ? "O" : "X";
                StatusText.Text = $"Tura gracza: {currentPlayer}";
            }
        }

        private bool CheckWin()
        {
            Button[] b = new Button[] { Btn0, Btn1, Btn2, Btn3, Btn4, Btn5, Btn6, Btn7, Btn8 };

            int[,] winConditions = new int[,]
            {
                {0,1,2}, {3,4,5}, {6,7,8},
                {0,3,6}, {1,4,7}, {2,5,8},
                {0,4,8}, {2,4,6}
            };

            for (int i = 0; i < winConditions.GetLength(0); i++)
            {
                int a = winConditions[i, 0];
                int b1 = winConditions[i, 1];
                int c = winConditions[i, 2];

                if (b[a].Content?.ToString() == currentPlayer &&
                    b[b1].Content?.ToString() == currentPlayer &&
                    b[c].Content?.ToString() == currentPlayer)
                {
                    return true;
                }
            }

            return false;
        }

        private void DisableAllButtons()
        {
            foreach (var child in GameGrid.Children)
            {
                if (child is Button btn)
                    btn.IsEnabled = false;
            }
        }

        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (var child in GameGrid.Children)
            {
                if (child is Button btn)
                {
                    btn.Content = null;
                    btn.IsEnabled = true;
                }
            }

            currentPlayer = "X";
            moves = 0;
            StatusText.Text = "Tura gracza: X";
        }
    }
}
