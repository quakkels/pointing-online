using Moq;
using PointingPoker.Domain;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using System;
using Xunit;

namespace PointingPoker.Tests
{
    public class CardServiceTests
    {
        private Mock<ICardCommands> _cardCommandsMock;
        private Mock<ICardQueries> _cardQueriesMock;

        private Card _card;
        private CardService _cardService;

        public void Setup()
        {
            _cardCommandsMock = new Mock<ICardCommands>();
            _cardQueriesMock = new Mock<ICardQueries>();
            _cardQueriesMock
                .Setup(x => x.DoesCardCreatorExist(It.IsAny<Guid>()))
                .Returns(true);

            _cardService = new CardService(
                _cardCommandsMock.Object,
                _cardQueriesMock.Object);

            _card = new Card
            {
                Id = Guid.NewGuid(),
                CreatedBy = Guid.NewGuid(),
                Description = "description",
                IsPointingClosed = false
            };
        }

        [Fact]
        public void WillNotSaveCardWithMissingId()
        {
            // arrange
            Setup();
            _card.Id = Guid.Empty;

            // act
            var result = _cardService.CreateCard(_card);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void WillNotSaveCardWithMissingCreator()
        {
            // arrange
            Setup();
            _card.CreatedBy = Guid.Empty;

            // act
            var result = _cardService.CreateCard(_card);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void WillNotSaveCardWithMissingDescription()
        {
            // arrange
            Setup();
            _card.Description = "";

            // act
            var result = _cardService.CreateCard(_card);
            _card.Description = null;
            var result2 = _cardService.CreateCard(_card);

            // assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void WillNotSaveWithCreatorThatDoesNotExist()
        {
            // arrange
            Setup();
            _cardQueriesMock
                .Setup(x => x.DoesCardCreatorExist(It.IsAny<Guid>()))
                .Returns(false);

            // act
            var result = _cardService.CreateCard(_card);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void WillCreate()
        {
            // arrange
            Setup();

            // act
            var result = _cardService.CreateCard(_card);

            // assert
            Assert.True(result);
            _cardCommandsMock
                .Verify(
                x => x.CreateCard(_card), Times.Once);
        }
    }
}
