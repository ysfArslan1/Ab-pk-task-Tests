using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.GenresOperations.Commands.DeleteGenre;
using Ab_pk_task3.Aplication.GenresOperations.Commands.UpdateGenre;
using Ab_pk_task3.Aplication.GenresOperations.Queries.GetGenreDetail;
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

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests: IClassFixture<CommonTextFicture>
    {
        private readonly PatikaDbContext _dbcontext;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTextFicture textFicture)
        {
            _dbcontext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        

        [Fact]
        public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingGenreId = -1; // Assuming -1 is not a valid Genre Id

            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbcontext,_mapper);
            query.Id = nonExistingGenreId;

            // Act & Assert
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bulunamadı");
        }

        [Fact]
        public void WhenExistingGenreIdIsGiven_GenreShouldBeReturn()
        {
            // Arrange
            Genre Genre = new Genre()
            {
                Name = "deneme",
            };

            _dbcontext.Genres.Add(Genre);
            _dbcontext.SaveChanges();

            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbcontext,_mapper);
            query.Id = Genre.Id;

            // Act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // Assert
            var result = _dbcontext.Genres.FirstOrDefault(x => x.Id == Genre.Id);
            result.Should().NotBeNull();
            result.Name.Should().Be(Genre.Name);
        }
    }
}
