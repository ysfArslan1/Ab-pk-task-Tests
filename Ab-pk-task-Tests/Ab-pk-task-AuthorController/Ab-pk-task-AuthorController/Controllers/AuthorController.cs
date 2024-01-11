using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Ab_pk_task3.DBOperations;

using Ab_pk_task3.Entities;
using Ab_pk_task3.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidation.Results;
using FluentValidation;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.UpdateAuthor;
using Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthors;
using Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthorDetail;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.CreateAuthor;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.DeleteAuthor;

namespace Ab_pk_task3.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IPatikaDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IPatikaDbContext bankDbContext, IMapper mapper)
        {
            _context = bankDbContext;
            _mapper = mapper;
        }

        // GET: get GetAuthors
        [HttpGet]
        public IActionResult GetAuthors()
        {
            // Author verilerinin AuthorViewModel alınması için kullanlan query sınıfı oluşturulur ve handle edilir
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var _list = query.Handle();
            return Ok(_list);
        }

        // GET: get Author from id
        [HttpGet("{id}")]
        public ActionResult<AuthorDetailViewModel> GetAuthorById([FromRoute] int id)
        {
            AuthorDetailViewModel result;
           
            // GetAuthorDetailQuery nesnesi oluşturulur
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.Id = id;
            // Validation işlemi yapılır.
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            
            return Ok(result);
        }

        // Post: create a Author
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newModel)
        {
            // CreateAuthorCommand nesnesi oluşturulur
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newModel;
            // validation yapılır.
            CreateAuthorCommandValidator _validator=new CreateAuthorCommandValidator();
            _validator.ValidateAndThrow(newModel);
            command.Handle();

            return Ok();
        }

        // PUT: update a Author
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            // CreateAuthorCommand nesnesi oluşturulur
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.Id = id;
            command.Model = updateAuthor;
            // validation yapılır.
            UpdateAuthorCommandValidator validator  = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
           
            return Ok();
        }

        // DELETE: delete a Author
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            // CreateAuthorCommand nesnesi oluşturulur
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = id;
            // validation yapılır.
            DeleteAuthorCommandValidator _validator = new DeleteAuthorCommandValidator();
            _validator.ValidateAndThrow(command);
            command.Handle();
           
            return Ok();
        }

    }
}
