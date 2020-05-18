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
    }
}
