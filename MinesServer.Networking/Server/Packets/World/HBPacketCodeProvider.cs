#nullable enable
namespace MinesServer.Networking.Server.Packets.World;
using System.Diagnostics.CodeAnalysis;

public sealed class HBPacketCodeProvider : NetworkPacketCodeProvider<IHBPacket, byte>
{
    private static readonly HBPacketCodeProvider _instance = new();

    private HBPacketCodeProvider()
    {
        Register<MapRegionPacket>();
        Register<BuildingPacket>();
        Register<RobotPositionPacket>();
        Register<AudioPacket>();
        Register<VisualPacket>();
        Register<RemoveBuildingPacket>();
    }

    public new static byte GetPacketCode<TPacket>() where TPacket : IHBPacket => ((NetworkPacketCodeProvider<IHBPacket, byte>)_instance).GetPacketCode<TPacket>();

    public new static byte GetPacketCode(IHBPacket packet) => ((NetworkPacketCodeProvider<IHBPacket, byte>)_instance).GetPacketCode(packet);

    public new static NetworkPacketDecoder<IHBPacket> GetDecoder(byte identity) => ((NetworkPacketCodeProvider<IHBPacket, byte>)_instance).GetDecoder(identity);

    public new static bool TryGetDecoder(byte identity, [NotNullWhen(true)] out NetworkPacketDecoder<IHBPacket>? decoder) =>
        ((NetworkPacketCodeProvider<IHBPacket, byte>)_instance).TryGetDecoder(identity, out decoder);

    public new static bool IsValid(byte identity) => ((NetworkPacketCodeProvider<IHBPacket, byte>)_instance).IsValid(identity);
}
