using MinesServer.Networking.Client;
using MinesServer.Networking.Client.Packets;

namespace MinesServer.Networking.Tests.Client;

internal abstract class RootClientPacketTest<TPacket> : PacketTest<TPacket> where TPacket : IRootClientPacket<TPacket>
{
    [Test]
    public virtual void WrappedEncoding()
    {
        var packet = new ClientPacket(0x11223344, Packet);
        Span<byte> encoded = stackalloc byte[packet.Size];
        int written = packet.Encode(encoded);
        var frame = encoded[..written];
        var pack = ClientPacket.Decode(frame);
        Assert.That(pack, Is.EqualTo(packet));
        Span<byte> re_encoded = stackalloc byte[pack.Size];
        int re_written = pack.Encode(re_encoded);
        var reEncodedBytes = re_encoded[..re_written].ToArray();
        var originalBytes = frame.ToArray();
        Assert.Multiple(() =>
        {
            Assert.That(re_written, Is.EqualTo(written));
            Assert.That(reEncodedBytes, Is.EqualTo(originalBytes));
        });
    }
}
