using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Aplication.BooksOperations.Queries.GetBookDetail;

namespace Ab_pk_task3.Aplication.BooksOperations.Queries.GetBookDetail
{
    // GetBookDetailQuery sınıfın için oluşturulan validation sınıfı.
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {

        public GetBookDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }

    }

}
