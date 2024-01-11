using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.CreateAuthor;
using Ab_pk_task3.Aplication.BooksOperations.Commands.CreateBook;
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

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests:IClassFixture<CommonTextFicture>
    {
        
        [Theory]  // hatalı deneme yapıyorum
        [InlineData("","surname")]
        [InlineData("name","")]
        [InlineData("", "")]
        public void WhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrors(string name, string surname)
        {
            //arrange(Hazırla)
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                Birthdate = DateTime.Now.AddYears(-23),

            };

            //act  (Çalıştır )
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command.Model);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı

        }

        [Fact]
        public void WhenDateTimeEquelNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange(Hazırla)
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            // sadece datetime test edildigi için digerleri valid olmalı
            command.Model = new CreateAuthorModel()
            {
                Name = "deneme",
                Surname = "deneme",
                Birthdate = DateTime.Now
            };

            //act  (Çalıştır )
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command.Model);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı

        }

        [Fact] // Dogru Çalışma durumu
        public void WhenValidInputsIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange(Hazırla)
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            // sadece datetime test edildigi için digerleri valid olmalı
            command.Model = new CreateAuthorModel()
            {
                Name = "name34",
                Surname = "surname34",
                Birthdate = DateTime.Now.AddYears(-23),

            };

            //act  (Çalıştır )
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command.Model);

            //assert (Dogrula)
            result.Errors.Count.Should().Be(0); 

        }
    }
}
