namespace MinesServer.Networking;

public interface INetworkPacket<TSelf> : INetworkPacketBase where TSelf : INetworkPacket<TSelf>
{
}
