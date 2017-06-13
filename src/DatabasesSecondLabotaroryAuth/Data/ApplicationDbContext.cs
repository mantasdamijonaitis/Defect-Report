using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DatabasesSecondLabotaroryAuth.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.EntityFrameworkCore.Extensions;
using DAL.Models;

namespace DatabasesSecondLabotaroryAuth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext Create(DbContextFactoryOptions options)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseMySQL("Data Source=localhost;port=3306;Initial Catalog=test; Max Pool Size=1000; SslMode=None");

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }

        public DbSet<DAL.Models.MeasureUnit> MeasureUnit { get; set; }

        public DbSet<DAL.Models.Material> Material { get; set; }

        public DbSet<DAL.Models.CompanyType> CompanyType { get; set; }

        public DbSet<DAL.Models.Company> Company { get; set; }

        public DbSet<DAL.Models.Vehicle> Vehicle { get; set; }

        public DbSet<DAL.Models.Defect> Defect { get; set; }

    }
}
