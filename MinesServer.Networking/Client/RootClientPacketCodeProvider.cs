#nullable enable
using MinesServer.Networking.Client.Packets.Actions;
using MinesServer.Networking.Client.Packets.Chat;
using MinesServer.Networking.Client.Packets.Connection;
using MinesServer.Networking.Client.Packets.GUI;
using MinesServer.Networking.Client.Packets.Inventory;
using MinesServer.Networking.Client.Packets.Programmator;
using MinesServer.Networking.Client.Packets.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace MinesServer.Networking.Client;

public sealed class RootClientPacketCodeProvider : NetworkPacketCodeProvider<IRootClientPacket, byte>
{
    private static readonly RootClientPacketCodeProvider _instance = new();

    private RootClientPacketCodeProvider()
    {
        Register<ClientHelloPacket>();
        Register<QueryChatHistoryPacket>();
        Register<ElementClickPacket>();
        Register<SendChatMessagePacket>();
        Register<SendLocalChatMessagePacket>();
        Register<ChangeChatColorPacket>();
        Register<DeleteProgramClickPacket>();
        Register<PauseProgramPacket>();
        Register<ProgramStepInPacket>();
        Register<ProgramStepOutPacket>();
        Register<ProgramStepOverPacket>();
        Register<QueryProgramMemoryPacket>();
        Register<RenameProgramClickPacket>();
        Register<SaveProgramPacket>();
        Register<StartProgramPacket>();
        Register<StopProgramPacket>();
        Register<PongPacket>();
        Register<OpenClanClickPacket>();
        Register<OpenHelpClickPacket>();
        Register<OpenProgramListClickPacket>();
        Register<OpenSettingsClickPacket>();
        Register<DeselectItemPacket>();
        Register<SelectItemPacket>();
        Register<UseItemPacket>();
        Register<ActionClientPacket>();
        Register<RuntimeAssetRequestPacket>();
        Register<DropBoxClickPacket>();
        Register<OpenBuildingsClickPacket>();
        // Регистрация новых пакетов — только в конец списка: коды присваиваются
        // последовательно, вставка в середину сдвигает коды всех последующих пакетов.
        Register<OpenChatListPacket>();
        Register<ChatMessageClickPacket>();
    }

    public new static byte GetPacketCode<TPacket>() where TPacket : IRootClientPacket => ((NetworkPacketCodeProvider<IRootClientPacket, byte>)_instance).GetPacketCode<TPacket>();

    public new static byte GetPacketCode(IRootClientPacket packet) => ((NetworkPacketCodeProvider<IRootClientPacket, byte>)_instance).GetPacketCode(packet);

    public new static NetworkPacketDecoder<IRootClientPacket> GetDecoder(byte identity) => ((NetworkPacketCodeProvider<IRootClientPacket, byte>)_instance).GetDecoder(identity);

    public new static bool TryGetDecoder(byte identity, [NotNullWhen(true)] out NetworkPacketDecoder<IRootClientPacket>? decoder) =>
        ((NetworkPacketCodeProvider<IRootClientPacket, byte>)_instance).TryGetDecoder(identity, out decoder);

    public new static bool IsValid(byte identity) => ((NetworkPacketCodeProvider<IRootClientPacket, byte>)_instance).IsValid(identity);
}
