namespace Sandbox_Simulator_2024.src.backend.network;

public class Packet : IName
{
    public const int DEFAULT_TLL = 256;

    public Action onDelivered = () => { };

    public string Name { get; set; } = "Nameless Packet";
    public string Source = string.Empty;
    public string LastHop = string.Empty;
    public string NextHop = string.Empty;
    public string Destination = string.Empty;
    public int TTL = DEFAULT_TLL;

    public bool traceRoute = false;

    public Packet() => SetTTL();

    public Packet(string name)
    {
        Name = name;
        SetTTL();
    }

    public Packet(string source, string destination)
    {
        Source = source;
        Destination = destination;
        LastHop = source;
        NextHop = source;
        SetTTL();
    }

    private void SetTTL() => TTL = Math.Max(DEFAULT_TLL, Network.GetNodeCount());

    public void Step()
    {
        if (traceRoute)
        {
            ForegroundColor = ConsoleColor.Magenta;
            WriteLine($"Trace Route: Packet {Name} is at {LastHop} and is going to {NextHop}");
            ResetColor();
        }

        if (this is not IEntity)
        {
            TTL = Math.Max(TTL - 1, 0);
        }
    }

    public void Deliver() => onDelivered();
}