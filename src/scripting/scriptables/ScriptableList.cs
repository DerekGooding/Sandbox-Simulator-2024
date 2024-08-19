namespace Sandbox_Simulator_2024.src.scripting.scriptable;
using Identifier = string;

public class ScriptableList(Identifier name) : List<IScriptable>(), IScriptable
{
    public Identifier identifier { get; } = name;
}