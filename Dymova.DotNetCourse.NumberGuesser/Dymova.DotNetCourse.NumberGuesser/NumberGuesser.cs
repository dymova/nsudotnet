using System;

namespace Dymova.DotNetCourse.NumberGuesser
{
    public class NumberGuesser
    {
        private readonly string[] _insults =
        {
            "{0}, you are idiot!",
            "{0}, you are dummie",
            "{0}, I can’t goddamn believe it!",
            "{0}, what the hell is that?",
            "{0}, are you nuts?"
        };

        private int[] _attempts = new int[1000];

        public void Start()
        {
            Console.WriteLine("Please, enter your name:");
            string name = Console.ReadLine();

            Random random = new Random();
            int number = random.Next(101);

            Console.WriteLine("Try to guess the number:");

            DateTime dateTime = DateTime.Now;
            int attemptCount = 0;
            while (true)
            {
                string inputValue = Console.ReadLine();

                if (inputValue == "q")
                {
                    Console.WriteLine("I'm sorry");
                    return;
                }

                int attempt;
                if (String.IsNullOrEmpty(inputValue) || !int.TryParse(inputValue, out attempt))
                {
                    Console.WriteLine("Try to guess the NUMBER:");
                    continue;
                }

                attemptCount++;
                if (attempt != number)
                {
                    Console.WriteLine(attempt < number ? "Your number is less" : "Your number is greater");

                    if (attemptCount%4 == 0)
                    {
                        int randomIndex = random.Next(_insults.Length);
                        Console.WriteLine(String.Format(_insults[randomIndex], name));
                    }

                    if (attempt < _attempts.Length)
                    {
                        _attempts[attemptCount] = attempt;
                    }
                }
                else
                {
                    TimeSpan timeSpan = DateTime.Now - dateTime;
                    Console.WriteLine("You are right!");
                    Console.WriteLine(String.Format("The number of attampts: {0}", attemptCount));

                    
                    for (int i = 0; i < attemptCount; i++)
                    {
                        Console.WriteLine(String.Format("{0} {1}", _attempts[i], (_attempts[i] < number) ? "<" : ">"));
                    }

                    Console.WriteLine(String.Format("Time: {0} m ", timeSpan.TotalMinutes));
                    return;
                }

            }
        }

        private void PrintResults()
        {
 
            
        }
    }
}