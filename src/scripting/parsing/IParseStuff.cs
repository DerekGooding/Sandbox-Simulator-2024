namespace Sandbox_Simulator_2024.src.scripting.parsing;

public interface IParseStuff
{
    ParseResult Parse(IEnumerable<Token> tokens, ScriptInterpreter scriptInterpreter);
}