using Epidemic.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Epidemic.API.Listings.Validators
{
    /// <summary>
    /// Validator for <see cref="ListingBrief"/>
    /// </summary>
    public class ListingBriefValidator:AbstractValidator<ListingBrief>
    {
        public ListingBriefValidator()
        {
            RuleFor(x => x.ID).NotEqual(Guid.Empty).WithMessage(ValidationErrorMessages.EmptyGuidIsNotAValidListingID);
            RuleFor(x => x.Reference).GreaterThan(0ul).WithMessage(ValidationErrorMessages.ListingReferenceNotValid);
        }
    }
}
