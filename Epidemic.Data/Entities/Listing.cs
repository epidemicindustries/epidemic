using System;
using System.Collections.Generic;
using System.Text;

namespace Epidemic.Data.Entities
{
    /// <summary>
    /// Represents an item listed for sale.
    /// </summary>
    public class Listing:ListingBrief
    {
        /// <summary>
        ///Gets or sets the title for this Listing.
        /// </summary>
        /// <remarks>This value is user-supplied and is used for display purposes.</remarks>
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
