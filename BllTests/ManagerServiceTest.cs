using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services;
using DAL.Entities;
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
	public class ManagerServiceTest
	{
		static ManagerServiceTest()
		{
			try
			{
				Mapper.Initialize(cfg =>
				MapperConfig.Configure(cfg)
				);
			}
			catch { }
		}
		[TestMethod]
		public void ShouldThrowExceptionIfConfirmedLotNull()
		{
			//Arrange
			Mock<IRepository<Lot>> repositoryMock = new Mock<IRepository<Lot>>();
			repositoryMock.Setup(a => a.Get(It.IsAny<int>())).Returns<Lot>(null);


			var unitOfWorkMock = new Mock<IUnitOfWork>();
			unitOfWorkMock.Setup(uow => uow.Lots).Returns(repositoryMock.Object);

			var managerService = new ManagerService(unitOfWorkMock.Object);

			//Act&assert
			Assert.ThrowsException<DataValidationException>(() => managerService.ConfirmLot(It.IsAny<int>()));
		}

		[TestMethod]
		public void ShouldThrowExceptionIfDeclinedLotNull()
		{
			Mock<IRepository<Lot>> repositoryMock = new Mock<IRepository<Lot>>();
			repositoryMock.Setup(a => a.Get(It.IsAny<int>())).Returns<Lot>(null);


			var unitOfWorkMock = new Mock<IUnitOfWork>();
			unitOfWorkMock.Setup(uow => uow.Lots).Returns(repositoryMock.Object);

			var managerService = new ManagerService(unitOfWorkMock.Object);

			Assert.ThrowsException<DataValidationException>(() => managerService.DeclineLot(It.IsAny<int>()));
		}

		[TestMethod]
		public void ShuldReturnTrueIfGetGetUnconfirmedLotsWork()
		{
			//Arrange
			Lot one = new Lot { Name = "a", Id = 1, IsConfirmed = false };
			LotDTO two = new LotDTO { Name = "a", Id = 1, IsConfirmed = false };

			Mock<IRepository<Lot>> repositoryMock = new Mock<IRepository<Lot>>();
			repositoryMock.Setup(a => a.GetAll()).Returns(new List<Lot>() { one });

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(uow => uow.Lots).Returns(repositoryMock.Object);

			var managerService = new ManagerService(uowMock.Object);

			List<LotDTO> expected = new List<LotDTO>();
			expected.Add(two);



			//Act
			List<LotDTO> actual = (List<LotDTO>)managerService.GetUnconfirmedLots();

			//Assert
			Assert.IsTrue(expected.SequenceEqual(actual, new LotDtoEqualityComparer()));
		}
	}

}
