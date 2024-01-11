using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.UpdateAuthor;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests: IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenValidInputsAreGiven_AuthorShouldBeUpdated()
        {
            // Arrange
            Author existingAuthor = new Author()
            {
                Name = "name",
                Surname = "surname",
                Birthdate = DateTime.Now.AddYears(-23),
            };

            _dbContext.Authors.Add(existingAuthor);
            _dbContext.SaveChanges();

            UpdateAuthorCommand updateCommand = new UpdateAuthorCommand(_dbContext);
            var existingAuthors = _dbContext.Authors.ToList();

            updateCommand.Id = existingAuthor.Id;
            updateCommand.Model = new UpdateAuthorModel()
            {
                Name = "name1",
                Surname = "surname1",
                Birthdate = DateTime.Now.AddYears(-25),
            };

            // Act
            FluentActions.Invoking(() => updateCommand.Handle()).Invoke();

            // Assert
            var updatedAuthor = _dbContext.Authors.Find(existingAuthor.Id);
            updatedAuthor.Should().NotBeNull();
            updatedAuthor.Name.Should().Be(updateCommand.Model.Name);
            updatedAuthor.Surname.Should().Be(updateCommand.Model.Surname);
            updatedAuthor.Birthdate.Should().Be(updateCommand.Model.Birthdate);
        }

        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingAuthorId = -1; // Assuming -1 is not a valid Author Id

            UpdateAuthorCommand updateCommand = new UpdateAuthorCommand(_dbContext);
            updateCommand.Id = nonExistingAuthorId;
            updateCommand.Model = new UpdateAuthorModel()
            {
                Name = "name",
                Surname = "surname",
                Birthdate = DateTime.Now.AddYears(-23),
            };

            // Act & Assert
            FluentActions.Invoking(() => updateCommand.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bulunamadı");
        }
    }
}
