// ---------------------------------------------------
// <copyright file="PasswordUtils.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Wini.Utils
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// tiện ích password.
    /// </summary>
    public class PasswordUtils
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;

        /// <summary>
        /// mã hóa password.
        /// </summary>
        /// <param name="password">password.</param>
        /// <returns>password hash.</returns>
        public static string HashPassword(string password)
        {
            // http://stackoverflow.com/questions/19957176/asp-net-identity-password-hashing
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HasingIterationsCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(HashByteSize);
            }

            byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
            Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
            return Convert.ToBase64String(dst);
        }

        /// <summary>
        /// giải mã passoword.
        /// </summary>
        /// <param name="hashedPassword">password hash.</param>
        /// <param name="password">password.</param>
        /// <returns>trạng thái có đúng password không.</returns>
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] passwordHashBytes;

            int arrayLen = (SaltByteSize + HashByteSize) + 1;

            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != arrayLen) || (src[0] != 0))
            {
                return false;
            }

            byte[] currentSaltBytes = new byte[SaltByteSize];
            Buffer.BlockCopy(src, 1, currentSaltBytes, 0, SaltByteSize);

            byte[] currentHashBytes = new byte[HashByteSize];
            Buffer.BlockCopy(src, SaltByteSize + 1, currentHashBytes, 0, HashByteSize);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, currentSaltBytes, HasingIterationsCount))
            {
                passwordHashBytes = bytes.GetBytes(SaltByteSize);
            }

            return AreHashesEqual(currentHashBytes, passwordHashBytes);
        }

        /// <summary>
        /// so sánh.
        /// </summary>
        /// <param name="firstHash">firstHash.</param>
        /// <param name="secondHash">secondHash.</param>
        /// <returns>bool.</returns>
        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }

    }
}
