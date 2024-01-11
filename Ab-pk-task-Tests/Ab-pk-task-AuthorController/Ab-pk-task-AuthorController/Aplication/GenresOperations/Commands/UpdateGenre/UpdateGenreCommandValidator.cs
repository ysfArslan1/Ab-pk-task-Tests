using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using Ab_pk_task3.Aplication.GenresOperations.Commands.UpdateGenre;

namespace Ab_pk_task3.Aplication.GenresOperations.Commands.UpdateGenre
{
    // UpdateStudentCommand sınıfın için oluşturulan validation sınıfı.
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {

        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Model.isActive).NotEmpty();
        }
    }

}
