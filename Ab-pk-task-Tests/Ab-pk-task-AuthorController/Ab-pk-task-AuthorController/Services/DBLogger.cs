namespace Ab_pk_task3.Services
{
    public class DBLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DBLogger] " + message);
        }
    }
}
