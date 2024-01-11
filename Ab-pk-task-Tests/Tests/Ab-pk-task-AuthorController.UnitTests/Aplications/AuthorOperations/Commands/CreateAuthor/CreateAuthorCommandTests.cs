using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.CreateAuthor;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests:IClassFixture<CommonTextFicture>
    {
        private readonly PatikaDbContext _dbcontext;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTextFicture textFicture)
        {
            _dbcontext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(Hazırla)
            Author author = new Author()
            {
                Name = "name",
                Surname = "surname",
                Birthdate = DateTime.Now.AddYears(-23),
                
            };
            _dbcontext.Authors.Add(author);
            _dbcontext.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_dbcontext, _mapper);
            command.Model = new CreateAuthorModel() { Name = author.Name,Surname=author.Surname,Birthdate=author.Birthdate};

            //act & assert (Çalıştır & Dogrula)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Zaten Mevcut");
        }

        [Fact]
        public void WhenValidInputsIsGiven_Author_ShouldBeCreated()
        {
            //arrange(Hazırla)
            
            CreateAuthorCommand command = new CreateAuthorCommand(_dbcontext, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = "name12",
                Surname = "surname12",
                Birthdate = DateTime.Now.AddYears(-23),

            };
            // act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // assert 
            var Author = _dbcontext.Authors.FirstOrDefault(x=>x.Name == command.Model.Name);
            Author.Should().NotBeNull();
            Author.Name.Should().Be(command.Model.Name);
            Author.Surname.Should().Be(command.Model.Surname);
            Author.Birthdate.Should().Be(command.Model.Birthdate);
        }
    }
}
