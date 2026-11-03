#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
#if !NET9_0_OR_GREATER
using System.Linq.Expressions;
#endif

namespace MinesServer.Networking;

public delegate TPacketType NetworkPacketDecoder<TPacketType>(ReadOnlySpan<byte> input);

public abstract class NetworkPacketCodeProvider<TPacketType, TIdentity>
    where TPacketType : class, INetworkPacketBase
    where TIdentity : unmanaged, IConvertible
{
    private readonly Dictionary<Type, TIdentity> _packetsMapping = new();
    private readonly List<NetworkPacketDecoder<TPacketType>> _decoders = new();
    private int _nextIdentity;

    protected void Register<TPacket>() where TPacket : INetworkPacket<TPacket>, TPacketType
    {
        var identity = (TIdentity)Convert.ChangeType(
            _nextIdentity,
            typeof(TIdentity),
            CultureInfo.InvariantCulture);
        _packetsMapping.Add(typeof(TPacket), identity);

#if NET9_0_OR_GREATER
        NetworkPacketDecoder<TPacketType> decoder = input => TPacket.Decode(input);
#else
        var parameter = Expression.Parameter(typeof(ReadOnlySpan<byte>), "input");
        NetworkPacketDecoder<TPacketType> decoder = Expression.Lambda<NetworkPacketDecoder<TPacketType>>(
            Expression.Convert(
                Expression.Call(typeof(TPacket), "Decode", null, parameter),
                typeof(TPacketType)),
            parameter).Compile();
#endif
        _decoders.Add(decoder);
        _nextIdentity++;
    }

    public TIdentity GetPacketCode(TPacketType packet) => _packetsMapping[packet.GetType()];

    public TIdentity GetPacketCode<TPacket>() where TPacket : TPacketType => _packetsMapping[typeof(TPacket)];

    public NetworkPacketDecoder<TPacketType> GetDecoder(TIdentity identity) =>
        _decoders[Convert.ToInt32(identity, CultureInfo.InvariantCulture)];

    public bool TryGetDecoder(
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
    public bool IsValid(TIdentity identity)
    {
        int code = Convert.ToInt32(identity, CultureInfo.InvariantCulture);
        return code >= 0 && code < _nextIdentity;
    }
}
