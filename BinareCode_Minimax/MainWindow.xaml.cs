using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

class Move
{
    public int position;
};

namespace BinareCode_Minimax
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        static string player = "Human", opponent = "Computer";
        static string hasStarted, numbers;
        string stringOfNumbers = null;
        char[] possibleCombinations;

        private void Start_Click(object sender, RoutedEventArgs e)
        {            

            if (playerButton.IsChecked == true)
            {
                Random rand = new Random();

                for (int i = 0; i < 10; i++)
                {
                    buttons(i).IsEnabled = true;
                    buttons(i).IsChecked = false;
                    stringOfNumbers += Convert.ToString(rand.Next(0, 2));
                }

                textBox.Text = stringOfNumbers;
                Combinations(textBox);

                startButton.IsEnabled = false;
                moveButton.IsEnabled = true;
                hasStarted = player;
                MessageBox.Show("Game has started");

            }
            else
            {
                if(computerButton.IsChecked == true)
                {
                    Random rand = new Random();

                    for (int i = 0; i < 10; i++)
                    {
                        buttons(i).IsEnabled = true;
                        buttons(i).IsChecked = false;
                        stringOfNumbers += Convert.ToString(rand.Next(0, 2));
                    }

                    textBox.Text = stringOfNumbers;
                    Combinations(textBox);

                    startButton.IsEnabled = false;
                    hasStarted = opponent;
                    MessageBox.Show("Game has started");

                    computerStep();
                    for (int i = 0; i < 10; i++)
                    {
                        buttons(i).IsChecked = false;
                    }
                    moveButton.IsEnabled = true;
                }
                else
                {                    
                    moveButton.IsEnabled = false;
                    MessageBox.Show("Error");                                  
                }                               
            }
            
        }

        public char[] Combinations(TextBox textBox)
        {
            possibleCombinations = null;
            possibleCombinations = new char[textBox.Text.Length];

            using (StringReader sr = new StringReader(textBox.Text))
            {
                sr.Read(possibleCombinations, 0, textBox.Text.Length);
            }
            return possibleCombinations;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < stringOfNumbers.Length; i++)
            {
                if (buttons(i).IsChecked == true && buttons(i - 1).IsChecked == true)
                {
                    numbers = Convert.ToString(possibleCombinations[i - 1]);
                    numbers += Convert.ToString(possibleCombinations[i]);
                    stringOfNumbers = null;

                    buttons(i - 1).IsChecked = false;
                    buttons(i).IsChecked = false;

                    if (numbers == "10" || numbers == "00")
                    {
                        possibleCombinations[i - 1] = '1';
                        possibleCombinations[i] = '-';
                    }

                    else
                    {
                        possibleCombinations[i - 1] = '0';
                        possibleCombinations[i] = '-';
                    }

                    for (int j = 0; j < possibleCombinations.Length; j++)
                    {
                        if (possibleCombinations[j] == '-')
                        {
                            continue;
                        }
                        else
                        {
                            stringOfNumbers += possibleCombinations[j];
                        }
                    }

                    textBox.Text = stringOfNumbers;
                    Combinations(textBox);
                    buttons(stringOfNumbers.Length).IsEnabled = false;

                    if(isVictory() == true)
                    {
                        break;
                    }

                    MessageBox.Show("AI step");
                    computerStep();

                    if(isVictory() == true)
                    {
                        break;
                    }

                }
            }
        }

        public Boolean isMovesLeft(char[] stringOfNumbers)
        {
            if(stringOfNumbers.Length > 2)
            {
                return true;
            }
            return false;
        }

        public static int evaluate(char[] combinationsForVictory)
        {
            numbers = new string(combinationsForVictory);
            if(hasStarted == player)
            {
                if(numbers == "11" || numbers == "00")
                {
                    return -10;
                }
                else if(numbers == "10" || numbers == "01")
                {
                    return +10;
                }
            }

            if(hasStarted == opponent)
            {
                if(numbers == "10" || numbers == "01")
                {
                    return -10;
                }
                else if (numbers == "11" || numbers == "00")
                {
                    return +10;
                }
            }
            return 0;
        }

        public int minimax(char[] possibleCombinations, int depth, Boolean isMax)
        {
            char[] tempArray;
            numbers = null;
            int score = evaluate(possibleCombinations);

            if (score == 10)
                return score;

            if (score == -10)
                return score;

            if (isMovesLeft(possibleCombinations) == false)
                return 0;

            if (isMax)
                {
                    int best = -1000;

                    for(int i = 1; i < possibleCombinations.Length; i++)
                    {
                        numbers = Convert.ToString(possibleCombinations[i - 1]);
                        numbers += Convert.ToString(possibleCombinations[i]);

                        if (numbers == "10" || numbers == "00")
                        {
                            string x = new string(possibleCombinations);
                        tempArray = x.ToCharArray();

                            possibleCombinations[i - 1] = '1';

                            string arr = new string(possibleCombinations);                            
                            arr = arr.Remove(i,1);
                            possibleCombinations = arr.ToCharArray();
                            
                            best = Math.Max(best, minimax(possibleCombinations, depth + 1, !isMax));

                            possibleCombinations = tempArray; 
                        }

                        else
                        {
                            string x = new string(possibleCombinations);
                        tempArray = x.ToCharArray();
                            possibleCombinations[i - 1] = '0';

                            string arr = new string(possibleCombinations);
                            arr = arr.Remove(i,1);
                            possibleCombinations = arr.ToCharArray();

                            best = Math.Min(best, minimax(possibleCombinations, depth + 1, !isMax));

                            possibleCombinations = tempArray;
                        }
                    }
                    return best;
                }
                else
                {
                int best = 1000;

                for (int i = 1; i < possibleCombinations.Length; i++)
                {
                    numbers = Convert.ToString(possibleCombinations[i - 1]);
                    numbers += Convert.ToString(possibleCombinations[i]);

                    if (numbers == "10" || numbers == "00")
                    {
                        string x = new string(possibleCombinations);
                        tempArray = x.ToCharArray();
                        possibleCombinations[i - 1] = '1';

                        string arr = new string(possibleCombinations);
                        arr = arr.Remove(i, 1);
                        possibleCombinations = arr.ToCharArray();

                        best = Math.Max(best, minimax(possibleCombinations, depth + 1, !isMax));

                        possibleCombinations = tempArray;
                    }

                    else
                    {
                        string x = new string(possibleCombinations);
                        tempArray = x.ToCharArray();
                        possibleCombinations[i - 1] = '0';

                        string arr = new string(possibleCombinations); 
                        arr = arr.Remove(i, 1);
                        possibleCombinations = arr.ToCharArray();

                        best = Math.Min(best, minimax(possibleCombinations, depth + 1, !isMax));

                        possibleCombinations = tempArray;
                    }
                }
                return best;
            }
        }

        Move findBestMove(char[] possibleCombinations)
        {
            int bestVal = -1000;
            Move bestMove = new Move();
            bestMove.position = -1;
            char[] tempArray;

            for (int i = 1; i < possibleCombinations.Length; i++)
            {
                numbers = Convert.ToString(possibleCombinations[i - 1]);
                numbers += Convert.ToString(possibleCombinations[i]);

                if (numbers == "10" || numbers == "00")
                {
                    string x = new string(possibleCombinations);
                    tempArray = x.ToCharArray();
                    possibleCombinations[i - 1] = '1';

                    string arr = new string(possibleCombinations);

                    arr = arr.Remove(i, 1);
                    possibleCombinations = arr.ToCharArray();

                    int moveVal = minimax(possibleCombinations, 0, false);

                    possibleCombinations = tempArray;

                    if (moveVal > bestVal)
                    {
                        bestMove.position = i;
                        bestVal = moveVal;
                    }
                }

                else
                {
                    string x = new string(possibleCombinations);
                    tempArray = x.ToCharArray();
                    possibleCombinations[i - 1] = '0';

                    string arr = new string(possibleCombinations);
                    
                    arr = arr.Remove(i, 1);
                    possibleCombinations = arr.ToCharArray();

                    int moveVal = minimax(possibleCombinations, 0, false);

                    possibleCombinations = tempArray;

                    if (moveVal > bestVal)
                    {
                        bestMove.position = i;
                        bestVal = moveVal;
                    }
                }
            }
            return bestMove;
        }

        public CheckBox buttons(int count)
        {
            CheckBox[] buttons = new CheckBox[10];
            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
            buttons[8] = button9;
            buttons[9] = button10;

            return buttons[count];
        }

        public void computerStep()
        {
            Move bestMove = findBestMove(possibleCombinations);

            for (int i = 1; i < possibleCombinations.Length; i++)
            {
                numbers = Convert.ToString(possibleCombinations[bestMove.position - 1]);
                numbers += Convert.ToString(possibleCombinations[bestMove.position]);

                if (numbers == "10" || numbers == "00")
                {
                    possibleCombinations[bestMove.position - 1] = '1';

                    string arr = new string(possibleCombinations);
                    arr = arr.Remove(bestMove.position, 1);
                    textBox.Text = arr;

                    buttons(arr.Length).IsEnabled = false;
                }

                else
                {
                    possibleCombinations[bestMove.position - 1] = '0';

                    string arr = new string(possibleCombinations);
                    arr = arr.Remove(bestMove.position, 1);
                    textBox.Text = arr;

                    buttons(arr.Length).IsEnabled = false;
                }
            }
            Combinations(textBox);
        }

        public Boolean isVictory()
        {
            if (isMovesLeft(possibleCombinations) == false)
            {
                if ((textBox.Text == "11" || textBox.Text == "00") && hasStarted == player)
                {
                    MessageBox.Show("Player has won");

                    stringOfNumbers = null;

                    button1.IsEnabled = false;
                    button2.IsEnabled = false;

                    startButton.IsEnabled = true;
                    moveButton.IsEnabled = false;

                    return true;
                }
                else
                {
                    if ((textBox.Text == "11" || textBox.Text == "00") && hasStarted == opponent)
                    {
                        MessageBox.Show("Computer has won");

                        stringOfNumbers = null;

                        button1.IsEnabled = false;
                        button2.IsEnabled = false;

                        startButton.IsEnabled = true;
                        moveButton.IsEnabled = false;

                        return true;
                    }
                }

                if ((textBox.Text == "10" || textBox.Text == "01") && hasStarted == player)
                {
                    MessageBox.Show("Computer has won");

                    stringOfNumbers = null;

                    button1.IsEnabled = false;
                    button2.IsEnabled = false;

                    startButton.IsEnabled = true;
                    moveButton.IsEnabled = false;

                    return true;
                }
                else
                {
                    if ((textBox.Text == "10" || textBox.Text == "01") && hasStarted == opponent)
                    {
                        MessageBox.Show("Player has won");

                        stringOfNumbers = null;

                        button1.IsEnabled = false;
                        button2.IsEnabled = false;

                        startButton.IsEnabled = true;
                        moveButton.IsEnabled = false;

                        return true;
                    }
                }
            }
            return false;
        }
    }
}
