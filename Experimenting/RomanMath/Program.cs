using RomanMath;

var calculator = new RomanCalculator();
var expression = "(IV+V)*X-V";//"(MMMDCCXXIV - MMCCXXIX) * II";
var result = calculator.Evaluate(expression);
Console.WriteLine($"{expression} = {result}");