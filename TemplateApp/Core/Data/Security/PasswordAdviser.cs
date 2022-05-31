using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TemplateApp.Core.Data.Security
{

    public static class PasswordAdvisor
    {
        public static int CheckStrength(string password)
        {

            var strength = 1;

            if (password.Length > 10)
                strength++;
            
            // Password contains uppercase letters.
            if (password.Any(c => char.IsUpper(c)))
                strength++;


            // Password contains numbers.
            if (password.Any(c => char.IsDigit(c)))
                strength++;


            // Password contains symbols.
            if (password.IndexOfAny("\\|¬¦`!\"£$%^&*()_+-=[]{};:'@#~<>,./? ".ToCharArray()) >= 0)
                strength++;


            return strength;

        }
    }
}

