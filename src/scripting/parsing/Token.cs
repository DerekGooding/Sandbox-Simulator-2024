namespace Sandbox_Simulator_2024.src.scripting.parsing;

public class Token(string value, Token.TokenType type, int lineNumber)
{
    public enum TokenType
    {
        Keyword,
        Identifier,
        Operator,
        Literal,
        String,
        Comment,
        Whitespace,
        NewLine,
        Delimiter,
        Unknown,
        Ignored
    }

    public string Value { get; set; } = value;
    public TokenType Type { get; set; } = type;
    public int SourceLineNumber { get; set; } = lineNumber;
}