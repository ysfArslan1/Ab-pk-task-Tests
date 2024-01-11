using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.GenresOperations.Commands.UpdateGenre;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests: IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenValidInputsAreGiven_GenreShouldBeUpdated()
        {
            // Arrange
            Genre existingGenre = new Genre()
            {
                Name = "ToUpdateGenre",
            };

            _dbContext.Genres.Add(existingGenre);
            _dbContext.SaveChanges();

            UpdateGenreCommand updateCommand = new UpdateGenreCommand(_dbContext);
            var existingGenres = _dbContext.Genres.ToList();

            updateCommand.Id = existingGenre.Id;
            updateCommand.Model = new UpdateGenreModel()
            {
                Name = "UpdatedGenreTitle",
            };

            // Act
            FluentActions.Invoking(() => updateCommand.Handle()).Invoke();

            // Assert
            var updatedGenre = _dbContext.Genres.Find(existingGenre.Id);
            updatedGenre.Should().NotBeNull();
            updatedGenre.Name.Should().Be(updateCommand.Model.Name);
        }

        [Fact]
        public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingGenreId = -1; // Assuming -1 is not a valid Genre Id

            UpdateGenreCommand updateCommand = new UpdateGenreCommand(_dbContext);
            updateCommand.Id = nonExistingGenreId;
            updateCommand.Model = new UpdateGenreModel()
            {
                Name = "UpdatedGenreTitle",
            };

            // Act & Assert
            FluentActions.Invoking(() => updateCommand.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre Bulunamadı");
        }
    }
}
