using Moq;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using PointingPoker.Domain;
using System;
using Xunit;

namespace PointingPoker.Tests
{
    public class PointServiceTests
    {
        private Mock<IPointCommands> _pointCommandsMock;
        private Mock<ICardQueries> _cardQueriesMock;

        private Point _point;

        private PointService service;

        private void SetUp()
        {
            _cardQueriesMock = new Mock<ICardQueries>();
            _pointCommandsMock = new Mock<IPointCommands>();
            
            service = new PointService(
                _pointCommandsMock.Object,
                _cardQueriesMock.Object);

            _point = new Point
            {
                Id = Guid.NewGuid(),
                PointedBy = Guid.NewGuid(),
                CardId = Guid.NewGuid(),
                Points = 1,
                DateCreated = DateTime.Now
            };
        }

        [Fact]
        public void CannotCreatePointWhenPointIdIsEmpty()
        {
            // arrange
            SetUp();
            _point.Id = Guid.Empty;

            // act
            var result = service.PointCard(_point);

            // assert
            Assert.False(result);
            _pointCommandsMock.Verify(x => x.CreatePoint(It.IsAny<Point>()), Times.Never);
        }

        [Fact]
        public void CannotCreatePointWhenCardIdIsEmpty()
        {
            // arrange
            SetUp();
            _point.CardId = Guid.Empty;

            // act
            var result = service.PointCard(_point);

            // assert
            Assert.False(result);
            _pointCommandsMock.Verify(x => x.CreatePoint(It.IsAny<Point>()), Times.Never);
        }

        [Fact]
        public void CannotCreatePointWhenPointedByIsEmpty()
        {
            // arrange
            SetUp();
            _point.PointedBy = Guid.Empty;

            // act
            var result = service.PointCard(_point);

            // assert
            Assert.False(result);
            _pointCommandsMock
                .Verify(
                    x => x.CreatePoint(It.IsAny<Point>()), 
                    Times.Never);
        }

        [Fact]
        public void CannotCreatePointWhenCardPointingIsClosed()
        {
            // arrange
            SetUp();
            _cardQueriesMock
                .Setup(x => x.IsCardClosedForPointing(It.IsAny<Guid>()))
                .Returns(true);

            // act
            var result = service.PointCard(_point);

            // assert
            Assert.False(result);
            _pointCommandsMock
                .Verify(
                    x => x.CreatePoint(It.IsAny<Point>()),
                    Times.Never);
        }

        [Fact]
        public void CanPointACard()
        {
            // arrange
            SetUp();

            // act
            var result = service.PointCard(_point);

            // assert
            Assert.True(result);
            _pointCommandsMock.Verify(x => x.CreatePoint(It.IsAny<Point>()), Times.Once);
        }
    }
}
