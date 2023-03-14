using Microsoft.Extensions.DependencyInjection;
using SonScript.Core;
using SonScript.Core.Nodes;

namespace Sonscript.Startup;

internal static class Program
{
    private static void Main()
    {
        // The input text string contains two function calls to the 'mult' function and a text message.
        string input = "#mult(5,10) and #mult(12, 50) is a numbers";

        // An instance of ExpressionSeparator is created to split the input string into segments.
        var separator = new ExpressionSeparator();

        var serviceCollection = new ServiceCollection()
            .AddSingleton(new FunctionContext());

        var functionFactory = new FunctionFactory(serviceCollection.BuildServiceProvider());
        var functionParser = new FunctionParser(functionFactory);

        var segments = separator.Separate(input);
        var nodes = new List<FunctionNode>();

        // Iterate through the segments and create a FunctionNode instance for each segment using the FunctionParser instance.
        foreach (var segment in segments)
        {
            nodes.Add(functionParser.Parse(segment));
        }

        // Iterate through the FunctionNode instances and evaluate each one, writing the result to the console.
        foreach (var node in nodes)
        {
            Console.Write(node.Evaluate());
        }

        // In the console, the output should be '50 and 600 is a numbers'.
    }
}