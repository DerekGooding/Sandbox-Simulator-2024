using System.Collections.Concurrent;

namespace Sandbox_Simulator_2024.src.backend.network;

public class Router : Node
{
    public struct RoutingEntry(string nextHop, int distance)
    {
        public string NextHop = nextHop;
        public int Distance = distance;
    }

    private readonly ConcurrentDictionary<string, RoutingEntry> routingTable = new();

    public Router(string name) => Name = name;

    public void CollectNeighbourRoutes()
    {
        foreach (var neighbour in Network.GetNeighbours(Name))
        {
            //Console.WriteLine($"{Name} is adding initial route to {neighbour}");
            routingTable[neighbour] = new RoutingEntry(neighbour, 1);
        }
    }

    public override void Receive(Packet packet)
    {
        //Console.WriteLine($"{Name} received packet from last hop {packet.LastHop}");
        if (packet.Destination == Name) // If the packet is for this router
        {
            Network.Delivered(packet);
        }
        else
        {
            ingressPackets.Add(packet); // Route the packet
        }
    }

    public override void Step()
    {
        foreach (Packet packet in ingressPackets)
        {
            //Console.WriteLine($"{Name} is processing packet {packet.Name}");
            Route(packet);
            egressPackets.Add(packet);
        }
        ingressPackets.Clear();
    }

    public override void Transmit()
    {
        foreach (Packet packet in egressPackets)
        {
            //Console.WriteLine($"{Name} is transmitting packet {packet.Name} to {packet.NextHop}");
            Network.Send(packet);
        }
        egressPackets.Clear();
    }

    public void Route(Packet packet)
    {
        packet.Step();
        packet.LastHop = Name;
        bool chance = Random.Shared.NextSingle() < 0.9;

        // Get a route from the routing table, but only 90% of the time. 10% of the time, choose a random route to midigate loops
        if (routingTable.TryGetValue(packet.Destination, out var entry) && chance)
        {
            //Console.WriteLine($"Route found. Routing packet to {packet.Destination} via {entry.NextHop}");
            packet.LastHop = Name;
            packet.NextHop = entry.NextHop;
            //Console.WriteLine($"{Name} is routing packet {packet.Name} to {packet.Destination} via {packet.NextHop}");
        }
        else
        {
            if (chance) WriteLine($"No route to {packet.Destination} from {Name} found, using random route");
            //else Console.WriteLine($"Loop evasion, using random route");
            var neighbours = Network.GetNeighbours(Name);
            if (neighbours.Count == 0) throw new ArgumentException("Router has no neighbours");
            packet.NextHop = neighbours[new Random().Next(neighbours.Count)];
        }
    }

    public bool BroadcastRoutingTable()
    {
        bool updated = false;
        //Console.WriteLine($"Broadcasting routing table from {Name}");
        var neighbours = Network.GetNeighbours(Name);
        Parallel.ForEach(neighbours, neighbour =>
        {
            if (Network.GetNode(neighbour) is Router neighborNode)
                updated |= neighborNode.UpdateRoutingTable(Name, routingTable);
        });
        return updated;
    }

    public bool UpdateRoutingTable(string neighbor, ConcurrentDictionary<string, RoutingEntry> neighborTable)
    {
        bool updated = false;
        foreach (var entry in neighborTable)
        {
            var destination = entry.Key;
            var neighborEntry = entry.Value;
            var newDistance = neighborEntry.Distance + 1;

            if (!routingTable.TryGetValue(destination, out RoutingEntry value) || value.Distance > newDistance)
            {
                value = new RoutingEntry(neighbor, newDistance);
                //Console.WriteLine($"Updating route to {destination} via neighbour {neighbor}'s routing table");
                routingTable[destination] = value;
                updated = true;
            }
        }
        return updated;
    }

    public override IEnumerable<T> ReportPackets<T>() => [];
}