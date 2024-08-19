namespace Sandbox_Simulator_2024.src.scripting.parsing;

using Sandbox_Simulator_2024.Scripting.Parsing.Parsers;
using Sandbox_Simulator_2024.src.scripting;

public class Parser
{
    private static readonly Token.TokenType[] tokensToSkip = new Token.TokenType[] {
        Token.TokenType.NewLine,
        Token.TokenType.Comment,
        Token.TokenType.Whitespace,
        Token.TokenType.Ignored
    };

    private static readonly List<IParseStuff> chainOfResponsibility = [
        new PrintExpression(),
        new ValidateExpression(),
        new DefineExpression(),
        new InterfaceExpression(),
    ];

    private ScriptInterpreter ScriptInterpreter;

    public Parser(ScriptInterpreter scriptInterpreter)
    {
        ScriptInterpreter = scriptInterpreter;
    }

    public ParseResult Parse(string script)
    {
        var tokens = Tokenizer.Tokenize(script);
        ResetColor();
        WriteLine("Parser found " + tokens.Count + " tokens");
        ParseResult currentResult = new ParseResult(ParseResult.State.Default, "");

        IterateExpressions(tokens, (expression) =>
        {
            foreach (var parser in chainOfResponsibility)
            {
                currentResult = parser.Parse(expression, ScriptInterpreter);
                if (currentResult.state == ParseResult.State.Failure) return false;
                //else if (currentResult.state == ParseResult.State.Skip) continue;
            }
            WriteLine();
            return true;
        });
        return new ParseResult(ParseResult.State.Success, "Parsing successful");
    }

    public bool IterateExpressions(IEnumerable<Token> tokens, Func<List<Token>, bool> Parse)
    {
        var lineTokens = new List<Token>();
        foreach (var token in tokens)
        {
            if (token.Type == Token.TokenType.Delimiter)
            {
                if (!Parse(lineTokens)) return false;
                lineTokens.Clear();
            }
            else if (tokensToSkip.Contains(token.Type)) continue;
            else lineTokens.Add(token);
        }
        return true;
    }
}