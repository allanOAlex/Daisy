using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Daisy.Shared.Extensions
{
    public static class ModelValidationsExtension
    {
        public static string valid = "Valid";
        public static string PasswordErrorMessage = "\n" +
        "Must have a minimum of 8 characters in length \n " +
        "Must contain at least one uppercase English letter \n" +
        "Must contain at least one lowercase English letter \n" +
        "Must contain at least one digit \n" +
        "Must contain at least one special character \n" +
        "";
        public static string ValidateUsername(string username) 
        {
            try
            {
                if (!username.Contains("admin"))
                {
                    return valid;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message.ToString());
                throw new Exception($"Error Validating Dates | {ex.Message}");
            }

        }
        public static bool ValidatePassword() { 
            return true;
        }
        public static string ValidateEmail(string email) 
        {

            try
            {
                string pattern = @"/^\d$/";
                Regex regex = new Regex(pattern);
                if (email == null)
                {
                    return "Email is required";
                }
                if (!regex.IsMatch(email))
                {
                    return "Please provide a valid email address";
                }

                return valid;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while validating phone number. | {ex.Message}");
            }
        }
        public static bool ValidatePasswordEmail() { 
            
            return true; 
        }
        public static string ValidateNames(string name) 
        {
            try
            {
                string pattern = @"^[a-zA-Z]{3,}$";
                Regex regex = new Regex(pattern);
                if (name == null)
                {
                    return "First name is required";
                }
                if (!regex.IsMatch(name))
                {
                    return "First name must contain only a mininum of 3 and a maximun 0f 20 alaphabetical letters";
                }

                return valid;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while validating names. | {ex.Message}");
            }
        }
        public static int ValidateDates(DateTime? start, DateTime? time, DateTime? end) 
        {
            try
            {
                if (start == null) { return 1; }
                if (time == null) { return 2; }
                if (end == null) { return 3; }
                if (start < DateTime.Today.AddDays(0)) { return -1;  }
                if (end < DateTime.Today.AddDays(0)) { return -3;  }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception($"Error Validating Dates | {ex.Message}");
            }
            
        }

        public static string ValidatePhone(string phone) 
        {
            try
            {
                //string pattern = @"^\d{10,10}$"; //^[0-9]{10}$
                string pattern = @"/^\d$/"; //^[0-9]{10}$
                Regex regex = new Regex(pattern);
                if (phone == null)
                {
                    return "First name is required";
                }
                if (!regex.IsMatch(phone)) 
                {
                    return "Phone number must contain only numbers";
                }

                return valid;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while validating phone number. | {ex.Message}");
            }
        }
    }
}
