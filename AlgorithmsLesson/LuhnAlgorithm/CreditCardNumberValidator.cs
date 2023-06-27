namespace LuhnAlgorithm;

public class CreditCardNumberValidator
{
    public bool Validate(string creditCardNumber)
    {
        if (creditCardNumber.Length != 16)
            return false;

        if (creditCardNumber[0] != '2' && // МИР
            creditCardNumber[0] != '3' && // American Express
            creditCardNumber[0] != '4' && // Visa
            creditCardNumber[0] != '5' && // MasterCard
            creditCardNumber[0] != '9') // ArCa
            return false;

        var sum = 0;
        for (var i = 0; i < 16; i++)
        {
            var digit = creditCardNumber[i] - '0';
            if (i % 2 == 0)
                digit *= 2;
            if (digit > 9)
                digit -= 9;
            sum += digit;
        }

        return sum % 10 == 0;
    }

    public int FindMissingNumber(string cardNumber)
    {
        var sum = 0;
        var isDoubled = false;
        for (var i = 0; i < 16; i++)
        {
            var digit = char.IsDigit(cardNumber[i]) ? cardNumber[i] - '0' : 0;
            
            if (i % 2 == 0)
            {
                if (!char.IsDigit(cardNumber[i]))
                    isDoubled = true;
                digit *= 2;
            }

            if (digit > 9)
                digit -= 9;
            
            sum += digit;
        }

        var missingNumber = 10 - sum % 10;
        if (isDoubled)
            missingNumber = (missingNumber + 9) / 2;
        return missingNumber;
    }
}