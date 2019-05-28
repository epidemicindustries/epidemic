using System;
using System.Collections.Generic;
using System.Text;
using Epidemic.Data.Entities;
using Xunit;

namespace Epidemic.API.Tests.Unit
{
   public class ListingTests
    {
        [Fact]
        public void Is_A_ListingBrief()
        {
            Assert.IsAssignableFrom<ListingBrief>(new Listing());
        }
    }
}
