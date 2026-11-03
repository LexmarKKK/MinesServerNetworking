using MinesServer.Networking.Server.Packets.World;

namespace MinesServer.Networking.Tests.Server.World;

internal sealed class AudioPacketTest : PacketTest<AudioPacket>
{
    public override AudioPacket Packet => new(
        Data.AudioEventType.Boom,
        303,
        50,
        60,
        [
            new("test", "ASDasdasd")
        ]);
}
