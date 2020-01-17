using System;

namespace TicTacToe_73
{
    class Game
    {
        private int turn = 0;
        char[,] GameMatrix = new char[3, 3]
        {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };
        private struct players
        {
            public string playerName;
            public char symbol;
        };
        private players[] player = new players[2];


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
                        Console.WriteLine();
                    else
                        Console.Write("|");
                }

                Console.WriteLine("     |     |     ");
                if (i != 2)
                    Console.WriteLine("-----------------");
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
            int vCount = 0, hCount = 0;
            for (int i = 0; i < 3; i++)
            {
                hCount = 0;
                vCount = 0;
                for (int j = 0; j < 3; j++)
                {
                    //check horizontal
                    if (GameMatrix[i, j] == ch)
                        hCount++;

                    //check vertical
                    if (GameMatrix[j, i] == ch)
                        vCount++;
                }
                if (vCount == 3 || hCount == 3) return true;
            }

            //check diagonal
            if (GameMatrix[1, 1] == ch)
            {
                if (GameMatrix[0, 0] == ch && GameMatrix[2, 2] == ch)
                    return true;
                if (GameMatrix[0, 2] == ch && GameMatrix[2, 0] == ch)
                    return true;
            }
            //if there is no winner return false
            return false;
        }

        //input player name and assign symbol
        private void InputPlayerNames()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.Write("Enter Player1's Name : ");
                player[i].playerName = Console.ReadLine();
            }
            player[0].symbol = 'X';
            player[1].symbol = 'O';
            for (int i = 0; i < 2; i++)
                Console.WriteLine("{0} symbol is {1}", player[i].playerName, player[i].symbol);
        }

        private bool CheckPlayFieldAt(int row, int column)
        {
            if (GameMatrix[row, column] == 'X' || GameMatrix[row, column] == 'O')
                throw new Exception("Symbol can't be overwritten.");
            return true;
        }

        public void play()
        {
            InputPlayerNames();
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
                            Console.Write($"{player[i].playerName}'s turn! \nEnter position :");
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
                                Update_PlayField(row, column, player[i].symbol);
                                gameOver = CheckGameMatrix(player[i].symbol);
                                turn++;
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
                        Console.WriteLine("{0} won", player[i].playerName);
                        break;
                    }

                    //check if game was draw
                    else if (turn == 9)
                    {
                        Console.WriteLine("Draw");
                        gameOver = true;
                        break;
                    }
                }
            } while (gameOver == false);
        }
    }
}