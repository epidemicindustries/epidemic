using System;

namespace Epidemic.Data.Entities
{
    /// <summary>
    /// Representation of a listing containing only unique identifiers for the listing.
    /// </summary>
    public class ListingBrief
    {
        /// <summary>
        /// Machine-friendly unique identifier for this listing.
        /// </summary>
        /// <remarks>This property is not intended for display purposes. Use <see cref="Reference"/> when a unique identifier is required for display purposes.</remarks>
        public Guid ID { get; set; }
        /// <summary>
        /// Human-tollerable unique identifier for this listing.
        /// </summary>
        /// <remarks>
        /// This property is intended for display purposes. Use <see cref="ID"/ if you need a unique <see cref="ID"/> for processing or replication.
        /// </remarks>
        public UInt64 Reference { get; set; }
    }
}
