using BusinessLogic.Interfaces;
using System.Text.RegularExpressions;

namespace BusinessLogic.Helpers
{
    ///<inheritdoc cref="IValidation"/>
    public class Validation : IValidation
    {
        public bool ValidateString(string name, int minLength, int maxLength)
        {
            try
            {
                return (!string.IsNullOrEmpty(name) || (name.Length >= minLength & name.Length <= maxLength));
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public bool ValidatePolicyReference(string reference)
        {
            return Regex.IsMatch(reference, "^[A-Z]{2}[-][0-9]{6}$");
        }
        public bool ValidateAgePlusEighteen(DateTime dob)
        {
            var today = DateTime.Now;
            var validDate = new DateTime(today.Year - 18, today.Month, today.Day);
            TimeSpan validAge = today.Subtract(validDate);
            TimeSpan actualAge = today.Subtract(dob);
            return TimeSpan.Compare(validAge, actualAge) < 1;
        }
        public bool ValidateEMail(string address)
        {
            try
            {
                // Regex example from EmailCore.Validation nuget.
                var expression = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|" +
                                 "(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
                var test = (address.EndsWith(".com") || address.EndsWith(".co.uk"));
                var test2 = new Regex(expression).IsMatch(address);
                return new Regex(expression).IsMatch(address)
                       && (address.EndsWith(".com") || address.EndsWith(".co.uk"));
            }
            catch
            {
                return false;
            }
        }
    }
}