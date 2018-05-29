using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BllTests
{
	[TestClass]
	public class CategoryServiceTest
	{
		static CategoryServiceTest()
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
		public void ShouldReturnTrueIfGetCategory()
		{
			//Arrange

			string actual = "a";

			Mock<IRepository<Category>> repositoryMock = new Mock<IRepository<Category>>();
			repositoryMock.Setup(a => a.Get(It.IsAny<int>())).Returns(new Category { Name = "a" });


			var unitOfWorkMock = new Mock<IUnitOfWork>();
			unitOfWorkMock.Setup(uow => uow.Categories).Returns(repositoryMock.Object);


			//Act
			var categoryService = new CategoryService(unitOfWorkMock.Object);
			var expected = categoryService.GetCategory(It.IsAny<int>()).Name;


			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ShuldReturnTrueIfGetCategoriesWork()
		{
			//Arrange
			Category one = new Category { Name = "a", Id = 1 };
			CategoryDTO two = new CategoryDTO { Name = "a", Id = 1 };

			Mock<IRepository<Category>> repositoryMock = new Mock<IRepository<Category>>();
			repositoryMock.Setup(a => a.GetAll()).Returns(new List<Category>() { one });

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(uow => uow.Categories).Returns(repositoryMock.Object);

			var categoryService = new CategoryService(uowMock.Object);

			List<CategoryDTO> expected = new List<CategoryDTO>();
			expected.Add(two);



			//Act
			List<CategoryDTO> actual = (List<CategoryDTO>)categoryService.GetCategories();

			//Assert
			Assert.IsTrue(expected.SequenceEqual(actual, new CategoryDtoEqualityComparer()));
		}
		
	}

}

