using SonScript.Core.Attributes;

namespace SonScript.Core.Functions;

[AllowFunctionCaching]
public sealed class AppendFunction : Function
{
    public override object Evaluate(List<object> arguments) => 
        string.Join(string.Empty, arguments.Select(x => (string)x));
}