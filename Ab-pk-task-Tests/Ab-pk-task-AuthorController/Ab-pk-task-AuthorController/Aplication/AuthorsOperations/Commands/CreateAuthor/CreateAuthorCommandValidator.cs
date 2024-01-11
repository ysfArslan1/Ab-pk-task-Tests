
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;

namespace Ab_pk_task3.Aplication.AuthorsOperations.Commands.CreateAuthor
{
    // CreateAuthorCommant sınıfın için oluşturulan validation sınıfı.
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorModel>
    {
        public CreateAuthorCommandValidator()
        {

            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Birthdate).NotEmpty().LessThan(DateTime.Now.AddYears(-10));
        }

    }
}
