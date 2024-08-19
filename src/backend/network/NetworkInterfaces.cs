namespace Sandbox_Simulator_2024.src.backend.network;

public interface IUDP
{ }

public interface IEntity
{ }

public interface IName
{
    string Name { get; set; }
}

public interface IGenerateNetworks
{
    public Task GenerateNetwork();
}