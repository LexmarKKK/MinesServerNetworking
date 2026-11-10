using MinesServer.Networking.Server.Packets.GUI.Components.Input;

namespace MinesServer.Networking.Tests.Server.GUI.Input;

internal sealed class StringDropdownTest : PacketTest<StringDropdownPacket>
{
    public override StringDropdownPacket Packet => new()
    {
        Name = "asdsa",
        DefaultValue = "qwewq",
        Values = ["qwewq", "rtytr", "uiopoiu"],
        IsEnabled = false,
    };
}
