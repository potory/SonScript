using Microsoft.Extensions.DependencyInjection;
using SonScript.Core;
using SonScript.Core.Nodes;

namespace Sonscript.Startup;

internal static class Program
{
    private static void Main()
    {
        string input = "#mult(5,10) and #mult(12, 50)";

        var separator = new ExpressionSeparator();

        var serviceCollection = new ServiceCollection()
            .AddSingleton(new FunctionContext());
        
        var functionFactory = new FunctionFactory(serviceCollection.BuildServiceProvider());
        var functionParser = new FunctionParser(functionFactory);

        var segments = separator.Separate(input);
        var nodes = new List<FunctionNode>();
        
        foreach (var segment in segments)
        {
            nodes.Add(functionParser.Parse(segment));
        }

        foreach (var node in nodes)
        {
            Console.Write(node.Evaluate());
        }

        Console.ReadKey();
    }
}