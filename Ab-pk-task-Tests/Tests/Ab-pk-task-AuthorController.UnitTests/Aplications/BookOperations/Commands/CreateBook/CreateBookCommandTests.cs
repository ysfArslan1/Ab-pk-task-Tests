using Ab_pk_task_AuthorController.UnitTests.TestsSetup;
using Ab_pk_task3.Aplication.BooksOperations.Commands.CreateBook;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.Aplications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests:IClassFixture<CommonTextFicture>
    {
        private readonly PatikaDbContext _dbcontext;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTextFicture textFicture)
        {
            _dbcontext = textFicture.Context;
            _mapper = textFicture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(Hazırla)
            Book book = new Book()
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 22,
                PublishDate = DateTime.Now.AddDays(123),
                GenreId = 1,
                AuthorId = 1
            };
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_dbcontext, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title};

            //act & assert (Çalıştır & Dogrula)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Zaten Mevcut");
        }

        [Fact]
        public void WhenValidInputsIsGiven_Book_ShouldBeCreated()
        {
            //arrange(Hazırla)
            
            CreateBookCommand command = new CreateBookCommand(_dbcontext, _mapper);
            command.Model = new CreateBookModel() { Title = "deneme1234", PageCount=111, PublishDate = DateTime.Now.AddDays(-123), GenreId=2, AuthorId=1 };
            // act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // assert 
            var book = _dbcontext.Books.FirstOrDefault(x=>x.Title == command.Model.Title);
            book.Should().NotBeNull();  
            book.PageCount.Should().Be(command.Model.PageCount);
            book.PublishDate.Should().Be(command.Model.PublishDate);
            book.GenreId.Should().Be(command.Model.GenreId);
            book.AuthorId.Should().Be(command.Model.AuthorId);
        }
    }
}
