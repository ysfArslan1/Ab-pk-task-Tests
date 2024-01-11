using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.TestsSetup
{
    public static class Books
    {
        public static void AddBooks(this PatikaDbContext content)
        {
            content.Books.AddRange(
            new Book{Title = "book1", PageCount = 111, GenreId = 1, AuthorId = 1, PublishDate = DateTime.Now.AddYears(-5),},
            new Book {Title = "book2",PageCount = 222,GenreId = 1,AuthorId = 2, PublishDate = DateTime.Now.AddYears(-3),},
            new Book {Title = "book3",PageCount = 333,GenreId = 2,AuthorId = 3,PublishDate = DateTime.Now.AddYears(-7), },
            new Book{Title = "book4", PageCount = 334,GenreId = 3, AuthorId = 2,PublishDate = DateTime.Now.AddYears(-9) });
        }
    }
}
