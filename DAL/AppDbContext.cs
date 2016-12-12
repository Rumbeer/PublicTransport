using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using Riganti.Utils.Infrastructure.EntityFramework;


namespace DAL
{
    public class AppDbContext : DbContext
    {
        #region Ctors

        public AppDbContext() : base()
        {
            //InitializeDbContext();
        }

        public AppDbContext(string connectionName) : base(connectionName)
        {
            //InitializeDbContext();
        }

        #endregion

        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RouteStation> RouteStations { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureMembershipRebootUserAccounts<UserAccount>();

            //modelBuilder.Entity<Ticket>()
            //    .HasOptional(ticket => ticket.Questionnaire)
            //    .WithOptionalDependent(questionnaire => questionnaire.Ticket);
        }

        private void InitializeDbContext()
        {
            Database.SetInitializer(new AppDbInitializer());
            this.RegisterUserAccountChildTablesForDelete<UserAccount>();
        }
    }
}
