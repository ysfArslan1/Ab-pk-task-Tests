using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.DBOperations;

namespace Ab_pk_task3.Aplication.AuthorsOperations.Commands.DeleteAuthor
{
    // DeleteAuthorCommand sınıfın için oluşturulan validation sınıfı.
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}

