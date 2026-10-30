using MinesServer.Networking.Client.Packets.Connection;

namespace MinesServer.Networking.Tests.Client.Connection;

internal sealed class ClientHelloTest : RootClientPacketTest<ClientHelloPacket>
{
    public override ClientHelloPacket Packet => new(
        321,
        "Windows",
        11,
        "00112233445566778899AABBCCDDEEFF",
        "m3.a.2C69E5FD-AD4C-4EFE-8449-58B4844E4D9F");
}
