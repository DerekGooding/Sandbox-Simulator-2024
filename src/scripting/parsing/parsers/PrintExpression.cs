namespace Sandbox_Simulator_2024.src.scripting.parsing.parsers;

public class PrintExpression : IParseStuff
{
    public ParseResult Parse(IEnumerable<Token> tokens, ScriptInterpreter scriptInterpreter)
    {
        foreach (var token in tokens)
        {
            BackgroundColor = ConsoleColor.Black;

            switch (token.Type)
            {
                case Token.TokenType.Delimiter:
                    break;

                case Token.TokenType.Keyword:
                    Print(token, ConsoleColor.DarkBlue);
                    break;

                case Token.TokenType.Identifier:
                    Print(token, ConsoleColor.DarkGreen);
                    break;

                case Token.TokenType.Operator:
                    Print(token, ConsoleColor.White);
                    break;

                case Token.TokenType.Literal:
                    Print(token, ConsoleColor.Yellow);
                    break;

                case Token.TokenType.String:
                    Print(token, ConsoleColor.Magenta);
                    break;

                case Token.TokenType.Comment:
                    Print(token, ConsoleColor.Gray);
                    break;

                case Token.TokenType.Whitespace:
                    Print(token, ConsoleColor.White);
                    break;

                case Token.TokenType.NewLine:
                    WriteLine();
                    break;

                case Token.TokenType.Ignored:
                    Print(token, ConsoleColor.DarkGray);
                    break;

                case Token.TokenType.Unknown:
                    Print("�", ConsoleColor.Red);
                    break;

                default:
                    Print($"�{token.Value}�", ConsoleColor.Red);
                    break;
            }
        }

        ResetColor();
        return new ParseResult(ParseResult.State.Success, "We printed all tokens :)");
    }

    private static void Print(Token token, ConsoleColor color) => Print(token.Value, color);

    private static void Print(string message, ConsoleColor color)
    {
        ForegroundColor = color;
        Write(message + " ");
    }
}