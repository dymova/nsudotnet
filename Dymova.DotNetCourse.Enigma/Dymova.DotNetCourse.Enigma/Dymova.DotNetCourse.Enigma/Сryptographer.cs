using System.Security.Cryptography;

namespace Dymova.DotNetCourse.Enigma
{
    public class Сryptographer
    {
        protected SymmetricAlgorithm GetSymmetricAlgorithm(string algName)
        {
            switch (algName)
            {
                case ArgParser.AesAlg:
                    return new AesCryptoServiceProvider();
                case ArgParser.DesAlg:
                    return new DESCryptoServiceProvider();
                case ArgParser.Rc2Alg:
                    return new RC2CryptoServiceProvider();
                case ArgParser.RijndaelAlg:
                    return new RijndaelManaged();
                default:
                    return null;
            }
        }
    }
}