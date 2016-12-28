using Moq;
using PointingPoker.Domain;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace PointingPoker.Tests
{
    public class CardServiceTests
    {
        private Mock<ICardCommands> _cardCommandsMock;
        private Mock<ICardQueries> _cardQueriesMock;
        private Mock<ITeamQueries> _teamQueriesMock;

        private Card _card;
        private CardService _cardService;

        public void Setup()
        {
            _teamQueriesMock = new Mock<ITeamQueries>();
            _teamQueriesMock
                .Setup(x => x.GetTeamsByUser(It.IsAny<int>()))
                .Returns(new List<Team> { new Team { Id = 1 } });

            _cardCommandsMock = new Mock<ICardCommands>();

            _cardQueriesMock = new Mock<ICardQueries>();
            _cardQueriesMock
                .Setup(x => x.DoesCardCreatorExist(It.IsAny<int>()))
                .Returns(true);
            _cardQueriesMock
                .Setup(x => x.GetClosedCardsForTeam(It.IsAny<int>()))
                .Returns(new List<Card> {
                    new Card { Id = 1 }
                });

            _cardService = new CardService(
                _cardCommandsMock.Object,
                _cardQueriesMock.Object,
                _teamQueriesMock.Object);

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
            var cardId = 1;
            var userId = 1;

            // act
            var result = _cardService.ClosePointing(cardId, userId);

            // assert
            Assert.True(result);
            _cardCommandsMock.Verify(x => x.ClosePointing(cardId, userId), Times.Once);
        }

        [Fact]
        public void CannotGetClosedCardsForNonTeamMember()
        {
            // arrange
            Setup();
            var teamId = 1;
            var userId = 5422545; // not a member of any team
            _teamQueriesMock
                .Setup(x => x.GetTeamsByUser(It.Is<int>(y => y == userId)))
                .Returns(new List<Team>());

            // act 
            var result = _cardService.GetClosedCardsForTeam(teamId, userId);

            // assert
            Assert.False(result.Any());
        }

        [Fact]

        public void CanGetClosedCardsForTeam()
        {
            // arrange
            Setup();
            var teamId = 1;
            var userId = 1;

            // act
            var result = _cardService.GetClosedCardsForTeam(teamId, userId);

            // assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
