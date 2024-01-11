
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.GenresOperations.Commands.CreateGenre
{
    // CreateStudentCommant sınıfın için oluşturulan validation sınıfı.
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreModel>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }

    }
}
