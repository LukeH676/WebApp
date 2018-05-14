using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.data
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions<dataContext> options) :base(options){}
        public DbSet<value> values {get; set;}

        public DbSet<Users> Users {get; set;}
    }
}