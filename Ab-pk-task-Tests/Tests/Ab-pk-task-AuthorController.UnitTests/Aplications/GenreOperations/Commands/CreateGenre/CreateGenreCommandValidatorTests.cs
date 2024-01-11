using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.GenresOperations.Commands.CreateGenre;
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

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests:IClassFixture<CommonTextFicture>
    {
        
        [Theory]  // hatalı deneme yapıyorum
        [InlineData("")]
        [InlineData("WhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrorsWhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrors")]
        public void WhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrors(string Name)
        {
            //arrange(Hazırla)
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel() { 
                Name = Name,
            };

            //act  (Çalıştır )
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command.Model);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı

        }

        [Fact] // Dogru Çalışma durumu
        public void WhenValidInputsIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange(Hazırla)
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            // sadece datetime test edildigi için digerleri valid olmalı
            command.Model = new CreateGenreModel()
            {
                Name = "deneme",
            };

            //act  (Çalıştır )
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command.Model);

            //assert (Dogrula)
            result.Errors.Count.Should().Be(0); 

        }
    }
}
