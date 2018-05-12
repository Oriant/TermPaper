﻿using DAL.Entities;
using DAL.Identity.Entities;
using DAL.Identity.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class AuctionDbInitializer : DropCreateDatabaseAlways<AuctionContext>
    {
        protected override void Seed(AuctionContext db)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var adminRole = new IdentityRole { Name = "admin" };
            var userRole = new IdentityRole { Name = "user" };
            var managerRole = new IdentityRole { Name = "manager" };

            roleManager.Create(adminRole);
            roleManager.Create(userRole);
            roleManager.Create(managerRole);

            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            string adminPassword = "Admin_1234";
            userManager.Create(admin, adminPassword);

            var manager = new ApplicationUser { Email = "mngr@gmail.com", UserName = "manager@gmail.com" };
            string managerPassword = "Manager_1234";
            userManager.Create(manager, managerPassword);

            var user = new ApplicationUser { Email = "user@gmail.com", UserName = "user@gmail.com" };
            string userPassword = "User_1234";
            userManager.Create(user, userPassword);

            userManager.AddToRole(admin.Id, adminRole.Name);
            userManager.AddToRole(admin.Id, userRole.Name);

            userManager.AddToRole(manager.Id, managerRole.Name);
            userManager.AddToRole(manager.Id, userRole.Name);

            userManager.AddToRole(user.Id, userRole.Name);

            base.Seed(db);
        }
    }
}
