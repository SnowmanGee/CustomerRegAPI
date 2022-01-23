namespace BusinessLogic.Interfaces
{
    public interface IValidation
    {
        bool ValidateString(string name, int minLength, int maxLength);

        bool ValidatePolicyReference(string reference);

        bool ValidateAgePlusEighteen(DateTime dob);

        bool ValidateEMail(string address);
    }
}