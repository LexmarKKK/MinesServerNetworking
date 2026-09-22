using System;

namespace MinesServer.Networking.Exceptions;

public class InvalidPayloadException : NetworkException
{
    public InvalidPayloadException(string message) : base(message) { }

    public InvalidPayloadException(string message, Exception innerException) : base(message, innerException) { }
}