using System;
using Epidemic.API.Listings.Validators;
using Epidemic.Data.Abstractions;
using Xunit;
using Epidemic.Data.Entities;
using Epidemic.Shared.Abstractions;
using FluentValidation;
using Moq;

namespace Epidemic.API.Tests.Unit
{
    public class ListingValidatorTests
    {
        public Listing ValidListing { get; set; }
        public Mock<ISystemClock> SystemClock { get; set; }
        public Mock<IListingConstraints> ListingConstraints { get; set; }

        public ListingValidatorTests()
        {
            SystemClock = new Mock<ISystemClock>();
            ListingConstraints = new Mock<IListingConstraints>();
            ListingConstraints.SetupGet(c => c.MaxListingTime).Returns(TimeSpan.FromDays(7));
            SystemClock.Setup(clock => clock.UtcNow).Returns(DateTime.Parse("00:00:00").ToUniversalTime());

            ValidListing = new Listing()
            {
                ID = Guid.NewGuid(),
                Reference = 1,
                CreatedDate = SystemClock.Object.UtcNow,
                ClosingDate = SystemClock.Object.UtcNow.AddDays(7),
                Description = "Test Instance",
                Title = "Test Instance"
            };
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_Title_Is_Null_Empty_Or_Whitespace_Then_Validation_Fails(string value)
        {
            var invalidListing = ValidListing;
            invalidListing.Title = value;
            var validator = new ListingValidator(SystemClock.Object, ListingConstraints.Object);
            var result = validator.Validate(invalidListing);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(-2)]
        public void
            When_Difference_Between_CreatedDate_And_System_Clock_Exceeds_Plus_Or_Minus_One_Minute_Validation_Fails(
                int minutesOffset)
        {

            var invalidListing = ValidListing;
            invalidListing.CreatedDate = SystemClock.Object.UtcNow.AddMinutes(minutesOffset);
            var validator = new ListingValidator(SystemClock.Object, ListingConstraints.Object);
            var result = validator.Validate(invalidListing);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void When_ClosingDate_Is_Before_CreatedDate_Then_Validation_Fails()
        {
            var invalidListing = ValidListing;
            invalidListing.ClosingDate = SystemClock.Object.UtcNow.AddDays(-1);
            var validator = new ListingValidator(SystemClock.Object, ListingConstraints.Object);
            var result = validator.Validate(invalidListing);
            Assert.False(result.IsValid);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void When_Description_Is_Null_Empty_Or_Whitespace_Then_Validation_Fails(string value)
        {
            var invalidListing = ValidListing;
            invalidListing.Description = value;
            var validator = new ListingValidator(SystemClock.Object, ListingConstraints.Object);
            var result = validator.Validate(invalidListing);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void
            When_Difference_Between_Creation_Date_And_ClosingDate_Is_Greater_Than_MaxListingAge_Then_Validation_Fails()
        {
            var constraints = new Mock<IListingConstraints>();
            constraints.SetupGet(c => c.MaxListingTime).Returns(TimeSpan.FromDays(7));
            var tooLong = constraints.Object.MaxListingTime.Add(TimeSpan.FromDays(1));

            var validator = new ListingValidator(SystemClock.Object, constraints.Object);
            var invalidListing = ValidListing;
            invalidListing.ClosingDate = invalidListing.ClosingDate.Add(tooLong);
            var result = validator.Validate(invalidListing);
            Assert.False(result.IsValid);

        }

    }
}
