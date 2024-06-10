using Microsoft.EntityFrameworkCore;
using TechnicalTest.Infrastructure.Entities;

namespace TechnicalTest.Infrastructure.Data
{
    public class DataEntities : DbContext
    {
        public DataEntities(DbContextOptions<DataEntities> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerState> States { get; set; }
        public DbSet<CustomerLGA> CustomerLGAs { get; set; }
        public DbSet<OtpLog> OtpLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Make sure every customer phone number is unique
            builder.Entity<Customer>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();


            #region Data Seeding
            builder.Entity<CustomerState>()
                    .HasData(
            new CustomerState { Id = 1, StateName = "Lagos", StateDescription = "Centre of exellence" },
            new CustomerState { Id = 2, StateName = "Ogun", StateDescription = "God's own state" },
            new CustomerState { Id = 3, StateName = "Oyo", StateDescription = "Owsome state" },
            new CustomerState { Id = 4, StateName = "Akwa Ibom", StateDescription = "Land of promise" },
            new CustomerState { Id = 5, StateName = "Delta", StateDescription = "Some description" });

            builder.Entity<CustomerLGA>()
                .HasData(
            new CustomerLGA { Id = 1, LGAName = "Kosofe", CustomerStateId = 1, LGADescription = "Some description" },
            new CustomerLGA { Id = 2, LGAName = "Ikorodu", CustomerStateId = 1, LGADescription = "Some description" },
            new CustomerLGA { Id = 3, LGAName = "Ikono", CustomerStateId = 4, LGADescription = "Some description" },
            new CustomerLGA { Id = 4, LGAName = "Asaba", CustomerStateId = 5, LGADescription = "Some description" },
            new CustomerLGA { Id = 5, LGAName = "Ibadan", CustomerStateId = 2, LGADescription = "Some description" },
            new CustomerLGA { Id = 6, LGAName = "Oyo", CustomerStateId = 3, LGADescription = "Some description" });
            #endregion
        }
    }
}
