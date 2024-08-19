namespace Sandbox_Simulator_2024.src.scripting.scriptable;
using Sandbox_Simulator_2024.src.backend.network;
using Identifier = string;

public class ScriptableRouter(Identifier name) : Router(name), IScriptable
{
    public Identifier identifier { get; } = name;
}