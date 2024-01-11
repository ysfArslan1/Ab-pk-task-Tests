using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.DeleteAuthor;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests:IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenExistingAuthorIdIsGiven_AuthorShouldBeDeleted()
        {
            // Arrange
            Author author = new Author()
            {
                Name = "ToDeleteAuthor",
                Surname = "ToDeleteAuthor",
                Birthdate = DateTime.Now.AddYears(-19),
            };

            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();

            DeleteAuthorCommand deleteCommand = new DeleteAuthorCommand(_dbContext);
            deleteCommand.Id = author.Id;

            // Act
            FluentActions.Invoking(() => deleteCommand.Handle()).Invoke();

            // Assert
            var deletedAuthor = _dbContext.Authors.FirstOrDefault(x => x.Id == author.Id);
            deletedAuthor.Should().BeNull();
        }
        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingAuthorId = -1; // Assuming -1 is not a valid Author Id

            DeleteAuthorCommand deleteCommand = new DeleteAuthorCommand(_dbContext);
            deleteCommand.Id = nonExistingAuthorId;

            // Act & Assert
            FluentActions.Invoking(() => deleteCommand.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bulunamadı");
        }

        [Fact]
        public void WhengAuthorHasBook_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            Author author = new Author()
            {
                Name = "ToDeleteAuthor",
                Surname = "ToDeleteAuthor",
                Birthdate = DateTime.Now.AddYears(-19),
            };

            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();

            // Arrange
            Book item = new Book()
            {
                Title = "deneme",
                PageCount = 22,
                PublishDate = DateTime.Now.AddDays(123),
                GenreId = 1,
                AuthorId = author.Id,
            };

            _dbContext.Books.Add(item);
            _dbContext.SaveChanges();

            DeleteAuthorCommand deleteCommand = new DeleteAuthorCommand(_dbContext);
            deleteCommand.Id = author.Id;

            // Act & Assert
            FluentActions.Invoking(() => deleteCommand.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Bu yazarın kitabı Databasede yer almakta, Lütfen önce kitabı silin");
        }
    }
}
