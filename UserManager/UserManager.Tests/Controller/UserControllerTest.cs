using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UserManager.Controllers;
using UserManager.DTO;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UsersController _usersController;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public UserControllerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _userServiceMock = new Mock<IUserService>();

            _usersController = new UsersController(
                _userServiceMock.Object
            );
        }


        [Fact]
        public async Task GetById_WhenUserExist_ReturnsOkObjectResult()//GetById_КогдаПользовательСуществует_ВозвращаетOkObjectResult
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new UserDTO { Id = userId, Email = "test@test.com", FirstName = "Name", LastName = "LastName" };

            _userServiceMock.Setup(ser => ser.GetUserByIdAsync(userId)).ReturnsAsync(user);

            //Act
            var result = await _usersController.GetById(userId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task GetAll_WhenNoUsers_ReturnsEmptyList() //GetAll_ПриОтсутствииПользователей_ВозвращаетПустойСписок
        {
            // Arrange
            var users = new List<UserDTO>();

            _userServiceMock
                .Setup(s => s.GetUsersAsync())
                .ReturnsAsync(users);

            // Act
            var result = await _usersController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsType<List<UserDTO>>(okResult.Value);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Empty(returnedUsers);
        }


        [Fact]
        public async Task GetById_WhenUserNotExist_ReturnsNotFound() //GetById_КогдаПользовательНе Существует_ВозвращаетNotFound
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userServiceMock
                .Setup(s => s.GetUserByIdAsync(userId))
                .ReturnsAsync((UserDTO)null!);

            // Act
            var result = await _usersController.GetById(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Create_WhenUserIsValid_ReturnsCreatedAtAction()//Create_КогдаПользовательДействителен_ВозвращаетCreatedAtAction
        {
            // Arrange
            var createDto = new CreateUserDTO
            {
                Email = "newuser@gmail.com",
                FirstName = "Alex",
                LastName = "Johnson",
                Role = "User"
            };

            var createdUser = new UserDTO
            {
                Id = Guid.NewGuid(),
                Email = createDto.Email,
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Role = createDto.Role
            };

            _userServiceMock
                .Setup(s => s.CreateAsync(createDto))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _usersController.Create(createDto);

            // Assert
            var createdResult =
                Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("GetById", createdResult.ActionName);
            Assert.Equal(createdUser.Id, createdResult.RouteValues!["id"]);
            Assert.Same(createdUser, createdResult.Value);

            _userServiceMock.Verify(
                s => s.CreateAsync(createDto), Times.Once);
        }


        [Fact]
        public async Task Delete_WhenUserExist_ReturnsNoContent()//Delete_КогдаПользовательСуществует_ВозвращаетNoContent
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userServiceMock
                .Setup(s => s.DeleteAsync(userId))
                .ReturnsAsync(true);

            // Act
            var result = await _usersController.Delete(userId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);

            Assert.Equal(204, noContentResult.StatusCode);

            _userServiceMock.Verify(
                s => s.DeleteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenUserNotExist_ReturnsNotFound()//Delete_КогдаПользовательНеСуществует_ВозвращаетNotFound
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userServiceMock
                .Setup(s => s.DeleteAsync(userId))
                .ReturnsAsync(false);

            // Act
            var result = await _usersController.Delete(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal(404, notFoundResult.StatusCode);

            _userServiceMock.Verify(
                s => s.DeleteAsync(userId), Times.Once);
        }


    }

}