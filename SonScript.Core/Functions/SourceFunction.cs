using SonScript.Core.Attributes;

namespace SonScript.Core.Functions;

[AllowFunctionCaching]
public sealed class SourceFunction : Function
{
    private readonly FunctionContext _context;

    public SourceFunction(FunctionContext context) => 
        _context = context;

    public override object Evaluate(List<object> arguments) => 
        _context.Source;
}