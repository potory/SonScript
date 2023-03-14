using SonScript.Core.Attributes;

namespace SonScript.Core.Functions;

[AllowFunctionCaching]
public sealed class IntFunction : Function
{
    public override object Evaluate(List<object> arguments)
    {
        var argument = arguments[0];

        return argument switch
        {
            double d => (int) d,
            string s => int.Parse(s),
            _ => throw new ArgumentException($"The provided argument of type {argument.GetType()} cannot be converted to an integer value. Please provide a valid integer or double value as the first argument.")
        };
    }
}