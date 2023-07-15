using Hotelier.Domain.Entities;
using Hotelier.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Common.Interfaces;

public interface IApplicationContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Subscribe> Subscribes { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
   
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}