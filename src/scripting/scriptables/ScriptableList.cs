namespace Sandbox_Simulator_2024.src.scripting.scriptables;
using Identifier = string;

public class ScriptableList : List<IScriptable>, IScriptable
{
    public Identifier identifier { get; private set; }

    public ScriptableList(Identifier name) : base()
    {
        identifier = name;
    }
}