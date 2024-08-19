namespace Sandbox_Simulator_2024.src.scripting.parsing;

public class ParseResult
{
    public enum State
    {
        Success,
        Skip,
        Failure,
        Default
    }

    public State state { get; }
    public string Message { get; }

    public ParseResult(State state, string message)
    {
        this.state = state;
        Message = message;
    }

    public ParseResult(State state, string message, (IEnumerable<Token>, Token?) effected)
    {
        //>> Setup the method
        this.state = state;
        Message = message;
        var expression = effected.Item1;
        var effectedToken = effected.Item2;

        if (state == State.Skip) return;

        //>> Print the error message line
        int count = expression.Count();
        ForegroundColor = ConsoleColor.Red;
        if (count >= 1)
        {
            Token firstToken = expression.First();
            Token? lastToken = null;
            if (count >= 2) lastToken = expression.Last();

            string errorMessage = count == 1 || firstToken.SourceLineNumber == lastToken!.SourceLineNumber
                ? $"Error on line: {firstToken.SourceLineNumber}"
                : $"Error on lines: {firstToken.SourceLineNumber} to {lastToken.SourceLineNumber}";

            WriteLine(errorMessage);
        }

        //>> Print the error message
        ForegroundColor = ConsoleColor.DarkRed;
        WriteLine(Message);

        //>> Print the effected expression and/or token
        foreach (var token in expression)
        {
            ForegroundColor = ConsoleColor.DarkYellow;
            if (effectedToken != null && token == effectedToken)
                ForegroundColor = ConsoleColor.DarkRed;
            Write(token.Value + " ");
        }
        WriteLine();

        //>> House cleaning
        ResetColor();
    }
}