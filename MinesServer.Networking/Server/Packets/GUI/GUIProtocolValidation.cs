#nullable enable

using System;

namespace MinesServer.Networking.Server.Packets.GUI;

internal static class GUIProtocolValidation
{
    public static void U1Ascii(string? value, string field)
    {
        NotNull(value, field);
        MaxLength(value!, byte.MaxValue, field);
        Ascii(value!, field);
    }

    public static void U2Ascii(string? value, string field)
    {
        NotNull(value, field);
        MaxLength(value!, ushort.MaxValue, field);
        Ascii(value!, field);
    }

    public static void U1Utf16(string? value, string field)
    {
        NotNull(value, field);
        MaxLength(value!, byte.MaxValue, field);
    }

    public static void U2Utf16(string? value, string field)
    {
        NotNull(value, field);
        MaxLength(value!, ushort.MaxValue, field);
    }

    public static void U1Count(int count, string field)
    {
        if ((uint)count > byte.MaxValue)
        {
            throw new ArgumentOutOfRangeException(field, count, "GUI protocol count exceeds 255.");
        }
    }

    public static void U2Count(int count, string field)
    {
        if ((uint)count > ushort.MaxValue)
        {
            throw new ArgumentOutOfRangeException(field, count, "GUI protocol count exceeds 65535.");
        }
    }

    private static void NotNull(string? value, string field)
    {
        if (value is null)
        {
            throw new ArgumentNullException(field);
        }
    }

    private static void MaxLength(string value, int maximum, string field)
    {
        if (value.Length > maximum)
        {
            throw new ArgumentOutOfRangeException(
                field,
                value.Length,
                $"GUI protocol field exceeds its {maximum}-character length prefix.");
        }
    }

    public static void Ascii(string? value, string field)
    {
        NotNull(value, field);
        foreach (char character in value!)
        {
            if (character > 0x7f)
            {
                throw new ArgumentException("GUI protocol ASCII field contains a non-ASCII character.", field);
            }
        }
    }
}
