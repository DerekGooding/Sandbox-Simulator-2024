namespace Sandbox_Simulator_2024.src.scripting.parsing.parsers;

using Sandbox_Simulator_2024.src.scripting;
using Sandbox_Simulator_2024.src.scripting.parsing;
using System.Collections.Generic;

public class DefineExpression : IParseStuff
{
    public ParseResult Parse(IEnumerable<Token> tokens, ScriptInterpreter scriptInterpreter)
    {
        //>> Check counts
        if (tokens.Count() != 3) return new ParseResult(ParseResult.State.Skip, "");

        //>> Pull tokens
        Token firstToken = tokens.First();
        Token secondToken = tokens.Skip(1).First();
        Token thirdToken = tokens.Skip(2).First();

        //>> Check types
        if (firstToken.Type != Token.TokenType.Identifier
            &&
            secondToken.Type != Token.TokenType.Keyword
            &&
            (thirdToken.Type != Token.TokenType.Keyword || thirdToken.Type != Token.TokenType.Identifier)
            )
        {
            return new ParseResult(ParseResult.State.Skip, "");
        }

        //>> Check second token
        if (secondToken.Value != "is") return new ParseResult(ParseResult.State.Skip, "Expected 'is' keyword", (tokens, secondToken));

        //>> Evaluate
        // Not interfaces have their own IParseStuff
        ParseResult? result = ThirdToken(thirdToken.Value, scriptInterpreter, firstToken);
        if (result != null)
            return result;

        //>> Special case for derived identifiers
        if (thirdToken.Type == Token.TokenType.Identifier)
            return scriptInterpreter.RegisterIdentifier(ScriptInterpreter.ScriptableType.Identifier, firstToken.Value);

        return new ParseResult(ParseResult.State.Success, $"Successfully defined {firstToken.Value} as a {thirdToken.Value}");
    }

    private static ParseResult? ThirdToken(string tokenValue, ScriptInterpreter scriptInterpreter, Token firstToken)
    {
        return tokenValue switch
        {
            "router" => scriptInterpreter.RegisterIdentifier(ScriptInterpreter.ScriptableType.Router, firstToken.Value),
            "host" => scriptInterpreter.RegisterIdentifier(ScriptInterpreter.ScriptableType.Host, firstToken.Value),
            "list" => scriptInterpreter.RegisterIdentifier(ScriptInterpreter.ScriptableType.List, firstToken.Value),
            "packet" => scriptInterpreter.RegisterIdentifier(ScriptInterpreter.ScriptableType.Packet, firstToken.Value),
            _ => null,
        };
    }
}