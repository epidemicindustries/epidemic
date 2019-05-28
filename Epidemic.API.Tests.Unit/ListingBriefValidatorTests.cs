using Epidemic.API.Listings.Validators;
using Epidemic.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Epidemic.API.Tests.Unit
{
   public class ListingBriefValidatorTests
    {
        ListingBriefValidator validator;
        public ListingBriefValidatorTests()
        {
            validator = new ListingBriefValidator();
        }
        [Fact]
        public void IsAnAbstractValidatorOfListingBrief()
        {
            Assert.IsAssignableFrom<AbstractValidator<ListingBrief>>(new ListingBriefValidator());
        }
        [Fact]
        public void Validation_Fails_When_ID_Is_Empty()
        {
            var invalidListing = new ListingBrief() { ID = Guid.Empty };
            var result = validator.Validate(invalidListing);
            Assert.False(result.IsValid);
        }
        [Fact]
        public void Validation_Fails_When_Reference_Is_Less_Than_One()
        {
            var invalidListing = new ListingBrief() { ID = Guid.NewGuid(), Reference = 0 };
            var result = validator.Validate(invalidListing);
            Assert.False(result.IsValid);
        }
    }
}
