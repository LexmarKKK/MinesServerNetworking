#nullable enable
using MinesServer.Networking.Server.Packets.GUI.Components;
using MinesServer.Networking.Server.Packets.GUI.Components.Containers;
using MinesServer.Networking.Server.Packets.GUI.Components.Input;
using MinesServer.Networking.Server.Packets.GUI.Components.Visual;
using System.Diagnostics.CodeAnalysis;

namespace MinesServer.Networking.Server.Packets.GUI;

public sealed class GUIComponentPacketCodeProvider : NetworkPacketCodeProvider<IGUIComponentPacket, byte>
{
    private static readonly GUIComponentPacketCodeProvider _instance = new();

    private GUIComponentPacketCodeProvider()
    {
        Register<TextPacket>();
        Register<ImagePacket>();
        Register<ScrollViewerPacket>();
        Register<DockPanelPacket>();
        Register<GridPacket>();
        Register<IntDropdownPacket>();
        Register<StringDropdownPacket>();
        Register<SelectablePacket>();
        Register<TextBoxPacket>();
        Register<SliderPacket>();
        Register<PanelPacket>();
        Register<LinePacket>();
        Register<CanvasPacket>();
    }

    public new static byte GetPacketCode<TPacket>() where TPacket : IGUIComponentPacket => ((NetworkPacketCodeProvider<IGUIComponentPacket, byte>)_instance).GetPacketCode<TPacket>();

    public new static byte GetPacketCode(IGUIComponentPacket packet) => ((NetworkPacketCodeProvider<IGUIComponentPacket, byte>)_instance).GetPacketCode(packet);

    public new static NetworkPacketDecoder<IGUIComponentPacket> GetDecoder(byte identity) => ((NetworkPacketCodeProvider<IGUIComponentPacket, byte>)_instance).GetDecoder(identity);

    public new static bool TryGetDecoder(byte identity, [NotNullWhen(true)] out NetworkPacketDecoder<IGUIComponentPacket>? decoder) =>
        ((NetworkPacketCodeProvider<IGUIComponentPacket, byte>)_instance).TryGetDecoder(identity, out decoder);

    public new static bool IsValid(byte identity) => ((NetworkPacketCodeProvider<IGUIComponentPacket, byte>)_instance).IsValid(identity);
}
