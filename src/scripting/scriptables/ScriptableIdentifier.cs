namespace Sandbox_Simulator_2024.src.scripting.scriptable;
using Identifier = string;

public class ScriptableIdentifier(Identifier name) : IScriptable
{
    public Identifier identifier { get; } = name;
}