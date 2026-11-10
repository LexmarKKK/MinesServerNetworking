using MinesServer.Networking.Server.Packets.GUI.Components.Input;

namespace MinesServer.Networking.Tests.Server.GUI.Input;

internal sealed class IntDropdownTest : PacketTest<IntDropdownPacket>
{
    public override IntDropdownPacket Packet => new()
    {
        DefaultValue = 1,
        Values = [1, 2, 3, 4, 5],
        Name = "asdsa"
    };
}