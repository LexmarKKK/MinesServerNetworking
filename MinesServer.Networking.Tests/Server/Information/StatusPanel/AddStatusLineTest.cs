using MinesServer.Networking.Server.Packets.Information.StatusPanel;
using System.Drawing;

namespace MinesServer.Networking.Tests.Server.Information.StatusPanel;

internal sealed class AddStatusLineTest : RootServerPacketTest<AddStatusLinePacket>
{
    public override AddStatusLinePacket Packet => new(
    10,
    Color.SteelBlue,
    "buff_status",
    ["Effect active: Speed", "Действует эффект: Скорость"]);
}