using Microsoft.EntityFrameworkCore;
using System.Linq;
 
namespace plane.Models
{
    public class planeContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public planeContext(DbContextOptions<planeContext> options) : base(options) { }

	public DbSet<User> users {get; set;}

    public DbSet<Fan> fans {get; set;}
    public DbSet<Post> posts {get; set;}

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
        // modelBuilder.Entity<AttendeesList>()
        //     .HasOne(act => act.activity)
        //     .WithMany(u => u.attendeesList)
        //     .HasForeignKey(a => a.activityID)
        //     .OnDelete(DeleteBehavior.SetNull);

        // modelBuilder.Entity<AttendeesList>()
        //     .HasOne(u => u.user)
        //     .WithMany(al => al.attendeesList)
        //     .HasForeignKey(fk => fk.user)
        //     .OnDelete(DeleteBehavior.ClientSetNull);
    // }
    }
}
