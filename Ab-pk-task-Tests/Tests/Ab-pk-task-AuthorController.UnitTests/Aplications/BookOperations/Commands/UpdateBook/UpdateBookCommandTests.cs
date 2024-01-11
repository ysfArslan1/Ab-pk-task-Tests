using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.BooksOperations.Commands.UpdateBook;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests: IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenValidInputsAreGiven_BookShouldBeUpdated()
        {
            // Arrange
            Book existingBook = new Book()
            {
                Title = "ToUpdateBook",
                PageCount = 50,
                PublishDate = DateTime.Now.AddDays(-30),
                GenreId = 3,
                AuthorId = 1
            };

            _dbContext.Books.Add(existingBook);
            _dbContext.SaveChanges();

            UpdateBookCommand updateCommand = new UpdateBookCommand(_dbContext);
            var existingBooks = _dbContext.Books.ToList();

            updateCommand.Id = existingBook.Id;
            updateCommand.Model = new UpdateBookModel()
            {
                Title = "UpdatedBookTitle",
                PageCount = 75,
                PublishDate = DateTime.Now.AddDays(-15),
                GenreId = 1,
                AuthorId = 2
            };

            // Act
            FluentActions.Invoking(() => updateCommand.Handle()).Invoke();

            // Assert
            var updatedBook = _dbContext.Books.Find(existingBook.Id);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be(updateCommand.Model.Title);
            updatedBook.PageCount.Should().Be(updateCommand.Model.PageCount);
            updatedBook.PublishDate.Should().Be(updateCommand.Model.PublishDate);
            updatedBook.GenreId.Should().Be(updateCommand.Model.GenreId);
            updatedBook.AuthorId.Should().Be(updateCommand.Model.AuthorId);
        }

        [Fact]
        public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingBookId = -1; // Assuming -1 is not a valid book Id

            UpdateBookCommand updateCommand = new UpdateBookCommand(_dbContext);
            updateCommand.Id = nonExistingBookId;
            updateCommand.Model = new UpdateBookModel()
            {
                Title = "UpdatedBookTitle",
                PageCount = 75,
                PublishDate = DateTime.Now.AddDays(-15),
                GenreId = 4,
                AuthorId = 2
            };

            // Act & Assert
            FluentActions.Invoking(() => updateCommand.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }
    }
}
