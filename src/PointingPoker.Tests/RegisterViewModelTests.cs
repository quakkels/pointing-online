using PointingPoker.Models;
using System;
using Xunit;

namespace PointingPoker.Tests
{
    public class RegisterViewModelTests
    {
        [Fact]
        public void NewModelHasSetId()
        {
            var result = new RegisterViewModel();

            Assert.NotNull(result.Id);
            Assert.False(result.Id == Guid.Empty);
        }
    }
}
