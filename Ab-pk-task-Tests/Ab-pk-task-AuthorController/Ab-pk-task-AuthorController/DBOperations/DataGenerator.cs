using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Entities;
using Ab_pk_task_AuthorController.Entities;

namespace Ab_pk_task3.DBOperations;

public class DataGenerator
{
    //inmemory de Student sınıfı verilerini program çalıştığında üretmek için kullanılıyor
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var content = new PatikaDbContext(serviceProvider.GetRequiredService<DbContextOptions<PatikaDbContext>>())) 
        {
            if (!content.Authors.Any())
            {
                content.Authors.AddRange(
                    new Author
                    {
                        Name = "A--",
                        Surname = "S--",
                        Birthdate = DateTime.Now.AddYears(-19),
                    }, new Author
                    {
                        Name = "B--",
                        Surname = "F--",
                        Birthdate = DateTime.Now.AddYears(-24),
                    }, new Author
                    {
                        Name = "H--",
                        Surname = "C--",
                        Birthdate = DateTime.Now.AddYears(-33),
                    }
                );
                content.SaveChanges();
            }

            

            if (!content.Genres.Any())
            {
                content.Genres.AddRange(
                    new Genre
                    {
                        Name = "PersonalGrowty"
                    }, new Genre
                    {
                        Name = "ScienceFiction"
                    }, new Genre
                    {
                        Name = "Noval"
                    }, new Genre
                    {
                        Name = "Romance"
                    }
                );
                content.SaveChanges();
            }

            

            if (!content.Books.Any())
            {
                content.Books.AddRange(
                   new Book
                   {
                       Title = "book1",
                       PageCount = 111,
                       GenreId = 1,
                       AuthorId = 1,
                       PublishDate = DateTime.Now.AddYears(-5),
                   },
                   new Book
                   {
                       Title = "book2",
                       PageCount = 222,
                       GenreId = 1,
                       AuthorId = 2,
                       PublishDate = DateTime.Now.AddYears(-3),
                   },
                   new Book
                   {
                       Title = "book3",
                       PageCount = 333,
                       GenreId = 2,
                       AuthorId = 3,
                       PublishDate = DateTime.Now.AddYears(-7),
                   },
                   new Book
                   {
                       Title = "book4",
                       PageCount = 334,
                       GenreId = 3,
                       AuthorId = 2,
                       PublishDate = DateTime.Now.AddYears(-9)
                   }
               );

                content.SaveChanges();
            }

           
        }
    }
}

