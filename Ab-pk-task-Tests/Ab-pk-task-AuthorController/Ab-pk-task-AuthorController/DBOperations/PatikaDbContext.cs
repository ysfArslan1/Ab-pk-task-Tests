using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Entities;
using Ab_pk_task_AuthorController.Entities;

namespace Ab_pk_task3.DBOperations;

public class PatikaDbContext : DbContext, IPatikaDbContext
{
    public PatikaDbContext(DbContextOptions<PatikaDbContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}

