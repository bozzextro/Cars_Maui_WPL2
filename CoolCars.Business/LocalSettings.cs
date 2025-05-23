namespace CoolCars.Business
{
    public static class LocalSettings
    {
        public static string ConnectionString { get; set; } = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cars;Integrated Security=True";
    }
}
