using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epidemic.Data.Abstractions;
using Epidemic.Data.Entities;
using Epidemic.Shared.Abstractions;
using FluentValidation;

namespace Epidemic.API.Listings.Validators
{
    /// <summary>
    /// A validator for <see cref="Listing"/> entities.
    /// </summary>
    public class ListingValidator:AbstractValidator<Listing>
    {
        private ISystemClock systemClock;
        private IListingConstraints constraints;

        public ListingValidator(ISystemClock systemClock, IListingConstraints constraints)
        {
            this.systemClock = systemClock;
            this.constraints = constraints;

            //INSTRUMENTATION: This should be logged as potentially suspect as it could be an attempt to manipulate the system.
            RuleFor(x => systemClock.UtcNow.Subtract(x.CreatedDate).TotalMinutes).GreaterThanOrEqualTo(-1)
                .LessThanOrEqualTo(1).WithMessage(ValidationErrorMessages.CreationDateNotValid);

            RuleFor(x => x.ClosingDate).GreaterThan(x => x.CreatedDate)
                .WithMessage((ValidationErrorMessages.ClosingDateBeforeEndDateNotValid));

            RuleFor(x => x.ClosingDate.Subtract(x.CreatedDate)).LessThanOrEqualTo(constraints.MaxListingTime)
                .WithMessage(string.Format(ValidationErrorMessages.ListingExceedsMaxAgeFormat,
                    constraints.MaxListingTime.TotalDays));

            RuleFor(x => x.Title).Must(value => string.IsNullOrWhiteSpace(value) == false)
                .WithMessage(ValidationErrorMessages.TitleCanNotBeEmpty);

            RuleFor(x => x.Description).Must(value => string.IsNullOrWhiteSpace(value) == false)
                .WithMessage(ValidationErrorMessages.DescriptionCanNotBeEmpty);
        }
    }
}
