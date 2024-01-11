using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.BooksOperations.Commands.DeleteBook;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests:IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteBookCommandTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenExistingBookIdIsGiven_BookShouldBeDeleted()
        {
            // Arrange
            Book book = new Book()
            {
                Title = "ToDeleteBook",
                PageCount = 50,
                PublishDate = DateTime.Now.AddDays(-30),
                GenreId = 3,
                AuthorId = 1
            };

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            DeleteBookCommand deleteCommand = new DeleteBookCommand(_dbContext);
            deleteCommand.Id = book.Id;

            // Act
            FluentActions.Invoking(() => deleteCommand.Handle()).Invoke();

            // Assert
            var deletedBook = _dbContext.Books.FirstOrDefault(x => x.Id == book.Id);
            deletedBook.Should().BeNull();
        }
        [Fact]
        public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingBookId = -1; // Assuming -1 is not a valid book Id

            DeleteBookCommand deleteCommand = new DeleteBookCommand(_dbContext);
            deleteCommand.Id = nonExistingBookId;

            // Act & Assert
            FluentActions.Invoking(() => deleteCommand.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }
    }
}
