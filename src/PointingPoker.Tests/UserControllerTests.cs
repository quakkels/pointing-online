using Moq;
using PointingPoker.Controllers;
using PointingPoker.Domain;
using PointingPoker.Models;
using System;
using Xunit;
namespace PointingPoker.Tests
{
    public class UserControllerTests
    {
        private Mock<IAuthService> _authServiceMock;
        private Mock<IUserService> _userServiceMock;
        private ProfileViewModel _profileViewModel;

        private void Setup()
        {
            _profileViewModel = new ProfileViewModel
            {
                Id = Guid.NewGuid(),
                Username = "username",
                Email = "email",
                Password = "password",
                VerifyPassword = "password"                        
            };

            _authServiceMock = new Mock<IAuthService>();

            _userServiceMock = new Mock<IUserService>();
            _userServiceMock
                .Setup(x => x.UpdatePassword(
                    _profileViewModel.Id,
                    _profileViewModel.Password))
                .Returns(true);
            _userServiceMock
                .Setup(x => x.UpdateUserInfo(
                    _profileViewModel.Id,
                    _profileViewModel.Username,
                    _profileViewModel.Email))
                .Returns(true);
        }

        [Fact]
        public void CanPostNewProfileInfoWithoutUpdatingPassword()
        {
            // arrange
            Setup();
            _profileViewModel.Password = null;
            var controller = new UserController(
                _authServiceMock.Object,
                _userServiceMock.Object);

            // act
            var result = controller.Profile(_profileViewModel);

            // assert
            Assert.True(controller.ModelState.IsValid);
            _userServiceMock.Verify(
                x => x.UpdateUserInfo(
                    _profileViewModel.Id,
                    _profileViewModel.Username,
                    _profileViewModel.Email),
                Times.Once);
            _userServiceMock.Verify(
                x => x.UpdatePassword(
                    It.IsAny<Guid>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public void CanUpdatePasswordAndProfileInfo()
        {
            // arrange
            Setup();
            var controller = new UserController(
                _authServiceMock.Object,
                _userServiceMock.Object);

            // act
            var result = controller.Profile(_profileViewModel);

            // assert
            Assert.True(controller.ModelState.IsValid);
            _userServiceMock.Verify(
                x => x.UpdateUserInfo(
                    _profileViewModel.Id,
                    _profileViewModel.Username,
                    _profileViewModel.Email),
                Times.Once);
            _userServiceMock.Verify(
                x => x.UpdatePassword(
                    _profileViewModel.Id,
                    _profileViewModel.Password),
                Times.Once);
        }
    }
}
