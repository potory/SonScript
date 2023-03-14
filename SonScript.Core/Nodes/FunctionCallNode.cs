using SonScript.Core.Functions;

namespace SonScript.Core.Nodes;

public class FunctionCallNode : FunctionNode
{
    private readonly Function _function;
    private readonly IReadOnlyList<FunctionNode> _arguments;

    public static FunctionCallNode Empty(Function function) => new(function, Array.Empty<FunctionNode>());

    public FunctionCallNode(Function function, IReadOnlyList<FunctionNode> arguments)
    {
        _function = function;
        _arguments = arguments;
    }

    public override object Evaluate()
    {
        // Evaluate the arguments
        var evaluatedArgs = _arguments.Select(a => a.Evaluate()).ToList();

        // Call the function
        return _function.Evaluate(evaluatedArgs);
    }
}