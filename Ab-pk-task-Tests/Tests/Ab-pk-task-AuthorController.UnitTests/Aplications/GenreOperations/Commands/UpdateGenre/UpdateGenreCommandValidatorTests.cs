using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.GenresOperations.Commands.UpdateGenre;
using Ab_pk_task3.Aplication.GenresOperations.Commands.DeleteGenre;
using Ab_pk_task3.Aplication.GenresOperations.Commands.UpdateGenre;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.GenreOperations.Commands.DeleteGenre
{
    public class UpdateGenreCommandValidatorTests:IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateGenreCommandValidatorTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Theory]
        [InlineData("",true)]
        [InlineData("deneme",null )]
        [InlineData("WhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrorsWhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrors",true)]
        public void WhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrors(string name, bool isActive)
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Id = _dbContext.Genres.First().Id;
            command.Model = new UpdateGenreModel()
            {
                Name = name,
                isActive=isActive
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı
        }

        

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Id = _dbContext.Genres.First().Id;
            // Only DateTime is being tested, others should be valid
            command.Model = new UpdateGenreModel()
            {
                Name = "UpdatedGenreTitle",
                isActive = true,
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            //assert (Dogrula)
            result.Errors.Count.Should().Be(0);
        }
    }
}
