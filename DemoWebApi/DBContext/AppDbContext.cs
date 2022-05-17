﻿using DemoWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> builder) : base (builder)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasData(new User
               {
                   Id = 1,
                   UserName = "user1",
                   Password = "user1"
               },
                new User
                {
                    Id = 2,
                    UserName = "user2",
                    Password = "user2"
                },
                new User
                {
                    Id = 3,
                    UserName = "user3",
                    Password = "user3"
                }
                );
        }
    }
}
