namespace CoolCars.Business
{
    public static class LocalSettings
    {
        public static string ConnectionString { get; set; } = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Cars;Integrated Security=True";
    }
}
