using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthors;
public class GetAuthorsQuery
{
    private readonly IPatikaDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetAuthorsQuery(IPatikaDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public List<AuthorViewModel> Handle()
    {

        var _list = _dbContext.Authors.OrderBy(x => x.Id).ToList();

        List<AuthorViewModel> result = _mapper.Map<List<AuthorViewModel>>(_list);
        return result;
    }
}

public class AuthorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Birthdate { get; set; }
}

