namespace Sandbox_Simulator_2024.src.scripting.scriptable;
using Sandbox_Simulator_2024.src.backend.network;
using Identifier = string;

public class ScriptablePacket(Identifier name) : Packet(name), IScriptable
{
    public Identifier identifier { get; } = name;
}