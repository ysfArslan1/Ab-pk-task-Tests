using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.BooksOperations.Commands.DeleteBook;
using Ab_pk_task3.Aplication.BooksOperations.Commands.UpdateBook;
using Ab_pk_task3.Aplication.BooksOperations.Queries.GetBookDetail;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests: IClassFixture<CommonTextFicture>
    {
        private readonly PatikaDbContext _dbcontext;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTextFicture textFicture)
        {
            _dbcontext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        

        [Fact]
        public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingBookId = -1; // Assuming -1 is not a valid book Id

            GetBookDetailQuery query = new GetBookDetailQuery(_dbcontext,_mapper);
            query.Id = nonExistingBookId;

            // Act & Assert
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bulunamadı");
        }

        [Fact]
        public void WhenExistingBookIdIsGiven_BookShouldBeReturn()
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

            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();

            GetBookDetailQuery query = new GetBookDetailQuery(_dbcontext,_mapper);
            query.Id = book.Id;

            // Act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // Assert
            var result = _dbcontext.Books.FirstOrDefault(x => x.Id == book.Id);
            result.Should().NotBeNull();
            result.Title.Should().Be(book.Title);
            result.PageCount.Should().Be(book.PageCount);
            result.PublishDate.Should().Be(book.PublishDate);
            result.GenreId.Should().Be(book.GenreId);
            result.AuthorId.Should().Be(book.AuthorId);
        }
    }
}
