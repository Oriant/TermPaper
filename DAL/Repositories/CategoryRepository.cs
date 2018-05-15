﻿using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class CategoryRepository : IRepository<Category>
	{
		private AuctionContext db;
		public CategoryRepository(AuctionContext context)
		{
			this.db = context;
		}
		public void Create(Category item)
		{
			db.Categories.Add(item);
		}

		public void Delete(int id)
		{
			Category category = db.Categories.Find(id);
			if(category != null)
			{
				db.Categories.Remove(category);
			}
		}

		public IEnumerable<Category> Find(Func<Category, bool> predicate)
		{
			return db.Categories.Where(predicate).ToList();
		}

		public Category Get(int id)
		{
			return db.Categories.Find(id);
		}

		public IEnumerable<Category> GetAll()
		{
			return db.Categories;
		}

		public void Update(Category item)
		{
			db.Entry(item).State = EntityState.Modified;
		}
	}
}