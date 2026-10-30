namespace MinesServer.Networking.Contracts.Packets;

public interface IKeyValuePairPacket<TKey, TValue> : INetworkPacketBase
{
    public TKey Key { get; init; }
    public TValue Value { get; init; }
}
