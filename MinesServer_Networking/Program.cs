using MinesServer.Networking.Client.Packets;
using MinesServer.Networking.Client.Packets.Chat;

namespace MinesServer.Networking.Smoke
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var packet = new ClientPacket(12345, new QueryChatHistoryPacket("FED"));
            Span<byte> span = stackalloc byte[packet.Size];
            packet.Encode(span);
            var decoded = ClientPacket.Decode(span);
            Console.ReadKey();
        }
    }
}
