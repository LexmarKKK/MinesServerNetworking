using MinesServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesServer.Networking.Client.Packets.Utilities
{
    public readonly record struct RuntimeAssetRequestPacket(IReadOnlyList<RuntimeAssetEntryPacket> Assets) : IRootClientPacket<RuntimeAssetRequestPacket>
    {
        public byte PacketCode => RootClientPacketCodeProvider.GetPacketCode<RuntimeAssetRequestPacket>();

        public int Size => Assets.Sum(x => x.Size);

        public int Encode(Span<byte> output)
        {
            var writer = output.Writer();
            foreach (RuntimeAssetEntryPacket item in Assets)
                writer.Write(item);
            return writer.Position;
        }

        public static RuntimeAssetRequestPacket Decode(ReadOnlySpan<byte> input)
        {
            var reader = input.Reader();
            List<RuntimeAssetEntryPacket> assets = new();
            while(reader.CanRead)
            {
                var asset = RuntimeAssetEntryPacket.Decode(reader.Remaining);
                reader.Advance(asset.Size);
                assets.Add(asset);
            }
            return new(assets);
        }

        public bool Equals(RuntimeAssetRequestPacket other) => Assets.SequenceEqual(other.Assets);

        public override int GetHashCode()
        {
            var hash = new System.HashCode();
            foreach (var asset in Assets)
                hash.Add(asset);
            return hash.ToHashCode();
        }

    }
}
