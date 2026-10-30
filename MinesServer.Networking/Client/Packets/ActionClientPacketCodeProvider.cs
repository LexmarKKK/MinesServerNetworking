#nullable enable
using MinesServer.Networking.Client.Packets.Actions;
using MinesServer.Networking.Client.Packets.Movement;
using System.Diagnostics.CodeAnalysis;

namespace MinesServer.Networking.Client.Packets;

public sealed class ActionClientPacketCodeProvider : NetworkPacketCodeProvider<IActionClientPacket, byte>
{
    private static readonly ActionClientPacketCodeProvider _instance = new();

    private ActionClientPacketCodeProvider()
    {
        Register<BuildCyanPacket>();
        Register<BuildGrayPacket>();
        Register<BuildGreenPacket>();
        Register<BuildWhitePacket>();
        Register<BzPacket>();
        Register<GeoPacket>();
        Register<HealPacket>();
        Register<SuicidePacket>();
        Register<ToggleAutoDigPacket>();
        Register<UnmappedKeyPacket>();
        Register<ClickCellPacket>();
        Register<MovePacket>();
        Register<RotatePacket>();
    }

    public new static byte GetPacketCode<TPacket>() where TPacket : IActionClientPacket => ((NetworkPacketCodeProvider<IActionClientPacket, byte>)_instance).GetPacketCode<TPacket>();

    public new static byte GetPacketCode(IActionClientPacket packet) => ((NetworkPacketCodeProvider<IActionClientPacket, byte>)_instance).GetPacketCode(packet);

    public new static NetworkPacketDecoder<IActionClientPacket> GetDecoder(byte identity) => ((NetworkPacketCodeProvider<IActionClientPacket, byte>)_instance).GetDecoder(identity);

    public new static bool TryGetDecoder(byte identity, [NotNullWhen(true)] out NetworkPacketDecoder<IActionClientPacket>? decoder) =>
        ((NetworkPacketCodeProvider<IActionClientPacket, byte>)_instance).TryGetDecoder(identity, out decoder);

    public new static bool IsValid(byte identity) => ((NetworkPacketCodeProvider<IActionClientPacket, byte>)_instance).IsValid(identity);
}
