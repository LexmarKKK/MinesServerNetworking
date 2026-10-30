using MinesServer.Networking.Server.Packets.GUI.Components.Input;

namespace MinesServer.Networking.Tests.Server.GUI.Input;

internal sealed class SliderTest : PacketTest<SliderPacket>
{
    public override SliderPacket Packet => new() {
        DefaultValue = 23,
        MinValue = 1,
        MaxValue = 100,
        Step = 59.78f,
        Name = "asdsa"
    };
}