using AutoMapper;
using Ab_pk_task3.Entities;
using Ab_pk_task3.Aplication.BooksOperations.Queries.GetBooks;
using Ab_pk_task3.Aplication.BooksOperations.Queries.GetBookDetail;
using Ab_pk_task3.Aplication.BooksOperations.Commands.CreateBook;
using Ab_pk_task3.Aplication.GenresOperations.Queries.GetGenres;
using Ab_pk_task3.Aplication.GenresOperations.Queries.GetGenreDetail;
using Ab_pk_task3.Aplication.GenresOperations.Commands.CreateGenre;
using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.CreateAuthor;
using Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthors;
using Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthorDetail;


namespace Ab_pk_task3.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {

            CreateMap<Book, BookViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name+ " " + src.Author.Surname));
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            CreateMap<CreateBookModel, Book>();

            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();

            CreateMap<Author, AuthorViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();
        }
    }
}
