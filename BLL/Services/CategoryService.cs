using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class CategoryService : ICategoryService
	{
		IUnitOfWork Database { get; set; }

		public CategoryService(IUnitOfWork unitOfWork)
		{
			Database = unitOfWork;
		}

		public CategoryDTO GetCategory(int? id)
		{
			if (id == null)
				throw new ValidationException("Category ID undefined");

			var category = Database.Categories.Get(id.Value);

			if (category == null)
				throw new ValidationException("Category not found");

			return new CategoryDTO { Id = category.Id, Name = category.Name };
		}

		public IEnumerable<CategoryDTO> GetCategories()
		{
			var mapper = new MapperConfiguration(cfg =>
			cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

			return mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
		}

		public void Dispose()
		{
			Database.Dispose();
		}
	}
}
