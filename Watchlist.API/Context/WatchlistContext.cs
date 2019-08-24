using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchlist.API.Entities;

namespace Watchlist.API.Context
{
    public class WatchlistContext : DbContext
    {
        public DbSet<Entities.Watchlist> Watchlists { get; set; }
        public DbSet<WatchlistMedia> WatchlistsMedias { get; set; }

        public WatchlistContext(DbContextOptions<WatchlistContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Watchlist>().HasData(
                new Entities.Watchlist()
                {
                    Id = Guid.Parse("C3E95C44-1E39-4328-9D52-E0291DDF2DA7"),
                    Name = "WatchlaterPlaylist",
                    Userid = Guid.Parse("434C538E-7AED-42D7-A667-92B0760EF88C") //Milo userId
                },
                new Entities.Watchlist()
                {
                    Id = Guid.Parse("34487500-BA60-4962-9342-907C4665681F"),
                    Name = "WatchlaterPlaylist",
                    Userid = Guid.Parse("36DE8E2F-E3FF-4718-A2E8-C9FC08610728")
                },
                new Entities.Watchlist()
                {
                    Id = Guid.Parse("1C389E4C-0272-48EA-B260-6B29C0C51AD7"),
                    Name = "WatchlaterPlaylist",
                    Userid = Guid.Parse("443ED08C-E98A-4AC1-84ED-FEE7B27513F9")
                }
            );

            modelBuilder.Entity<WatchlistMedia>().HasData(
                new WatchlistMedia()
                {
                    Id = Guid.NewGuid(),
                    WatchlistId = Guid.Parse("C3E95C44-1E39-4328-9D52-E0291DDF2DA7"),
                    MediaId = Guid.Parse("4259cac2-fcf2-4ca2-b311-813bc291c2ce"),

                },
                 new WatchlistMedia()
                 {
                     Id = Guid.NewGuid(),
                     WatchlistId = Guid.Parse("C3E95C44-1E39-4328-9D52-E0291DDF2DA7"),
                     MediaId = Guid.Parse("B79A54F9-45A2-421B-735E-08D713CEC375"),

                 },
                 new WatchlistMedia()
                 {
                     Id = Guid.NewGuid(),
                     WatchlistId = Guid.Parse("C3E95C44-1E39-4328-9D52-E0291DDF2DA7"),
                     MediaId = Guid.Parse("8D81A6F1-F933-429A-91FB-7D38CD54B142"),

                 }

             );

            base.OnModelCreating(modelBuilder);
        }


    }
}
