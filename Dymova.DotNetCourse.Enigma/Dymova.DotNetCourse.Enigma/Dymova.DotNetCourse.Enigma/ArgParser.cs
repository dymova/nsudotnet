using System;
using System.Collections.Generic;
using System.IO;

namespace Dymova.DotNetCourse.Enigma
{
    public class ArgParser
    {
        public const string EncryptCommand = "encrypt";
        public const string DecryptCommand = "decrypt";
        public const string Rc2Alg = "rc2";
        public const string AesAlg = "aes";
        public const string DesAlg = "des";
        public const string RijndaelAlg = "rijndael";

        public static bool IsCorrect(string[] args)
        {
            switch (args[0])
            {
                case EncryptCommand:
                    if (args.Length != 4)
                    {
                        throw new ArgParserException("Not enough parameters.");
                    }
                    if (!File.Exists(args[1]))
                    {
                        throw new ArgParserException("File '" + args[1] + "doesn't exist.");
                    }
                    break;
                case DecryptCommand:
                    if (args.Length != 5)
                    {
                        throw new ArgParserException("Not enough parameters.");
                    }
                    if (!File.Exists(args[1]))
                    {
                        throw new ArgParserException("Binfile '" + args[1] + "doesn't exist.");
                    }

                    break;
                default:
                    throw new ArgParserException("Unknown command '" + args[0] +"'.");
            }

            string algName = args[2];
            if (algName != AesAlg && algName != DesAlg &&
                algName != Rc2Alg && algName != RijndaelAlg)
            {
                throw new ArgParserException("Unknown algorithm '" + args[3] + "'.");
            }


            return true;
        }
    }

    public class ArgParserException : Exception
    {
        public ArgParserException(string message) : base(message)
        {
        }
    }
}