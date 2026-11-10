#nullable enable
using System;

namespace MinesServer.Networking.Exceptions;

public class PacketDecodeException : NetworkException
{
    /// <summary>
    /// Сколько байт кадра едет в <see cref="FrameHead"/>. Кадр сверху не ограничен
    /// ничем, кроме <see cref="ProtocolLimits.MaxFrameLength"/> в 4 МБ, и тащить
    /// его в исключение на каждом битом письме — это 4 МБ мусора по проводу от
    /// одного клиента.
    /// </summary>
    public const int FrameHeadBytes = 32;

    public PacketDecodeException(string message) : base(message) { }

    public PacketDecodeException(string message, Exception innerException) : base(message, innerException) { }

    public PacketDecodeException(string message, ReadOnlySpan<byte> frame) : this(message)
    {
        FrameHead = frame[..Math.Min(frame.Length, FrameHeadBytes)].ToArray();
    }

    public PacketDecodeException(string message, Exception innerException, ReadOnlySpan<byte> frame)
        : this(message, innerException)
    {
        FrameHead = frame[..Math.Min(frame.Length, FrameHeadBytes)].ToArray();
    }

    /// <summary>
    /// Начало кадра, на котором декодирование сломалось. Хост логирует его вместо
    /// всего кадра: 9 байт заголовка (длина, timestamp, код) определяют пакет
    /// полностью, а payload у битого кадра всё равно мусор.
    /// </summary>
    public byte[]? FrameHead { get; }

    public string FrameHeadHex
    {
        get
        {
            if (FrameHead is null)
                return string.Empty;
            const string hexDigits = "0123456789ABCDEF";
            var hex = new char[FrameHead.Length * 2];
            for (int index = 0; index < FrameHead.Length; index++)
            {
                byte value = FrameHead[index];
                hex[index * 2] = hexDigits[value >> 4];
                hex[index * 2 + 1] = hexDigits[value & 0x0F];
            }
            return new string(hex);
        }
    }
}
