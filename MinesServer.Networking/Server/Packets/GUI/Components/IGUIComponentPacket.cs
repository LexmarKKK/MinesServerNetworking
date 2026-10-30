using MinesServer.Networking.Contracts.Packets;

namespace MinesServer.Networking.Server.Packets.GUI.Components;

public interface IGUIComponentPacket : INetworkPacketBase
{
    public GUIStylePacket? Style { get; init; }
    public string OnClickContext { get; init; }
    public StringPairPacket[] AttachedProperties { get; init; }

    public byte PacketCode { get; }
}
public interface IGUIComponentPacket<TSelf> : IServerPacket<TSelf>, IGUIComponentPacket where TSelf : IGUIComponentPacket<TSelf> { }
