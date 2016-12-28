using Moq;
using PointingPoker.Domain;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
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
                .Setup(x => x.DoesCardCreatorExist(It.IsAny<int>()))
                .Returns(true);

            _cardService = new CardService(
                _cardCommandsMock.Object,
                _cardQueriesMock.Object);

            _card = new Card
            {
                Id = 1,
                CreatedBy = 1,
                Description = "description",
                ClosedBy = 0,
                TeamId = 1
            };
        }

        [Fact]
        public void WillNotSaveCardWithMissingCreator()
        {
            // arrange
            Setup();
            _card.CreatedBy = 0;

            // act
            var result = _cardService.CreateCard(_card);

            // assert
            Assert.Equal(0, result);
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
            Assert.Equal(0, result);
            Assert.Equal(0, result2);
        }

        [Fact]
        public void WillNotSaveCardWithMissingTeamId()
        {
            // arrange
            Setup();
            _card.TeamId = 0;

            // act
            var result = _cardService.CreateCard(_card);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void WillNotSaveWithCreatorThatDoesNotExist()
        {
            // arrange
            Setup();
            _cardQueriesMock
                .Setup(x => x.DoesCardCreatorExist(It.IsAny<int>()))
                .Returns(false);

            // act
            var result = _cardService.CreateCard(_card);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void WillCreate()
        {
            // arrange
            Setup();
            _cardCommandsMock
                .Setup(x => x.CreateCard(It.IsAny<Card>()))
                .Returns(1);
            _card.Id = 0;

            // act
            var result = _cardService.CreateCard(_card);

            // assert
            Assert.Equal(1, result);
            Assert.Equal(1, _card.Id);
            _cardCommandsMock
                .Verify(
                x => x.CreateCard(_card), Times.Once);
        }

        [Fact]
        public void WillNotCloseCardWithoutCardId()
        {
            // arrange
            Setup();
            var cardId = 0;
            var userId = 1;

            // act
            var result = _cardService.ClosePointing(cardId, userId);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void WillNotCloseCardWithoutUserId()
        {
            // arrange
            Setup();
            var cardId = 1;
            var userId = 0;

            // act
            var result = _cardService.ClosePointing(cardId, userId);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void WillClosePointingOnCard()
        {
            // arrange
            Setup();
            var cardId = 3;
            var userId = 5;

            // act
            var result = _cardService.ClosePointing(cardId, userId);

            // assert
            Assert.True(result);
            _cardCommandsMock.Verify(x => x.ClosePointing(cardId, userId), Times.Once);
        }
    }
}
