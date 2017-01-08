using PointingPoker.Models;
using Xunit;

namespace PointingPoker.Tests.ModelTests
{
    public class InviteTeamMembersViewModelTests
    {
        [Fact]
        public void GetParsedInvitedEmails()
        {
            // arrange
            var model = new InviteTeamMembersViewModel();
            model.InvitedEmails = "email1 email2";

            // act
            var results = model.GetParsedInvitedEmails;

            // assert
            Assert.Equal(2, results.Count);
            Assert.Equal("email1", results[0]);
            Assert.Equal("email2", results[1]);
        }

        [Fact]
        public void WillNotErrorWhenEmailStringIsEmpty()
        {
            // arrange
            var model = new InviteTeamMembersViewModel();
            model.InvitedEmails = string.Empty;

            // act
            var result = model.GetParsedInvitedEmails;

            // assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void WillNotErrorWhenEmailStringIsNull()
        {
            // arrange
            var model = new InviteTeamMembersViewModel();
            model.InvitedEmails = null;

            // act
            var result = model.GetParsedInvitedEmails;

            // assert
            Assert.Equal(0, result.Count);
        }
    }
}
