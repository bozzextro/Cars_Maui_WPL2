namespace CoolCars.Business.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string EngineSpecs { get; set; }
        public string Price { get; set; }
        public string Acceleration_0_100 { get; set; }
        public string Acceleration_0_200 { get; set; }
        public string QuarterMileTime { get; set; }
        public string TopSpeed { get; set; }

        public Car()
        {
            Name = string.Empty;
            ImageUrl = string.Empty;
            EngineSpecs = string.Empty;
            Price = string.Empty;
            Acceleration_0_100 = string.Empty;
            Acceleration_0_200 = string.Empty;
            QuarterMileTime = string.Empty;
            TopSpeed = string.Empty;
        }
    }
}
