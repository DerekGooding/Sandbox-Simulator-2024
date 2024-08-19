namespace Sandbox_Simulator_2024.src.scripting;
using Sandbox_Simulator_2024.src.scripting.parsing;
using Sandbox_Simulator_2024.src.scripting.scriptable;
using Identifier = string;

public class ScriptInterpreter
{
    public enum ScriptableType
    {
        List,
        Host,
        Packet,
        Router,
        Interface,
        Identifier
    }

    readonly List<Identifier> allIdentifiers = [];
    readonly List<IScriptable> scriptable = [];

    readonly Dictionary<Identifier, ScriptableInterface> interfaces = [];

    public ScriptInterpreter(string script)
    {
        Parser parser = new(this);
        ParseResult parseResult = parser.Parse(script);
        ResetColor();
        WriteLine();
        WriteLine(parseResult.Message);
    }

    public ParseResult RegisterIdentifier(ScriptableType scriptableType, Identifier identifier)
    {
        //>> Check if the identifier exists
        if (allIdentifiers.Contains(identifier))
        {
            ForegroundColor = ConsoleColor.Red;
            Write("(duplicate) ");
            return new ParseResult(ParseResult.State.Failure, $"Identifier already exists: {identifier}");
        }

        //>> Register the identifier
        allIdentifiers.Add(identifier);
        ForegroundColor = ConsoleColor.White;
        Write($"({identifier} registered to allIdentifiers) ");
        return new ParseResult(ParseResult.State.Success, $"Registered {scriptableType} with identifier: {identifier}");
    }

    public ParseResult RegisterInterface(Identifier identifier, ScriptableInterface scriptableInterface)
    {
        //>> Register the interface identifier, this also checks for duplicates
        ParseResult parseResult = RegisterIdentifier(ScriptableType.Interface, identifier);
        if (parseResult.state == ParseResult.State.Failure) return parseResult;

        //>> Register the interface
        interfaces.Add(identifier, scriptableInterface);
        Write($"({identifier} registered as interface) ");
        scriptable.Add(scriptableInterface);
        Write($"(scriptableInterface {scriptableInterface.identifier} registered) ");
        return new ParseResult(ParseResult.State.Success, "Registered interface");
    }
}