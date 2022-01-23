namespace BusinessLogic.Interfaces
{
    public interface IValidation
    {
        /// <summary>
        /// Takes a string and checks it is not null, and it has more chars than the min and max values passed into
        /// the method.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <returns>Boolean true if validation passes.</returns>
        bool ValidateString(string name, int minLength, int maxLength);

        /// <summary>
        /// Takes a string and checks that it has 2 alpha chars, a dash, and 6 numeric values within it.
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        bool ValidatePolicyReference(string reference);

        /// <summary>
        /// Takes a date of birth and checks that it is currently above the age of 18.
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        bool ValidateAgePlusEighteen(DateTime dob);

        /// <summary>
        /// Uses a EmailCore.Validation library to check the structure of a string is and email, and also validates
        /// the domain of the address, to verify it is either .com or .co.uk.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool ValidateEMail(string address);
    }
}