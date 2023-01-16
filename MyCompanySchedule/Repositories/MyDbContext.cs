using Microsoft.EntityFrameworkCore;
using MyCompanySchedule.Entities;

namespace MyCompanySchedule.Repositories
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<ToDoList> Tasks {get; set;}
        public DbSet<UserToTask> UserToTasks {get; set;}
        public DbSet<Company> Companies {get; set;}

        public MyDbContext()
        {
            this.Users = this.Set<User>();
            this.Tasks = this.Set<ToDoList>();
            this.UserToTasks = this.Set<UserToTask>();
            this.Companies = this.Set<Company>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=MyCompanyScheduleDB;Trusted_Connection=True;")
                .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Username = "KKRAST",
                    Password = "pass",
                    FirstName = "Kristina",
                    LastName = "Krasteva"
                });
        }
    }
}
