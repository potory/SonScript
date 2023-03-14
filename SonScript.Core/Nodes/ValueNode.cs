namespace SonScript.Core.Nodes;

public class ValueNode : FunctionNode
{
    public string Value { get; }

    public ValueNode(string value) => 
        Value = value;

    public override object Evaluate() => 
        Value;
}