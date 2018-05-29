using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

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
				MapperConfig.Initialize()
				);
			}
			catch { }
		}
		
		/*
		[TestMethod]
		public void GetCategoriesTest()
		{
			//Arrange
			var categoryRepositoryMock = new Mock<IRepository<Category>();
			categoryRepositoryMock.Setup(a => a.GetAll()).Returns(new List<Category>()

			{
				new Category {  Name = "Name1" },
                new Category {  Name = "Name2" }

			});

			var uowMock = new Mock<IUnitOfWork>();
			uowMock.Setup(uow => uow.Categories).Returns(categoryRepositoryMock.Object);

			var service = new CategoryService(uowMock.Object);

			//Act
			var actual = service.GetCategories();

			//Assert
			Microsoft.VisualStudio.TestTools.UnitTesting.Assert.List(new List<string>) { "Все", "Gonduras", "Nigeria" }, actual);
		}
		*/
	}

}

