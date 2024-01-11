using Ab_pk_task3.DBOperations;
using Ab_pk_task3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.TestsSetup
{
    public static class Genres
    {
        public static void AddGenres(this PatikaDbContext content)
        {
            content.Genres.AddRange(
            new Genre{ Name = "PersonalGrowty" },
            new Genre{Name = "ScienceFiction" },
            new Genre{Name = "Noval" },
            new Genre{Name = "Romance"});
        }
    }
}
