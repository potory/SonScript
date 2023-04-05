using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SonScript.Core.Attributes;
using SonScript.Core.Functions;

namespace SonScript.Core;

public class FunctionFactory
{
    private readonly IServiceProvider _provider;
    private readonly Dictionary<string, Type> _register = new();

    private readonly Dictionary<Type, Function> _cache = new();

    public FunctionFactory(IServiceProvider provider) => 
        _provider = provider;

    public void RegisterFunction<T>(string name) where T : Function => 
        _register.Add(name.ToLower(), typeof(T));

    public Function CreateFunction(string functionName)
    {
        var callName = functionName.ToLower();

        if (_register.TryGetValue(callName, out var type))
        {
            return CreateFunction(type);
        }

        return callName switch
        {
            "source" => CreateFunction<SourceFunction>(),
            "mult" => CreateFunction<MultFunction>(),
            "multiply" => CreateFunction<MultFunction>(),
            "eight" => CreateFunction<EightFunction>(),
            "int" => CreateFunction<IntFunction>(),
            "replace" => CreateFunction<ReplaceFunction>(),
            "linefrom" => CreateFunction<LineFromFunction>(),
            "oneof" => CreateFunction<OneOfFunction>(),
            "append" => CreateFunction<AppendFunction>(),
            _ => throw new ArgumentException($"Unknown function '{functionName}'")
        };
    }

    public void ClearCache() => _cache.Clear();

    private Function CreateFunction<T>() where T: Function => 
        CreateFunction(typeof(T));

    private Function CreateFunction(Type type)
    {
        var isCachingAllowed = IsCachingAllowed(type);

        if (!isCachingAllowed)
        {
            return CreateFunctionUsingProvider(type);
        }

        if (_cache.TryGetValue(type, out var function))
        {
            return function;
        }

        function = CreateFunctionUsingProvider(type);
        _cache.Add(type, function);
        return function;
    }

    private Function CreateFunctionUsingProvider(Type type) => 
        (Function)ActivatorUtilities.CreateInstance(_provider, type);

    private static bool IsCachingAllowed(Type type) => 
        type.GetCustomAttribute<AllowFunctionCachingAttribute>() != null;
}