using System;
using System.Text.RegularExpressions;

namespace Library
{
    public class clsValidation
    {
    
        /// <summary>
        /// This method checks wether a specific string is in an appropriate email format.
        /// </summary>
        /// <param name="Email">The string to be checked if it is in email format.</param>
        /// <returns>Wether the string is in an email format.</returns>
        public static bool ValidateEmail(string Email)
        {
            string pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(Email);
        }
    }
}
