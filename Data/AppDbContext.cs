using Microsoft.EntityFrameworkCore;
using StudentManagementWithJWT.Models;

namespace StudentManagementWithJWT.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Student> tbl_Students { get; set; }
    }
}
