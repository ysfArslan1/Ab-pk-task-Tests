using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.UpdateAuthor;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.DeleteAuthor;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.UpdateAuthor;
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

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.AuthorOperations.Commands.DeleteAuthor
{
    public class UpdateAuthorCommandValidatorTests:IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandValidatorTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Theory]
        [InlineData("", "surname")]
        [InlineData("name", "")]
        [InlineData("", "")]
        public void WhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrors(string name, string surname)
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Id = _dbContext.Authors.First().Id;
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                Birthdate = DateTime.Now.AddYears(-23),
            };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı
        }

        

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Id = _dbContext.Authors.First().Id;
            // Only DateTime is being tested, others should be valid
            command.Model = new UpdateAuthorModel()
            {
                Name = "name",
                Surname = "surname",
                Birthdate = DateTime.Now.AddYears(-23),
            };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            //assert (Dogrula)
            result.Errors.Count.Should().Be(0);
        }
    }
}
