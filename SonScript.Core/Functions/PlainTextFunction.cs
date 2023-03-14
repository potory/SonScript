using SonScript.Core.Attributes;

namespace SonScript.Core.Functions;

[AllowFunctionCaching]
public sealed class PlainTextFunction : Function
{
    private readonly string _text;

    public PlainTextFunction(string text) => 
        _text = text;

    public override object Evaluate(List<object> arguments) => 
        _text;
}