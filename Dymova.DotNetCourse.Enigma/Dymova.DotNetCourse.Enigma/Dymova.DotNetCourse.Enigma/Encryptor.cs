using System;
using System.IO;
using System.Security.Cryptography;

namespace Dymova.DotNetCourse.Enigma
{
    public class Encryptor : Сryptographer
    {
        private readonly string _inputFile;
        private readonly string _algName;
        private readonly string _outputFile;
        private SymmetricAlgorithm _algorithm;

        public Encryptor(string[] args)
        {
            _inputFile = args[1];
            _algName = args[2];
            _outputFile = args[3];
        }

        public void Encrypt()
        {
           using (_algorithm = getSymmetricAlgorithm(_algName))
            {
                _algorithm.GenerateKey();
                _algorithm.GenerateIV();
                WriteKeyAndIv();

                var encryptor = _algorithm.CreateEncryptor();
                using (FileStream inStream = new FileStream(_inputFile, FileMode.Open))
                {
                    using (FileStream outStream = new FileStream(_outputFile, FileMode.OpenOrCreate))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(outStream, encryptor, CryptoStreamMode.Write))
                        {
                            inStream.CopyTo(cryptoStream);
                        }
                    }
                }
            }
        }

        private void WriteKeyAndIv()
        {
            string outDirectory = Path.GetDirectoryName(Path.GetFullPath(_inputFile));
            string keyFileName = string.Concat(Path.GetFileNameWithoutExtension(_inputFile), ".key.txt");
            string keyFilePath = string.Concat(outDirectory, Path.DirectorySeparatorChar, keyFileName);

            using (var sw = new StreamWriter(keyFilePath))
            {
                sw.WriteLine(Convert.ToBase64String(_algorithm.Key));
                sw.WriteLine(Convert.ToBase64String(_algorithm.IV));
            }
        }
    }
}