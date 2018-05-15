﻿using DAL.Entities;
using DAL.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class AuctionContext : IdentityDbContext<ApplicationUser>, DbContext
    {
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
		public DbSet<Lot> Lots { get; set; }

        static AuctionContext()
        {
            Database.SetInitializer(new AuctionDbInitializer());
        }

        public AuctionContext(string connectionString) : base(connectionString) { }
    }
}
