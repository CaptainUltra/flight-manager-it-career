using FlightManager.Data.Models;
using FlightsManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PassengerReservation> PassengersReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region IdentityRole seeding
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Admin"
                },
                new IdentityRole
                {
                    Name = "Employee"
                }
            );
            #endregion

            modelBuilder.Entity<Passenger>()
                .HasIndex(b => b.PersonalNo)
                .IsUnique();

            modelBuilder.Entity<PassengerReservation>()
            .HasKey(x => new { x.PassengerId, x.ReservationId });

            modelBuilder.Entity<PassengerReservation>()
                .HasOne(pr => pr.Passenger)
                .WithMany(p => p.Reservations)
                .HasForeignKey(pr => pr.PassengerId);

            modelBuilder.Entity<PassengerReservation>()
                .HasOne(pr => pr.Reservation)
                .WithMany(r => r.Passengers)
                .HasForeignKey(pr => pr.PassengerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
