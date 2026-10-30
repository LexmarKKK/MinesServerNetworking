namespace MinesServer.Data;

public enum CellAnimationType : byte
{
    // Значения сериализуются в CellConfigurationPacket и дублируются в шейдере террейна.
    None, // Без анимации; например, у Empty.
    Blinking, // Мигание; используется GrayAcid.
    Shimmer, // Мерцание; используется PurpleAcid.
    Rainbow, // Перелив радужных цветов; в текущей конфигурации клеток не используется.
    Molten, // Анимация расплавленной поверхности; используется Lava.
    Faceted, // Переливчатая гранёная поверхность; используется Green.
    Prismatic, // Призматический перелив; например, у XGreen.
    Legacy2Test,
    Legacy3Test,
    Legacy4Test,
    Legacy5Test,
    Legacy6Test,
    Legacy7Test,
    Legacy8Test,
}
