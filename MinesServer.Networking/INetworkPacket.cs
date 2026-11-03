#if NET9_0_OR_GREATER
using System;
#endif

namespace MinesServer.Networking;

public interface INetworkPacket<TSelf> : INetworkPacketBase where TSelf : INetworkPacket<TSelf>
{
#if NET9_0_OR_GREATER
    static abstract TSelf Decode(ReadOnlySpan<byte> input);
#endif
}
