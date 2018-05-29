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
	public class BidServiceTest
	{
		static BidServiceTest()
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
		public void ShuldReturnTrueIfGetBidsWork()
		{
			//Arrange
			Bid one = new Bid { Sum = 10, Id = 1 };
			BiddingDTO two = new BiddingDTO { Sum = 10, Id = 1 };

			Mock<IRepository<Bid>> repositoryMock = new Mock<IRepository<Bid>>();
			repositoryMock.Setup(a => a.GetAll()).Returns(new List<Bid>() { one });

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(uow => uow.Biddings).Returns(repositoryMock.Object);

			var bidService = new BidService(uowMock.Object);

			List<BiddingDTO> expected = new List<BiddingDTO>();
			expected.Add(two);



			//Act
			List<BiddingDTO> actual = (List<BiddingDTO>)bidService.GetBids();

			//Assert
			Assert.IsTrue(expected.SequenceEqual(actual, new BiddingDtoEqualityComparer()));
		}
	}
}
