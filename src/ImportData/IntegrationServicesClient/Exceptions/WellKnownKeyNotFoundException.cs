using System;
using System.Collections.Generic;

namespace ImportData.IntegrationServicesClient.Exceptions
{
    class WellKnownKeyNotFoundException : KeyNotFoundException
    {
        public string Key { get; private set; }

        public WellKnownKeyNotFoundException(string key, string message): this(key, message, null) { }

        public WellKnownKeyNotFoundException(string key, string message, Exception innerException) : base(message, innerException)
        {
            this.Key = key;
        }
    }
}
