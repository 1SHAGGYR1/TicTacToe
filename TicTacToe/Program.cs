using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {

        class Board
        {
            Random rnd = new Random();
            List<int> freecells = new List<int>();
            int dimension, steps = 0, x, y;
            sbyte[,] field;
            bool PlayWithBot;
            void CreateField()
            {
                Console.WriteLine("Insert the number of dimensions!");
                dimension = int.Parse(Console.ReadLine());
                field = new sbyte[dimension, dimension];
                FillIn();
            }
            void FillIn()
            {
                for (int i = 0; i < dimension; i++)
                    for (int j = 0; j < dimension; j++)
                    {
                        field[i, j] = 0;
                        freecells.Add(j);
                        freecells.Add(i);
                    }
                Step();
            }
            public void GameModChose()
            {

                Console.WriteLine("Hi. Do u wanna play against your friend (yourself)(type 1 to choose) or against bot?(type 2 to choose)");
                string input = "0";
                do
                {
                    Console.WriteLine("Insert 1 or 2 please!");
                    input = Console.ReadLine();
                    PlayWithBot = (input == "1") ? false : true;
                }
                while (!((input == "1") || (input == "2")));
                CreateField();
            }
            bool SymbolsInRow()
            {
                sbyte save = field[y, x];
                byte count = 1;
                if (x >= y)
                {
                    int startx = x - y, starty = 0;
                    while ((startx < dimension - 1) && (starty < dimension - 1))
                    {
                        if ((field[starty, startx] == field[starty + 1, startx + 1]) && (field[starty, startx] != 0))
                        {
                            count++;
                            if (count == 3)
                                return true;
                        }
                        else
                            count = 1;
                        startx++;
                        starty++;
                    }
                }
                else
                {
                    int startx = 0, starty = y - x;
                    while ((startx < dimension - 1) && (starty < dimension - 1))
                    {
                        if ((field[starty, startx] == field[starty + 1, startx + 1]) && (field[starty, startx] != 0))
                        {
                            count++;
                            if (count == 3)
                                return true;
                        }
                        else
                            count = 1;
                        startx++;
                        starty++;
                    }
                }
                count = 1;
                if ((y + x) < dimension)
                {
                int startx2 = 0, starty2 = y + x;
                    while ((startx2 < dimension - 1) && (starty2 > 0))
                    {
                        if ((field[starty2, startx2] == field[starty2 - 1, startx2 + 1]) && (field[starty2, startx2] != 0))
                        {
                            count++;
                            if (count == 3)
                                return true;
                        }
                        else
                            count = 1;
                        startx2++;
                        starty2--;
                    }
                }
                count = 1;
                for (int i = 0; i < dimension - 1; i++)
                    if ((field[y, i] == field[y, i + 1]) && (field[y, i] != 0))
                    {
                        count++;
                        if (count == 3)
                            return true;
                    }
                    else
                        count = 1;
                count = 1;
                for (int i = 0; i < dimension - 1; i++)
                    if ((field[i, x] == field[i + 1, x]) && (field[i, x] != 0))
                    {
                        count++;
                        if (count == 3)
                            return true;
                    }
                    else
                        count = 1;

                return false;
            }
            bool CheckPosition(int number)
            {
                if  ((number < 0) || (number >= dimension))
                {
                    Console.WriteLine("U need to choose a number between zero and {0}, sry!", dimension);
                    return true;
                }
                return false;
            }
            bool CheckCell()
            {
                if (field[y, x] != 0)
                {
                    Console.WriteLine("Sorry dude, U can't assign this cell!");
                    return true;
                }
                return false;
            }
            void Display()
            {
                for (int i = 0; i < dimension; i++)
                {
                    string s = null;
                    for (int j = 0; j < dimension; j++)
                        s = s + "__";
                    Console.WriteLine(s);
                    for (int j = 0; j < dimension; j++)
                    {
                        if ((field[j, i]) == -1)
                            Console.Write("X|");
                        if ((field[j, i]) == 1)
                            Console.Write("O|");
                        if ((field[j, i]) == 0)
                            Console.Write(" |");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            void CreateRandomCell(ref int x, ref int y)
            {
                freecells.Clear();
                for (int i = 0; i < dimension; i++)
                    for (int j = 0; j < dimension; j++)
                    {
                        if (field[i, j] == 0)
                        {
                            freecells.Add(j);
                            freecells.Add(i);
                        }
                    }
                int randomindex = rnd.Next(0,freecells.Count - 1);
                if (randomindex % 2 == 0)
                {
                    x = freecells[randomindex];
                    y = freecells[randomindex + 1];
                    freecells.RemoveRange(randomindex, 2);
                }
                else
                {
                    y = freecells[randomindex];
                    x = freecells[randomindex - 1];
                    freecells.RemoveRange(randomindex - 1, 2);
                }
                return;
                
            }
            void RandomBotStep()
            {
                steps++;
                Console.WriteLine("Bot's turn now.");
                CreateRandomCell(ref x, ref y);
                field[y, x] = -1;
                if (SymbolsInRow())
                {
                    Display();
                    Console.WriteLine("Oh. What's a pity. Bot won.");
                    return;
                }
                else
                {
                    Display();
                    Step();
                }
                
            }
            void Step()
            {
                if (steps == dimension * dimension)
                {
                    Console.WriteLine("It's draw!.Wp!");
                    return;
                }
                else
                {
                    steps++;
                    do
                    {
                        do
                        {
                            if ((steps % 2) == 0)
                                Console.WriteLine("Your turn, player 2. Make your choise, player 2.{0}Your row: ", Environment.NewLine);
                            else
                                Console.WriteLine("Your turn, player 1. Make your choise, player 1.{0}Your row: ", Environment.NewLine);
                            x = int.Parse(Console.ReadLine()) - 1;
                        }
                        while (CheckPosition(x));
                        do
                        {

                            Console.WriteLine("Now choose your collumn: ");
                            y = int.Parse(Console.ReadLine()) - 1;
                        }
                        while (CheckPosition(y));
                    }
                    while (CheckCell());
                    
                    if (steps % 2 == 0)
                        field[y, x] = -1;
                    else
                        field[y, x] = 1;

                    if ((SymbolsInRow()) && (steps % 2 == 1))
                    {
                        Display();
                        Console.WriteLine("Congrats! First player won!");
                        return;
                    }
                    if ((SymbolsInRow()) && (steps % 2 == 0))
                    {
                        Display();
                        Console.WriteLine("Congrats! Second player won!");
                        return;
                    }
                    else
                    {
                        if (!PlayWithBot)
                        {
                            Display();
                            Step();
                        }
                        else
                        {
                            Display();
                            RandomBotStep();
                        }
                    }

                }
            }
        }
        static void Main(string[] args)
        {
            Board TicTac = new Board();
            TicTac.GameModChose();
            Console.ReadKey();
        }
    }
}
