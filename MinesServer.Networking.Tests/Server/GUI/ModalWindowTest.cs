using MinesServer.Networking.Server.Packets.GUI;

namespace MinesServer.Networking.Tests.Server.GUI;

internal sealed class ModalWindowTest : RootServerPacketTest<ModalWindowPacket>
{
    public override ModalWindowPacket Packet => new(
        "Confirmation",
        "Are you sure you want to proceed? Вы уверены?",
        "Yes",
        "gui/icons/question.png");
}