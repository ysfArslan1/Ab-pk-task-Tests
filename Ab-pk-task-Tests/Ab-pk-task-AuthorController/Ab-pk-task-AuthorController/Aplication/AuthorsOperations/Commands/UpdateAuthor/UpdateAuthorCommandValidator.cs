using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using Ab_pk_task3.Aplication.AuthorsOperations.Commands.UpdateAuthor;

namespace Ab_pk_task3.Aplication.AuthorsOperations.Commands.UpdateAuthor
{
    // UpdateAuthorCommand sınıfın için oluşturulan validation sınıfı.
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {

        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Model.Surname).NotEmpty().MaximumLength(50); 
            RuleFor(x => x.Model.Birthdate).NotEmpty().LessThan(DateTime.Now.AddYears(-10)); 
        }
    }

}
