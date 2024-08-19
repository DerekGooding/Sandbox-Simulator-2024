namespace Sandbox_Simulator_2024.src.scripting.scriptables;
using Sandbox_Simulator_2024.src.backend.network;
using Identifier = string;

public class ScriptablePacket : Packet, IScriptable
{
    public Identifier identifier { get; private set; }

    public ScriptablePacket(Identifier name) : base(name)
    {
        identifier = name;
    }
}