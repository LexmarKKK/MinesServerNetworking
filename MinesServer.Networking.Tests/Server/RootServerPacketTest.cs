using MinesServer.Networking.Server;
using MinesServer.Networking.Server.Packets;

namespace MinesServer.Networking.Tests.Server;

internal abstract class RootServerPacketTest<TPacket> : PacketTest<TPacket> where TPacket : IRootServerPacket<TPacket>
{
    [Test]
    public virtual void WrappedEncoding()
    {
        var packet = new ServerPacket(Packet);
        Span<byte> encoded = stackalloc byte[packet.Size];
        int written = packet.Encode(encoded);
        var frame = encoded[..written];
        var pack = ServerPacket.Decode(frame);
        Assert.That(pack, Is.EqualTo(packet));
        Span<byte> re_encoded = stackalloc byte[pack.Size];
        int re_written = pack.Encode(re_encoded);
        var originalBytes = frame.ToArray();
        var reEncodedBytes = re_encoded[..re_written].ToArray();
        Assert.Multiple(() =>
        {
            Assert.That(re_written, Is.EqualTo(written));
            Assert.That(originalBytes, Is.EqualTo(reEncodedBytes));
        });
    }
}
