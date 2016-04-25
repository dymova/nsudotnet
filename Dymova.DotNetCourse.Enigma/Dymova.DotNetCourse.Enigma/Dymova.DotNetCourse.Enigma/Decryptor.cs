using System;
using System.IO;
using System.Security.Cryptography;

namespace Dymova.DotNetCourse.Enigma
{
    public class Decryptor : Сryptographer
    {
        private readonly string _inputFile;
        private readonly string _algName;
        private readonly string _keyFile;
        private readonly string _outputFile;
        private SymmetricAlgorithm _algorithm;

        public Decryptor(string[] args)
        {
            _inputFile = args[1];
            _algName = args[2];
            _keyFile = args[3];
            _outputFile = args[4];
        }

        public void Decrypt()
        {
            using (_algorithm = getSymmetricAlgorithm(_algName))
            {
                ReadKeyAndIv();
                var decryptor = _algorithm.CreateDecryptor();
                using (FileStream inStream = new FileStream(_inputFile, FileMode.Open))
                {
                    using (FileStream outStream = new FileStream(_outputFile, FileMode.OpenOrCreate))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(inStream, decryptor, CryptoStreamMode.Read))
                        {
                            cryptoStream.CopyTo(outStream);
                        }
                    }
                }
            }
        }

        private void ReadKeyAndIv()
        {
            using (var sr = new StreamReader(_keyFile))
            {
                _algorithm.Key = Convert.FromBase64String(sr.ReadLine());
                _algorithm.IV = Convert.FromBase64String(sr.ReadLine());
                
            }
        }
    }
}