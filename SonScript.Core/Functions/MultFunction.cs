using SonScript.Core.Attributes;

namespace SonScript.Core.Functions;

[AllowFunctionCaching]
public sealed class MultFunction : Function
{
    public override object Evaluate(List<object> arguments)
    {
        double result = GetDouble(arguments[0]);

        for (int i = 1; i < arguments.Count; i++)
        {
            var mult = GetDouble(arguments[i]);
            result *= mult;
        }

        return result;
    }
}