namespace MinesServer.Networking.Server.Packets.GUI;

/// <summary>
/// Well-known ключи attached properties (<c>GUIComponentPacket.AttachedProperties</c>).
///
/// Значения — строки, поэтому контракт провода здесь не ловится типами:
/// расхождение написания ключа между сторонами молча выключает свойство.
/// Ключи объявляются только здесь; литералы в коде клиента и сервера запрещены.
///
/// Механизм существует без изменения wire-формата компонентов: пары и так
/// едут в каждом компоненте, сторона-потребитель решает, как применить
/// известные ей ключи.
/// </summary>
public static class AttachedPropertyKeys
{
    /// Позиция и размер ребёнка внутри Canvas, px; любой из ключей делает
    /// ребёнка абсолютным (position: absolute).
    public const string CanvasX = "Canvas.X";
    public const string CanvasY = "Canvas.Y";
    public const string CanvasWidth = "Canvas.Width";
    public const string CanvasHeight = "Canvas.Height";

    /// Пристегивание ребёнка DockPanel: Left / Top / Right / Bottom.
    public const string DockPanelDock = "DockPanel.Dock";

    /// Ячейка и размах ребёнка Grid, нумерация с нуля.
    public const string GridRow = "Grid.Row";
    public const string GridColumn = "Grid.Column";
    public const string GridRowSpan = "Grid.RowSpan";
    public const string GridColumnSpan = "Grid.ColumnSpan";

    /// Действие окна на элементе; сейчас знает одно значение — "close"
    /// (клиент рисует крестик и закрывает окно этим элементом).
    public const string ServerWindowAction = "ServerWindow.Action";

    /// Вариант кнопки (сейчас знает одно значение — "primary"): скин клиента
    /// рисует главное действие — заливку сигналом со срезом угла и
    /// состояние наведения. Кнопка без варианта — обычная клавиша скина.
    public const string ButtonVariant = "Button.Variant";

    /// Гарнитура текста: значения — имена из перечня клиента (Display = Exo 2,
    /// Mono = JetBrains Mono; пусто/неизвестно — основной текст, Onest).
    public const string TextFont = "Text.Font";

    /// Размер любого элемента в px: применяется как style.width/height
    /// вне зависимости от контейнера, в отличие от Canvas.*. Ноль или
    /// отсутствие ключа — размер по контейнеру.
    public const string ElementWidth = "Element.Width";
    public const string ElementHeight = "Element.Height";
}
