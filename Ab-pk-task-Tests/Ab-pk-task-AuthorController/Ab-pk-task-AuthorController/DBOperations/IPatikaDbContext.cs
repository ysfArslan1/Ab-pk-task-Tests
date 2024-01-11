using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Entities;
using Ab_pk_task_AuthorController.Entities;

namespace Ab_pk_task3.DBOperations;

public interface IPatikaDbContext 
{ 
    DbSet<Genre> Genres { get; set; }
    DbSet<Book> Books { get; set; }
    DbSet<Author> Authors { get; set; }
    int SaveChanges();

}

