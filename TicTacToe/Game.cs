using System;

namespace TicTacToe_73
{
    class Game
    {
        private int Turn = 0;
        private readonly char[,] GameMatrix = new char[3, 3]
        {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };
        private struct Player
        {
            public string PlayerName;
            public char Symbol;
        };
        private readonly Player[] Players = new Player[2];
        private bool PlayerNamesSet = false;

        private void SetConsole()
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("     |     |     ");

                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"  {GameMatrix[i, j]}  ");
                    if (j == 2)
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("|");
                    }
                }

                Console.WriteLine("     |     |     ");
                if (i != 2)
                {
                    Console.WriteLine("-----------------");
                }
            }
        }

        private void Update_PlayField(int row, int column, char symbol = ' ')
        {
            Console.Clear();
            GameMatrix[row, column] = symbol;
            SetConsole();
        }

        private bool CheckGameMatrix(char ch)
        {
            int vCount, hCount;
            for (int i = 0; i < 3; i++)
            {
                hCount = 0;
                vCount = 0;
                for (int j = 0; j < 3; j++)
                {
                    //check horizontal
                    if (GameMatrix[i, j] == ch)
                    {
                        hCount++;
                    }

                    //check vertical
                    if (GameMatrix[j, i] == ch)
                    {
                        vCount++;
                    }
                }
                if (vCount == 3 || hCount == 3)
                {
                    return true;
                }
            }

            //check diagonal
            if (GameMatrix[1, 1] == ch)
            {
                if (GameMatrix[0, 0] == ch && GameMatrix[2, 2] == ch)
                {
                    return true;
                }

                if (GameMatrix[0, 2] == ch && GameMatrix[2, 0] == ch)
                {
                    return true;
                }
            }

            //if there is no winner return false
            return false;
        }

        private void ResetGameMatrix()
        {
            Console.Clear();
            int counter = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameMatrix[i, j] = char.Parse((counter++).ToString());
                }
            }

            Turn = 0;
        }

        //input player name and assign symbol
        private void InputPlayerNames()
        {
            for (int i = 0; i < 2; i++)
            {
                string name = null;
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Enter Player {0}'s Name: ", i + 1);
                    name = Console.ReadLine();
                }

                Players[i].PlayerName = name;
            }

            Players[0].Symbol = 'X';
            Players[1].Symbol = 'O';

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("{0} symbol is {1}", Players[i].PlayerName, Players[i].Symbol);
            }

            PlayerNamesSet = true;
        }

        private bool CheckPlayFieldAt(int row, int column)
        {
            if (GameMatrix[row, column] == 'X' || GameMatrix[row, column] == 'O')
            {
                throw new Exception("Symbol can't be overwritten.");
            }

            return true;
        }

        public void Play()
        {
            while (true)
            {
                if (!PlayerNamesSet)
                {
                    InputPlayerNames();
                }
                SetConsole();
                bool gameOver = false;

                //loop run till the end of the match(either draw or someone wins)
                do
                {
                    //loop to switch between 2 players
                    for (int i = 0; i < 2; i++)
                    {
                        //do-while loop to check if input is correct
                        do
                        {
                            try
                            {
                                Console.Write($"{Players[i].PlayerName}'s turn! \nEnter position: ");
                                int position;
                                if (!int.TryParse(Console.ReadLine(), out position))
                                {
                                    throw new Exception("Invalid position entered.");
                                }
                                else if (position < 1 || position > 9)
                                {
                                    throw new Exception("Entered value is out of range (range: 1-9");
                                }
                                else
                                {
                                    int row, column;
                                    --position;
                                    row = position / 3;
                                    column = position % 3;

                                    CheckPlayFieldAt(row, column);
                                    Update_PlayField(row, column, Players[i].Symbol);
                                    gameOver = CheckGameMatrix(Players[i].Symbol);
                                    Turn++;
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Please enter input again");
                            }
                        } while (true);

                        //check if any player one won the game
                        if (gameOver == true)
                        {
                            Console.WriteLine("{0} won", Players[i].PlayerName);
                            break;
                        }

                        //check if game was draw
                        else if (Turn == 9)
                        {
                            Console.WriteLine("Draw");
                            gameOver = true;
                            break;
                        }
                    }
                } while (gameOver == false);

                Console.Write("\nDo you want to play again(y/n)? ");
                var response = Console.ReadKey();
                if (response.Key == ConsoleKey.Y)
                {
                    Console.Write("\nDo you want to use the same players(y/n)? ");
                    response = Console.ReadKey();
                    if (response.Key == ConsoleKey.N)
                    {
                        PlayerNamesSet = false;
                    }

                    ResetGameMatrix();
                }
                else
                {
                    break;
                }
            }
        }
    }
}