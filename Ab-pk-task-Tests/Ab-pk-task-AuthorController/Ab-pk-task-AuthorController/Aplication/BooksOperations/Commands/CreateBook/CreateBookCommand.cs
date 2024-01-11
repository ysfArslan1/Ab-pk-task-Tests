
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.BooksOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly IPatikaDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(IPatikaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var item = _dbContext.Books.Where(x => x.Title == Model.Title ).FirstOrDefault();
            if (item is not null)
                throw new InvalidOperationException("Zaten Mevcut");

            var genre = _dbContext.Genres.Where(x => x.Id == Model.GenreId).FirstOrDefault();
            if (genre == null)
                throw new InvalidOperationException("Böyle bir tür yok");


            var author = _dbContext.Authors.Where(x => x.Id == Model.AuthorId).FirstOrDefault();
            if (author == null)
                throw new InvalidOperationException("Böyle bir yazar yok");


            item = _mapper.Map<Book>(Model);
            // database işlemleri yapılır.
            _dbContext.Books.Add(item);
            _dbContext.SaveChanges();

        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
