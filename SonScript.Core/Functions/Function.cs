using System.Globalization;

namespace SonScript.Core.Functions;

public abstract class Function
{
    public abstract object Evaluate(List<object> arguments);

    protected static double GetDouble(object obj)
    {
        if (obj is double result)
        {
            return result;
        }
        
        if (!double.TryParse(obj.ToString(), CultureInfo.InvariantCulture, out result))
        {
            throw new ArgumentException($"The provided object of type {obj.GetType()} is not a valid double or cannot be converted to a double using the InvariantCulture.");
        }

        return result;
    }
}