#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
#if NET9_0_OR_GREATER
using System.Numerics;
#endif
#if !NET9_0_OR_GREATER
using System.Globalization;
using System.Linq.Expressions;
#endif

namespace MinesServer.Networking;

public delegate TPacketType NetworkPacketDecoder<TPacketType>(ReadOnlySpan<byte> input);

public abstract class NetworkPacketCodeProvider<TPacketType, TIdentity, TSelf>
    where TPacketType : class, INetworkPacketBase
#if NET9_0_OR_GREATER
    where TIdentity : unmanaged, IBinaryInteger<TIdentity>
#else
    where TIdentity : unmanaged, IConvertible
#endif
    where TSelf : NetworkPacketCodeProvider<TPacketType, TIdentity, TSelf>, new()
{
    private static readonly Dictionary<Type, TIdentity> _packetsMapping = new();
    private static readonly List<NetworkPacketDecoder<TPacketType>> _decoders = new();
    private static int _nextIdentity;

    static NetworkPacketCodeProvider()
    {
        _ = new TSelf();
    }

    protected static void Register<TPacket>() where TPacket : INetworkPacket<TPacket>, TPacketType
    {
#if NET9_0_OR_GREATER
        TIdentity identity = TIdentity.CreateChecked(_nextIdentity);
#else
        var identity = (TIdentity)Convert.ChangeType(
            _nextIdentity,
            typeof(TIdentity),
            CultureInfo.InvariantCulture);
#endif
        _packetsMapping.Add(typeof(TPacket), identity);

#if NET9_0_OR_GREATER
        NetworkPacketDecoder<TPacketType> decoder = input => TPacket.Decode(input);
#else
        var parameter = Expression.Parameter(typeof(ReadOnlySpan<byte>), "input");
        // Для NAOT/IL2CPP компиляция выражения автоматически переходит в интерпретируемую форму.
        NetworkPacketDecoder<TPacketType> decoder = Expression.Lambda<NetworkPacketDecoder<TPacketType>>(
            Expression.Convert(
                Expression.Call(typeof(TPacket), "Decode", null, parameter),
                typeof(TPacketType)),
            parameter).Compile();
#endif
        _decoders.Add(decoder);
        _nextIdentity++;
    }

    public static TIdentity GetPacketCode(TPacketType packet) => _packetsMapping[packet.GetType()];

    public static TIdentity GetPacketCode<TPacket>() where TPacket : TPacketType => Cache<TPacket>.Code;

    public static NetworkPacketDecoder<TPacketType> GetDecoder(TIdentity identity) =>
        _decoders[ToIndex(identity)];

    public static bool TryGetDecoder(
        TIdentity identity,
        [NotNullWhen(true)] out NetworkPacketDecoder<TPacketType>? decoder)
    {
        decoder = default;
        if (!IsValid(identity))
            return false;
        decoder = GetDecoder(identity);
        return true;
    }

    /// <summary>
    /// Код 0 соответствует первому зарегистрированному пакету, а код,
    /// равный числу зарегистрированных пакетов или превышающий его, невалиден.
    /// </summary>
    public static bool IsValid(TIdentity identity)
    {
        int code = ToIndex(identity);
        return code >= 0 && code < _nextIdentity;
    }

    /// <summary>
    /// Код пакета берётся из реестра один раз на тип, чтобы кодирование кадра
    /// не обращалось к словарю на каждый пакет.
    /// </summary>
    public static class Cache<TPacket> where TPacket : TPacketType
    {
        public static TIdentity Code { get; } = _packetsMapping[typeof(TPacket)];
    }

    private static int ToIndex(TIdentity identity) =>
#if NET9_0_OR_GREATER
        int.CreateChecked(identity);
#else
        Convert.ToInt32(identity, CultureInfo.InvariantCulture);
#endif
}
