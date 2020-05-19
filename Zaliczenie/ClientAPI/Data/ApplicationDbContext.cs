using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClientAPI.Models;

namespace ClientAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ClientAPI.Models.EmployeeDepartment> EmployeeDepartment { get; set; }
        public DbSet<ClientAPI.Models.Employees> Employees { get; set; }
        public DbSet<ClientAPI.Models.EmployeeGrade> EmployeeGrade { get; set; }
        public DbSet<ClientAPI.Models.EmployeePosition> EmployeePosition { get; set; }
        public DbSet<ClientAPI.Models.ItemCategory> ItemCategory { get; set; }
        public DbSet<ClientAPI.Models.ItemType> ItemType { get; set; }
        public DbSet<ClientAPI.Models.Item> Item { get; set; }
        public DbSet<ClientAPI.Models.Transaction> Transactions { get; set; }
    }
}
