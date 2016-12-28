using PointingPoker.DataAccess.Models;
using Xunit;

namespace PointingPoker.Tests
{
    public class CardTests
    {
        [Fact]
        public void IsNotClosedWheninvalidIdIsPresent()
        {
            // arrange
            var card = new Card
            {
                ClosedBy = 0
            };

            // act
            var result = card.IsClosed;

            // assert
            Assert.False(result);
        }

        [Fact]
        public void IsNotClosedWhenIdIsNull()
        {
            // arrange
            var card = new Card
            {
                ClosedBy = null
            };

            // act
            var result = card.IsClosed;

            // assert
            Assert.False(result);
        }

        [Fact]
        public void IsClosedWhenUserHasClosedIt()
        {
            // arrange
            var card = new Card
            {
                ClosedBy = 1
            };

            // act
            var result = card.IsClosed;

            // assert
            Assert.True(result);
        }
    }
}
