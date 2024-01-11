using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.BooksOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public int Id { get; set; }
        public UpdateBookModel Model { get; set; }
        private readonly IPatikaDbContext _dbContext;
        public UpdateBookCommand(IPatikaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var item = _dbContext.Books.Where(x => x.Id == Id).FirstOrDefault();
            if (item is null)
                throw new InvalidOperationException("Kitap Bulunamadı");

            var book = _dbContext.Books.Where(x => x.Title.ToLower() == Model.Title.ToLower()).Where(x=> x.Id != Id).FirstOrDefault();
            if(book!=null)
                throw new InvalidOperationException("Aynı isim bulunmakta");

            var genre = _dbContext.Genres.Where(x => x.Id == Model.GenreId).FirstOrDefault();
            if (genre == null)
                throw new InvalidOperationException("Böyle bir tür yok");

            var author = _dbContext.Authors.Where(x => x.Id == Model.AuthorId).FirstOrDefault();
            if (author == null)
                throw new InvalidOperationException("Böyle bir yazar yok");


            item.Title = Model.Title != default ? Model.Title : item.Title;
            item.GenreId = Model.GenreId != default ? Model.GenreId : item.GenreId;
            item.AuthorId = Model.AuthorId != default ? Model.AuthorId : item.AuthorId;
            item.PageCount = Model.PageCount != default ? Model.PageCount : item.PageCount;
            item.PublishDate = Model.PublishDate != default ? Model.PublishDate : item.PublishDate;

            // database işlemleri yapılır.
            _dbContext.Books.Update(item);
            _dbContext.SaveChanges();

        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
