using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.GenresOperations.Commands.DeleteGenre;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests:IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenExistingGenreIdIsGiven_GenreShouldBeDeleted()
        {
            // Arrange
            Genre Genre = new Genre()
            {
                Name = "ToDeleteGenre",
            };

            _dbContext.Genres.Add(Genre);
            _dbContext.SaveChanges();

            DeleteGenreCommand deleteCommand = new DeleteGenreCommand(_dbContext);
            deleteCommand.Id = Genre.Id;

            // Act
            FluentActions.Invoking(() => deleteCommand.Handle()).Invoke();

            // Assert
            var deletedGenre = _dbContext.Genres.FirstOrDefault(x => x.Id == Genre.Id);
            deletedGenre.Should().BeNull();
        }
        [Fact]
        public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingGenreId = -1; // Assuming -1 is not a valid Genre Id

            DeleteGenreCommand deleteCommand = new DeleteGenreCommand(_dbContext);
            deleteCommand.Id = nonExistingGenreId;

            // Act & Assert
            FluentActions.Invoking(() => deleteCommand.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre Bulunamadı");
        }
    }
}
