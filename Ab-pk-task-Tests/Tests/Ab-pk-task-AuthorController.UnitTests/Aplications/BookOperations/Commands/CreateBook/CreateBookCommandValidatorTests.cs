using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
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

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests:IClassFixture<CommonTextFicture>
    {
        
        [Theory]  // hatalı deneme yapıyorum
        [InlineData("deneme",100,0,0)]
        [InlineData("deneme", 100, 1, 0)]
        [InlineData("deneme", 100, 0, 1)]
        [InlineData("deneme", 0, 1, 1)]
        [InlineData("", 100, 1, 1)]
        [InlineData("deneme", 100, 0, 1)]
        [InlineData("deneme", 100, 1, 0)]
        [InlineData("", 100, 1, 1)]
        [InlineData("deneme", 0, 1, 1)]
        [InlineData("deneme", 100, 1, 0)]
        [InlineData("", 100, 1, 1)]
        [InlineData("deneme", 0, 1, 1)]
        [InlineData("deneme", 100, 0, 1)]
        public void WhenInvalidInputsAreGiven_Validators_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            //arrange(Hazırla)
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel() { 
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                AuthorId = authorId,
                PublishDate = DateTime.Now.AddYears(-1)
            };

            //act  (Çalıştır )
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command.Model);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı

        }
        [Fact]
        public void WhenDateTimeEquelNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange(Hazırla)
            CreateBookCommand command = new CreateBookCommand(null, null);
            // sadece datetime test edildigi için digerleri valid olmalı
            command.Model = new CreateBookModel()
            {
                Title = "deneme",
                PageCount = 111,
                GenreId = 1,
                AuthorId = 1,
                PublishDate = DateTime.Now.AddDays(1)
            };

            //act  (Çalıştır )
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command.Model);

            //assert (Dogrula)
            result.Errors.Count.Should().BeGreaterThan(0); // error sayısı 0'dan fazla olmalı

        }

        [Fact] // Dogru Çalışma durumu
        public void WhenValidInputsIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange(Hazırla)
            CreateBookCommand command = new CreateBookCommand(null, null);
            // sadece datetime test edildigi için digerleri valid olmalı
            command.Model = new CreateBookModel()
            {
                Title = "deneme",
                PageCount = 111,
                GenreId = 1,
                AuthorId = 1,
                PublishDate = DateTime.Now.AddYears(-1)
            };

            //act  (Çalıştır )
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command.Model);

            //assert (Dogrula)
            result.Errors.Count.Should().Be(0); 

        }
    }
}
