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
	public class LotServiceTest
	{
		static LotServiceTest()
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
		public void ShouldReturnTrueIfGetLot()
		{
			//Arrange

			string actual = "a";

			Mock<IRepository<Lot>> repositoryMock = new Mock<IRepository<Lot>>();
			repositoryMock.Setup(a => a.Get(It.IsAny<int>())).Returns(new Lot { Name = "a" });


			var unitOfWorkMock = new Mock<IUnitOfWork>();
			unitOfWorkMock.Setup(uow => uow.Lots).Returns(repositoryMock.Object);


			//Act
			var lotService = new LotService(unitOfWorkMock.Object);
			var expected = lotService.GetLotById(It.IsAny<int>()).Name;


			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ShuldReturnTrueIfGetLotsWork()
		{
			//Arrange
			Lot one = new Lot { Name = "a", Id = 1 };
			LotDTO two = new LotDTO { Name = "a", Id = 1 };

			Mock<IRepository<Lot>> repositoryMock = new Mock<IRepository<Lot>>();
			repositoryMock.Setup(a => a.GetAll()).Returns(new List<Lot>() { one });

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(uow => uow.Lots).Returns(repositoryMock.Object);

			var lotService = new LotService(uowMock.Object);

			List<LotDTO> expected = new List<LotDTO>();
			expected.Add(two);



			//Act
			List<LotDTO> actual = (List<LotDTO>)lotService.GetLots();

			//Assert
			Assert.IsTrue(expected.SequenceEqual(actual, new LotDtoEqualityComparer()));
		}

	}

}
