using System;
using System.Text;

namespace Dymova.DotNetCourse.TicTacToe
{

    public class ConsoleInterface
    {
        private const string symbolX = "x";
        private const string symbolO = "o";
        private const string symbolDraw = "d";
        private const string symbolFree = " ";

        public static void DisplayHelp()
        {
//            Console.Write(
//                " Игровое поле разделено на 9 полей, каждое из которых представляет собой мини-поле для классических крестиков-ноликов. \n Участники поочередно ставят крестики и нолики. \n Следующий участник имеет право ходить только в то маленькое поле (относительно большого), которое соответствует клетке, относительно маленького поля, в которую походил предыдущий участник. \n Выигрывая маленькое поле, участник ставит в нем свой символ. \n Чтобы выиграть в игре – участник должен выиграть три маленьких поля, стоящих по горизонтали, по диагонали или по вертикали. \n Если в маленьком поле образовалась ничья, то это поле считается ничейным. \n Если участник вынужден ходить в уже заполненное маленькое поле, то он вправе выбрать любое другое из свободных полей. \n После выигрыша маленького поля – оно не выходит из игры, пока в нем есть свободные клетки. \n"
//                );
            Console.WriteLine("Enter coordinates of next step: <x> <y> \n 0 < x,y < 8");
        }

        public static void DisplayField(Cell[ ,] field, Cell[,] sectorsInfo)
        {
            Console.WriteLine("╔═══╦═══╦═══╗");            
            StringBuilder sb = new StringBuilder();
            for(int line = 0; line < 9; line++)
            {
                sb.Append("║");
                for( int position = 0; position < 9; position++)
                {
                    switch (field[line, position])
                    {
                        case Cell.X:
                            sb.Append(symbolX);
                            break;
                        case Cell.O:
                            sb.Append(symbolO);
                            break;
                        case Cell.Free:
                            sb.Append(symbolFree);
                            break;

                    }
                    if ((position + 1) % 3 == 0)
                    {
                        sb.Append("║");
                    }

                }

                Console.WriteLine(sb.ToString());
                sb.Clear();
                if ((line + 1) % 3 == 0 && (line + 1) != 9)
                {
                    Console.WriteLine("╠═══╬═══╬═══╣");
                }

            }
            Console.WriteLine("╚═══╩═══╩═══╝");

            Console.WriteLine("SECTORS INFO:");  
            Console.WriteLine("╔═╦═╦═╗");
            for (int i = 0; i < 3; i++)
            {
                sb.Append("║");
                for (int j = 0; j < 3; j++)
                {
                    switch (sectorsInfo[i, j])
                    {
                        case Cell.X:
                            sb.Append(symbolX);
                            break;
                        case Cell.O:
                            sb.Append(symbolO);
                            break;
                        case Cell.Free:
                            sb.Append(symbolFree);
                            break;
                        case Cell.Draw:
                            sb.Append(symbolDraw);
                            break;
                    }
                    sb.Append("║");
                }
                Console.WriteLine(sb.ToString());
                sb.Clear();
                if (i + 1 != 3)
                {
                    Console.WriteLine("╠═╬═╬═╣");
                }
            }
            Console.WriteLine("╚═╩═╩═╝");


        }

        public static void GetNextStep(out int x, out int y, Player currentPlayer)
        {
            while (true)
            {
                DisplayHelp();
                string str = Console.ReadLine();
                if (String.IsNullOrEmpty(str))
                {
                    continue;
                }

                str = str.Trim();

                string[] coordinates = str.Split(' ');
                if (coordinates.Length < 2)
                {
                    continue;
                }

                if (!int.TryParse(coordinates[0], out x))
                {
                    continue;
                }     
                if (!int.TryParse(coordinates[1], out y))
                {
                    continue;
                }

                if (x < 0 || y < 0
                    || x > 8 || y > 8)
                {
                    continue;
                }
                
                return;
            }

        }

        public static void DisplayError(string error)
        {
            Console.WriteLine(error);
        }

        public static void DisplayResult(Cell fieldStatus)
        {
            switch (fieldStatus)
            {
                case Cell.Draw:
                    Console.WriteLine("Draw!");
                    break;
                case Cell.X:
                    Console.WriteLine("PlayerX won!");
                    break;
                case Cell.O:
                    Console.WriteLine("PlayerO won!");
                    break;

            }
        }
    }
}