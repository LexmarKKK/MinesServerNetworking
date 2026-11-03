#nullable enable
namespace MinesServer.Networking.Server.Packets.World;

public sealed class HBPacketCodeProvider : NetworkPacketCodeProvider<IHBPacket, byte, HBPacketCodeProvider>
{
    static HBPacketCodeProvider()
    {
        Register<MapRegionPacket>();
        Register<BuildingPacket>();
        Register<RobotPositionPacket>();
        Register<AudioPacket>();
        Register<VisualPacket>();
        Register<RemoveBuildingPacket>();
    }
}
