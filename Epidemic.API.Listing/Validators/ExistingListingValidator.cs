using Epidemic.Data;
using Epidemic.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Epidemic.API.Listings.Validators
{
    /// <summary>
    /// Validates that a specified listing reference exists in the database
    /// </summary>
    public class ExistingListingValidator : AbstractValidator<ListingBrief>
    {
        private ApplicationDbContext dbContext;

        public ExistingListingValidator(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            RuleFor(x => x.Reference).Must((listing, reference) => ReferenceExists(listing, reference))
                .WithMessage(ValidationErrorMessages.ListingWithReferenceDoesNotExist);
        }
        private bool ReferenceExists(ListingBrief listing, ulong reference )
        {
            using (dbContext)
            {
                return dbContext.Listings.Where(x => x.Reference == reference).Count() > 0;
            }
        }
    }
}
