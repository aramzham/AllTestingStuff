using System.Diagnostics;
using LuhnAlgorithm;

var creditCardChecker = new CreditCardNumberValidator();
var isValid = creditCardChecker.Validate("5555555555554444");
Debug.Assert(isValid == true);
var missingNumber = creditCardChecker.FindMissingNumber("55555555555*4444");
Debug.Assert(missingNumber == 5);