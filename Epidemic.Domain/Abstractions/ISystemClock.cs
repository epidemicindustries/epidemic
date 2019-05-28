using System;

namespace Epidemic.Shared.Abstractions
{
    /// <summary>
    /// Abstracts the system clock to facilitate testing.
    /// </summary>
    public interface ISystemClock
    {
        /// <summary>
        /// Gets the current system time in UTC.
        /// </summary>
        DateTime UtcNow { get;  }
    }
}