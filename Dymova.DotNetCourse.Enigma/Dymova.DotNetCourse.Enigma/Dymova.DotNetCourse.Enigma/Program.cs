using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dymova.DotNetCourse.Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (ArgParser.IsCorrect(args))
                {
                    switch (args[0])
                    {
                        case ArgParser.DecryptCommand:
                            new Decryptor(args).Decrypt();
                            break;
                        case ArgParser.EncryptCommand:
                            new Encryptor(args).Encrypt();
                            break;
                    }                    
                }

            }
            catch (ArgParserException e)
            {
                Console.WriteLine(e.Message);
                PrintHelp();
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Use one of these templates to start the crypto.exe:");
            Console.WriteLine("To encrypt: crypto.exe encrypt file.txt rc2 output.bin");
            Console.WriteLine("To decrypt: crypto.exe decrypt output.bin rc2 file.key.txt file.txt");
        }

    }
}
