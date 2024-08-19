namespace Sandbox_Simulator_2024.src.scripting.scriptables;
using Sandbox_Simulator_2024.src.backend.network;
using Identifier = string;

public class ScriptableHost : Host, IScriptable
{
    public Identifier identifier { get; private set; }

    public ScriptableHost(Identifier name, Identifier defaultGateway) : base(name, defaultGateway)
    {
        identifier = name;
    }
}