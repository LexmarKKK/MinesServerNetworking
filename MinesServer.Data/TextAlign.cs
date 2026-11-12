namespace MinesServer.Data;

/// <summary>
/// Горизонтальное выравнивание текста элемента окна. Default оставляет
/// решение скину клиента — текущее поведение до введения поля.
/// </summary>
public enum TextAlign : byte
{
    Default,
    Left,
    Center,
    Right,
}
