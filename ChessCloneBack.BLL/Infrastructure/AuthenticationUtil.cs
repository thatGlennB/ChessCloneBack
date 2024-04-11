using ChessCloneBack.DAL.Repositories;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace ChessCloneBack.BLL.Infrastructure
{
    public static class AuthenticationUtil
    {
        private static int _keySize = 64;
        private static int _iterations = 350000;
        private static HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
        
        public static bool IsValid(byte[] saltedHash, string password)
        {
            if (saltedHash.Length < _keySize * 2)
            {
                throw new ArgumentException($"Invalid salted hash length: record's salted hash is too short to be evaluated", nameof(saltedHash));
            }

            byte[] salt = saltedHash.Skip(_keySize).Take(_keySize).ToArray();
            byte[] hash = saltedHash.Take(_keySize).ToArray();
            byte[] input = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize);
            return CryptographicOperations.FixedTimeEquals(input, hash);
        }
        public static byte[] GetPasswordHash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_keySize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize);
            byte[] saltedHash = new byte[2 * _keySize];
            Buffer.BlockCopy(hash, 0, saltedHash, 0, _keySize);
            Buffer.BlockCopy(salt, 0, saltedHash, _keySize, _keySize);
            return saltedHash;
        }
    }
}
