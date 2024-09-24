using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Backend_Teamwork.src.Controllers
{
    public class PasswordUtils
    {

        // method
        // convert plain password => hashed

        // salt = key
        public static string HashPassword(string originalPassword, out string hashedPassword, out byte[] salt)
        {
            // logic
            var hmac = new HMACSHA256();
            salt = hmac.Key;

            // string: 123
            // hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword))
            // array of bytes: Encoding.UTF8.GetBytes
            // convert from [bytes] => string
            // [byte1, byte2 , byte3]
            // 4F-3A-85
            // - - - - - 


            hashedPassword = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));

            return hashedPassword;
            // change return type of method => string

        }

        // method
        // compare plain password vs hashed 
        // bool
        // comparePassword
        public static bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt)
        {
            var hmac = new HMACSHA256(salt);
            // convert original to hashed
            // compare hashed to hashedPassword
            // 123 vs in database


            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword))) == hashedPassword;
        }
    }
}


// new PasswordUtils 

