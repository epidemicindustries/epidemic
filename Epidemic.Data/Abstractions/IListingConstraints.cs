using System;

namespace Epidemic.Data.Abstractions
{
    public interface IListingConstraints
    {
        /// <summary>
        /// Gets or sets a <see cref="TimeSpan"/> representing the maximum amount of time a between the creation date and closing date of a listing.
        /// </summary>
         TimeSpan MaxListingTime { get; set; }
    }
}