namespace Sandbox_Simulator_2024.src.scripting.scriptables;
using Sandbox_Simulator_2024.src.backend.network;
using Identifier = string;

public class ScriptableRouter : Router, IScriptable
{
    public Identifier identifier { get; private set; }

    public ScriptableRouter(Identifier name) : base(name)
    {
        identifier = name;
    }
}