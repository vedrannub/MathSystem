using MathTestSystem.Application.Interfaces;

namespace MathTestSystem.Infrastructure.Services
{
    public class MathProcessor : IMathProcessor
    {
        public double EvaluateExpression(string expression)
        {
            try
            {
                var e = new NCalc.Expression(expression);
                var result = e.Evaluate();
                return Convert.ToDouble(result);
            }
            catch
            {
                return double.NaN;
            }
        }
    }
}
