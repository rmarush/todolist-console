﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace todolist_console.Models
{
    public class MailKey
    {
        public string Email {get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public MailKey() { }
        public MailKey(string email, string hashedPassword)
        {
            Email = email;
            Salt = GenerateSalt();
            HashedPassword = HashPassword(hashedPassword, Salt);
        }
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
        public bool VerifyPassword(string password)
        {
            string hashedInput = HashPassword(password, Salt);
            return HashedPassword.Equals(hashedInput);
        }
    }
}
