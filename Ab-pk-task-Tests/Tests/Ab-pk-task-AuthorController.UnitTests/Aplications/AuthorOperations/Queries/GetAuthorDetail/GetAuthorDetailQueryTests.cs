using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.DeleteAuthor;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.UpdateAuthor;
using Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthorDetail;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests: IClassFixture<CommonTextFicture>
    {
        private readonly PatikaDbContext _dbcontext;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTextFicture textFicture)
        {
            _dbcontext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        

        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeThrown()
        {
            // Arrange
            int nonExistingAuthorId = -1; // Assuming -1 is not a valid Author Id

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbcontext,_mapper);
            query.Id = nonExistingAuthorId;

            // Act & Assert
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bulunamadı");
        }

        [Fact]
        public void WhenExistingAuthorIdIsGiven_AuthorShouldBeReturn()
        {
            // Arrange
            Author author = new Author()
            {
                Name = "name",
                Surname = "surname",
                Birthdate = DateTime.Now.AddYears(-23),
            };

            _dbcontext.Authors.Add(author);
            _dbcontext.SaveChanges();

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbcontext,_mapper);
            query.Id = author.Id;

            // Act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // Assert
            var result = _dbcontext.Authors.FirstOrDefault(x => x.Id == author.Id);
            result.Should().NotBeNull();
            result.Name.Should().Be(author.Name);
            result.Surname.Should().Be(author.Surname);
            result.Birthdate.Should().Be(author.Birthdate);
        }
    }
}
