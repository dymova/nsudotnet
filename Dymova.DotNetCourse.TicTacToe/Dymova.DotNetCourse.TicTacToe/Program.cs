using System;
using System.Collections.Generic;

namespace Dymova.DotNetCourse.TicTacToe
{
  class Program
  {
    static void Main(string[] args)
    {
        Game game = new Game();
        IUserInterface userInterface = new ConsoleInterface(game);
        userInterface.Run();

        Console.ReadKey();
    }
  }
}


