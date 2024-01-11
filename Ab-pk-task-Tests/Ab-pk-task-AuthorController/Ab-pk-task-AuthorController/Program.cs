
using Ab_pk_task3.DBOperations;
using Ab_pk_task3;

namespace Ab_pk_task_AuthorController
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // in memory de baslangýc olarak database kontrolu ve veri ekleme için kullanýlýyor
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DataGenerator.Initialize(services);
            }
            host.Run();


            //CreateHostBuilder(args).Build().Run(); // inmemory olmadan
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
