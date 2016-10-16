using Moq;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace PointingPoker.Tests
{
    public class TeamServiceTests
    {
        private Mock<ITeamCommands> _teamCommands;
        private Team _team;
        private IEnumerable<string> _memberEmails;
        private TeamService _service;
        
        private void SetUp()
        {
            _team = new Team
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                CreatedBy = Guid.NewGuid()
            };

            _teamCommands = new Mock<ITeamCommands>();

            _service = new TeamService(_teamCommands.Object);
        }

        [Fact]
        public void WillNotAddWhenMissingId()
        {
            // arrange
            SetUp();
            _team.Id = Guid.Empty;

            // act
            var result = _service.CreateTeam(_team, _memberEmails);

            //
            Assert.False(result);
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
            _team.CreatedBy = Guid.Empty;

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

            // act
            var result = _service.CreateTeam(_team, _memberEmails);

            // assert
            _teamCommands.Verify(
                x => x.CreateTeam(
                    It.IsAny<Team>(), 
                    It.IsNotNull<IEnumerable<string>>()), 
                Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void CanCreateTeamWithMembers()
        {
            // arrange
            SetUp();
            _memberEmails = new List<string> { "email1", "eamil2" };

            // act
            _service.CreateTeam(_team, _memberEmails);
        }
    }
}
