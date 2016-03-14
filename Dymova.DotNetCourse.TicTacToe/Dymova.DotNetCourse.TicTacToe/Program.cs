using System;
using System.Collections.Generic;

namespace Dymova.DotNetCourse.TicTacToe
{
  class Program
  {
    static void Main(string[] args)
    {
        Controller controller = new Controller();
        controller.Run();
        Console.ReadKey();
    }
  }
}


