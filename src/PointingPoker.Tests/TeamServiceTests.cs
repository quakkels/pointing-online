using Moq;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using PointingPoker.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace PointingPoker.Tests
{
    public class TeamServiceTests
    {
        private Mock<ITeamCommands> _teamCommands;
        private Mock<ITeamQueries> _teamQueries;
        private Team _team;
        private IEnumerable<string> _memberEmails;
        private TeamService _service;
        
        private void SetUp()
        {
            _team = new Team
            {
                Id = 1,
                Name = "Name",
                CreatedBy = 1
            };

            _teamCommands = new Mock<ITeamCommands>();
            _teamCommands
                .Setup(x => x.CreateTeam(It.IsAny<Team>(), It.IsAny<IEnumerable<string>>()))
                .Returns(1);

            _teamQueries = new Mock<ITeamQueries>();

            _service = new TeamService(_teamCommands.Object, _teamQueries.Object);
        }
        
        [Fact]
        public void WillNotAddWhenMissingName()
        {
            // arrange
            SetUp();
            _team.Name = "";

            // act
            var result = _service.CreateTeam(_team, _memberEmails);
            _team.Name = null;
            var result2 = _service.CreateTeam(_team, _memberEmails);
            
            // assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void WillNotAddWhenMissingCreator()
        {
            // arrange
            SetUp();
            _team.CreatedBy = 0;

            // act
            var result = _service.CreateTeam(_team, _memberEmails);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void CanAddATeamWithoutMembers()
        {
            // arrange
            SetUp();
            _memberEmails = null;
            _team.Id = 0;

            // act
            var result = _service.CreateTeam(_team, _memberEmails);

            // assert
            _teamCommands.Verify(
                x => x.CreateTeam(
                    It.IsAny<Team>(), 
                    It.IsNotNull<IEnumerable<string>>()), 
                Times.Once);
            Assert.NotEqual(0, _team.Id);
            Assert.True(result);
        }

        [Fact]
        public void CanCreateTeamWithMembers()
        {
            // arrange
            SetUp();
            _team.Id = 0;
            _memberEmails = new List<string> { "email1", "eamil2" };

            // act
            _service.CreateTeam(_team, _memberEmails);

            // assert
            Assert.Equal(1, _team.Id);
        }

        [Fact]
        public void KnowsWhenUserIsTeamMember()
        {
            // arrange
            SetUp();
            _teamQueries
                .Setup(x => x.GetTeamsByUser(It.IsAny<int>()))
                .Returns(new List<Team>
                {
                    new Team { Id = 123 }
                });

            // act
            var result = _service.IsUserInTeam(1, 123);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void KnowsWhenUserIsNotTeamMember()
        {
            // arrange
            SetUp();
            _teamQueries
                .Setup(x => x.GetTeamsByUser(It.IsAny<int>()))
                .Returns(new List<Team>
                {
                    new Team { Id = 123 }
                });

            // act
            var result = _service.IsUserInTeam(1, 321);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void DoesNotErrorWhenUserIsInNoTeams()
        {
            // arrange
            SetUp();
            _teamQueries
                .Setup(x => x.GetTeamsByUser(It.IsAny<int>()))
                .Returns((IEnumerable<Team>)null);

            // act
            var result = _service.IsUserInTeam(1, 321);

            // assert
            Assert.False(result);
        }
    }
}
