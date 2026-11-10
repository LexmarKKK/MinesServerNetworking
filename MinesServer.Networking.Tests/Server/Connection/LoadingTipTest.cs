using MinesServer.Networking.Server.Packets.Connection;

namespace MinesServer.Networking.Tests.Server.Connection;

internal sealed class LoadingTipTest : RootServerPacketTest<LoadingTipPacket>
{
    public override LoadingTipPacket Packet => new("<b>Синие кристаллы</b> встречаются глубже зелёных.");
}

internal sealed class EmptyLoadingTipTest : RootServerPacketTest<LoadingTipPacket>
{
    public override LoadingTipPacket Packet => new(string.Empty);
}
