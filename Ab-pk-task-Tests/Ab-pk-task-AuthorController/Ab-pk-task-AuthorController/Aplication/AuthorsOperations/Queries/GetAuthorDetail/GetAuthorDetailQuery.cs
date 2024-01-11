using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;

namespace Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {

        private readonly IPatikaDbContext _dbContext;
        private readonly IMapper _mapper;
        public int Id { get; set; }
        public GetAuthorDetailQuery(IPatikaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
            var item = _dbContext.Authors.Where(x => x.Id == Id).SingleOrDefault();
            if (item is null)
                throw new InvalidOperationException("Bulunamadı");

            AuthorDetailViewModel itemDetail = _mapper.Map<AuthorDetailViewModel>(item);

            return itemDetail;
        }
    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthdate { get; set; }
    }

}
