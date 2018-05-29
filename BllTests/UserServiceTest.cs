using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services;
using DAL.Entities;
using DAL.Identity.Interfaces;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BllTests
{
	[TestClass]
	public class UserServiceTest
	{
		static UserServiceTest()
		{
			try
			{
				Mapper.Initialize(cfg =>
				MapperConfig.Initialize()
				);
			}
			catch { }
		}

		[TestMethod]
		public void ShouldReturnTrueIfGetUser()
		{
			//Arrange

			string actual = "a";
			string id = "1";
			Mock<IRepository<User>> repositoryMock = new Mock<IRepository<User>>();
			repositoryMock.Setup(a => a.Get(id)).Returns(new User { Name = "a" });


			var unitOfWorkMock = new Mock<IUnitOfWork>();
			unitOfWorkMock.Setup(uow => uow.Users).Returns(repositoryMock.Object);


			//Act
			var userService = new UserService(unitOfWorkMock.Object);
			var expected = userService.GetUserById(id).Name;


			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ShuldReturnTrueIfGetUsersWork()
		{
			//Arrange
			User one = new User { Name = "a", Id = "1"};
			UserDTO two = new UserDTO { Name = "a", Id = "1" };

			Mock<IRepository<User>> repositoryMock = new Mock<IRepository<User>>();
			repositoryMock.Setup(a => a.GetAll()).Returns(new List<User>() { one });

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(uow => uow.Users).Returns(repositoryMock.Object);

			var userService = new UserService(uowMock.Object);

			List<UserDTO> expected = new List<UserDTO>();
			expected.Add(two);



			//Act
			List<UserDTO> actual = (List<UserDTO>)userService.GetUsers();

			//Assert
			Assert.IsTrue(expected.SequenceEqual(actual, new UserDtoEqualityComparer()));
		}


	}
}
