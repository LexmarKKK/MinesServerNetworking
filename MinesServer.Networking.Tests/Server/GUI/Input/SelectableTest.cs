using MinesServer.Networking.Server.Packets.GUI.Components.Input;

namespace MinesServer.Networking.Tests.Server.GUI.Input;

internal sealed class SelectableTest : PacketTest<SelectablePacket>
{
    public override SelectablePacket Packet => new() {
        Name = "asdsa",
        DefaultValue = true
    };
}