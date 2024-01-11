using Ab_pk_task3.Common;
using Ab_pk_task3.DBOperations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ab_pk_task_AuthorController.UnitTests.TestsSetup
{
    public class CommonTextFicture
    {
        public PatikaDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTextFicture()
        {
            var options = new DbContextOptionsBuilder<PatikaDbContext>().UseInMemoryDatabase(databaseName: "PatikaTestDb").Options;
            Context = new PatikaDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddBooks();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();

        }
    }
}
