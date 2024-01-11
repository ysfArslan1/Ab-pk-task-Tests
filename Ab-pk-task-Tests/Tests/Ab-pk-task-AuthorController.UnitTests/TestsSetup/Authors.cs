using Ab_pk_task_AuthorController.Entities;
using Ab_pk_task3.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.TestsSetup
{
    public static class Authors
    {
        public static void AddAuthors(this PatikaDbContext content)
        {
            content.Authors.AddRange(
            new Author{ Name = "A--", Surname = "S--",Birthdate = DateTime.Now.AddYears(-19), }, 
            new Author{Name = "B--",Surname = "F--", Birthdate = DateTime.Now.AddYears(-24),},
            new Author { Name = "H--", Surname = "C--", Birthdate = DateTime.Now.AddYears(-33), });
        }
    }
}
