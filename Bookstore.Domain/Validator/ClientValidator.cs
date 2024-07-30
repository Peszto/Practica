using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookStore.Domain.Validator
{
    public class ClientValidator
    {
        public bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsValidPhoneNr(string phoneNr)
        {
            //exactly 10 digits long
            string pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNr, pattern);
        }

    }
}
