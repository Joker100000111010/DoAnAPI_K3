﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebAPI_DoAn.Data
{
    public class Shop_NhanVienDbContextcs : IdentityDbContext
    {
        public Shop_NhanVienDbContextcs(DbContextOptions<Shop_NhanVienDbContextcs> options): 
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "004c7e80-7dfc-44be-8952-2c7130898655";
            var writeRoleId = "71e282d3-76ca-485e-b094-eff019287fa5";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Read",
                    NormalizedName = "READ"
                },
                new IdentityRole
                {
                    Id = writeRoleId,
                    ConcurrencyStamp = writeRoleId,
                    Name = "Write",
                    NormalizedName = "WRITE"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
