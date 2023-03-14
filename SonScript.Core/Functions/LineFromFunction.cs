using SonScript.Core.Attributes;
using SonScript.Core.Extensions;

namespace SonScript.Core.Functions;

[AllowFunctionCaching]
public sealed class LineFromFunction : Function
{
    public override object Evaluate(List<object> arguments)
    {
        var arg = arguments.SingleOrDefault();

        if (arg is not string path)
        {
            throw new ArgumentException("The provided argument is not a valid file path. Please provide a valid string argument that represents the path to a file to be read.");
        }

        var lines = File.ReadAllLines(path);
        return lines.OneOf();
    }
}