using Microsoft.EntityFrameworkCore;
using RESTWebAPI.Models;

namespace RESTWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
