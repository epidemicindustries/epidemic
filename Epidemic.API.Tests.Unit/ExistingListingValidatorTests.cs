using Epidemic.Data;
using Epidemic.Data.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Epidemic.API.Listings.Validators;
using Xunit;

namespace Epidemic.API.Tests.Unit
{
    public class ExistingListingValidatorTests
    {
        [Fact]
        public void Is_An_AbstractValidator_Of_ListingBrief()
        {
            Assert.IsAssignableFrom<AbstractValidator<ListingBrief>>(new ExistingListingValidator(null));
        }
        [Fact]
        public void Validation_Fails_When_No_Listing_With_Specified_Reference_Exists()
        {
            var nonExistentReference = new ListingBrief() { Reference = 0 };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UnitTest-DB")
                .Options;
            using (var dbContext = new ApplicationDbContext(options))
            {
                var validator = new ExistingListingValidator(dbContext);
                var result = validator.Validate(nonExistentReference);
                Assert.False(result.IsValid);

            }
        }
    }
}
