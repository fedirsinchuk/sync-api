using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sync.DAL.Entities;

namespace Sync.DAL;

public class SyncDataBaseContext : DbContext
{
    public SyncDataBaseContext(DbContextOptions<SyncDataBaseContext> options ) : base(options) { }
    
    public DbSet<Data> Actions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Data>()
            .HasData(
                new Data { Id = 1, Name = "Test 1" , Values = " "},
                new Data { Id = 2, Name = "Test 2" , Values = " "},
                new Data { Id = 3, Name = "Test 3" , Values = " "},
                new Data { Id = 4, Name = "Test 4" , Values = " "},
                new Data { Id = 5, Name = "Test 5" , Values = " "},
                new Data { Id = 6, Name = "Test 6" , Values = " "}
            );
    }
}