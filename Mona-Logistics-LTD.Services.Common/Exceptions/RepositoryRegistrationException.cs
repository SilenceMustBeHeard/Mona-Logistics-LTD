using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.Common.Exceptions;

public class RepositoryRegistrationException : Exception
{
    public RepositoryRegistrationException() : base() { }

    public RepositoryRegistrationException(string message) : base(message) { }

    public RepositoryRegistrationException(string message, Exception innerException)
        : base(message, innerException) { }
}