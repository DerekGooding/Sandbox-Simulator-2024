namespace Sandbox_Simulator_2024.src.scripting.parsing;

using Sandbox_Simulator_2024.src.scripting;
using Sandbox_Simulator_2024.src.scripting.parsing.parsers;

public class Parser(ScriptInterpreter scriptInterpreter)
{
    private static readonly Token.TokenType[] tokensToSkip = [
        Token.TokenType.NewLine,
        Token.TokenType.Comment,
        Token.TokenType.Whitespace,
        Token.TokenType.Ignored
    ];

    private static readonly List<IParseStuff> chainOfResponsibility = [
        new PrintExpression(),
        new ValidateExpression(),
        new DefineExpression(),
        new InterfaceExpression(),
    ];

    private readonly ScriptInterpreter ScriptInterpreter = scriptInterpreter;

    public ParseResult Parse(string script)
    {
        var tokens = Tokenizer.Tokenize(script);
        ResetColor();
        WriteLine("Parser found " + tokens.Count + " tokens");
        ParseResult currentResult = new(ParseResult.State.Default, "");

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

    public static bool IterateExpressions(IEnumerable<Token> tokens, Func<List<Token>, bool> Parse)
    {
        var lineTokens = new List<Token>();
        foreach (var token in tokens)
        {
            if (token.Type == Token.TokenType.Delimiter)
            {
                if (!Parse(lineTokens)) return false;
                lineTokens.Clear();
            }
            else if (!tokensToSkip.Contains(token.Type))
            {
                lineTokens.Add(token);
            }
        }
        return true;
    }
}