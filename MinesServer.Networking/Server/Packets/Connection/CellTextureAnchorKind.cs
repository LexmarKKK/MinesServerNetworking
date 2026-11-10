namespace MinesServer.Networking.Server.Packets.Connection;

public enum CellTextureAnchorKind : byte
{
    Cell = 0, // Своя картинка на клетку.
    World = 1 // Кусок общей картинки по мировой позиции: соседние клетки читаются без швов.
}
