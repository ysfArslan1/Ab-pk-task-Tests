using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthorDetail;

namespace Ab_pk_task3.Aplication.AuthorsOperations.Queries.GetAuthorDetail
{
    // GetAuthorDetailQuery sınıfın için oluşturulan validation sınıfı.
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {

        public GetAuthorDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }

    }

}
