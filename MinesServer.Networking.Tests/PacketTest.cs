using System;
using System.Linq.Expressions;

namespace MinesServer.Networking.Tests;

internal abstract class PacketTest<TPacket> where TPacket : INetworkPacket<TPacket>
{
    private delegate TPacket PacketDecoder(ReadOnlySpan<byte> input);

    private static readonly PacketDecoder _decoder = CreateDecoder();

    public abstract TPacket Packet { get; }

    [Test]
    public virtual void LengthPrediction()
    {
        var packet = Packet;
        Span<byte> encoded = stackalloc byte[packet.Size];
        var len = packet.Encode(encoded);
        if(packet is IUnreliableLengthPacket)
            Assert.That(len, Is.LessThanOrEqualTo(packet.Size));
        else
            Assert.That(len, Is.EqualTo(packet.Size));
    }

    [Test]
    public virtual void Encoding()
    {
        var packet = Packet;
        Span<byte> encoded = stackalloc byte[packet.Size];
        int written = packet.Encode(encoded);
        var frame = encoded[..written];
        var pack = _decoder(frame);
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

    private static PacketDecoder CreateDecoder()
    {
        var input = Expression.Parameter(typeof(ReadOnlySpan<byte>), "input");
        var decode = Expression.Call(typeof(TPacket), "Decode", null, input);
        return Expression.Lambda<PacketDecoder>(decode, input).Compile();
    }
}
