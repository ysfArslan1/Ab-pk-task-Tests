using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Aplication.GenresOperations.Queries.GetGenreDetail;

namespace Ab_pk_task3.Aplication.GenresOperations.Queries.GetGenreDetail
{
    // GetStudentDetailQuery sınıfın için oluşturulan validation sınıfı.
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {

        public GetGenreDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }

    }

}
