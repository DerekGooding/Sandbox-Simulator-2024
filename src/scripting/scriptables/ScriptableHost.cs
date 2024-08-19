namespace Sandbox_Simulator_2024.src.scripting.scriptable;
using Sandbox_Simulator_2024.src.backend.network;
using Identifier = string;

public class ScriptableHost(Identifier name, Identifier defaultGateway) : Host(name, defaultGateway), IScriptable
{
    public Identifier identifier { get; } = name;
}