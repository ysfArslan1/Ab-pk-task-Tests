using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.GenresOperations.Commands.CreateGenre;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests:IClassFixture<CommonTextFicture>
    {
        private readonly PatikaDbContext _dbcontext;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTextFicture textFicture)
        {
            _dbcontext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(Hazırla)
            Genre Genre = new Genre()
            {
                Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn",
                
            };
            _dbcontext.Genres.Add(Genre);
            _dbcontext.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_dbcontext, _mapper);
            command.Model = new CreateGenreModel() { Name = Genre.Name};

            //act & assert (Çalıştır & Dogrula)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Zaten Mevcut");
        }

        [Fact]
        public void WhenValidInputsIsGiven_Genre_ShouldBeCreated()
        {
            //arrange(Hazırla)
            
            CreateGenreCommand command = new CreateGenreCommand(_dbcontext, _mapper);
            command.Model = new CreateGenreModel() { Name = "deneme1234"};
            // act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // assert 
            var Genre = _dbcontext.Genres.FirstOrDefault(x=>x.Name == command.Model.Name);
            Genre.Should().NotBeNull();
            Genre.Name.Should().Be(command.Model.Name);
        }
    }
}
