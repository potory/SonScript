using SonScript.Core.Attributes;
using SonScript.Core.Extensions;

namespace SonScript.Core.Functions;

[AllowFunctionCaching]
public sealed class OneOfFunction : Function
{
    public override object Evaluate(List<object> arguments) => 
        arguments.OneOf();
}