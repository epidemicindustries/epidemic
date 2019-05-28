using Microsoft.EntityFrameworkCore;
using Epidemic.Data.Entities;

namespace Epidemic.Data
{
    /// <summary>
    /// An entity framework DbContext for the application database.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ListingBrief> Listings { get; set; }
        /// <summary>
        /// Creates a new instance of <see cref="ApplicationDbContext"/>.
        /// </summary>
        public ApplicationDbContext()
        {

        }
        /// <summary>
        /// Creates a new instance of <see cref="ApplicationDbContext"/> configured by the specified options.
        /// </summary>
        /// <param name="options">Options used to confugyre the DbContext.</param>
        /// <remarks>This constructor is primarily used to configure the instance to use alternative providers (e.g. InMemory) for testing.</remarks>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //This space deliberatly left blank. Initialization logic is in the base class.
        }
    }
}
