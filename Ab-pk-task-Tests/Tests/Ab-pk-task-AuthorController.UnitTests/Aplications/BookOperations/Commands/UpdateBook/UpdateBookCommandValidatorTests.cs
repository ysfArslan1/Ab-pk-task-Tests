using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.BookOperations.Commands.UpdateBook;
using Ab_pk_task3.Aplication.BooksOperations.Commands.DeleteBook;
using Ab_pk_task3.Aplication.BooksOperations.Commands.UpdateBook;
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

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.BookOperations.Commands.DeleteBook
{
    public class UpdateBookCommandValidatorTests:IClassFixture<CommonTextFicture>
    {

        private readonly PatikaDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateBookCommandValidatorTests(CommonTextFicture textFicture)
        {
            _dbContext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Theory]
        [InlineData("UpdatedBookTitle", 75, 0, 0)]
        [InlineData("", 75, 4, 2)]
        [InlineData("UpdatedBookTitle", 0, 4, 2)]
        [InlineData("UpdatedBookTitle", 75, 0, 2)]
        [InlineData("UpdatedBookTitle", 75, 4, 0)]
        public void WhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = _dbContext.Books.First().Id;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                AuthorId = authorId
            };

            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı
        }

        [Fact]
        public void WhenDateTimeEqualToNowIsGiven_Validator_ShouldBeReturnError()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = _dbContext.Books.First().Id;
            // Only DateTime is being tested, others should be valid
            command.Model = new UpdateBookModel()
            {
                Title = "UpdatedBookTitle",
                PageCount = 75,
                GenreId = 4,
                AuthorId = 2,
                PublishDate = DateTime.Now.AddDays(1)
            };

            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = _dbContext.Books.First().Id;
            // Only DateTime is being tested, others should be valid
            command.Model = new UpdateBookModel()
            {
                Title = "UpdatedBookTitle",
                PageCount = 75,
                GenreId = 4,
                AuthorId = 2,
                PublishDate = DateTime.Now.AddDays(-1)
            };

            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            //assert (Dogrula)
            result.Errors.Count.Should().Be(0);
        }
    }
}
