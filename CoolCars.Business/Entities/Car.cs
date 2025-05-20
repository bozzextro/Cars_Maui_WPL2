using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoolCars.Business.Entities
{
    public class Car : INotifyPropertyChanged
    {
        private bool _isSelected;

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string EngineSpecs { get; set; }
        public string Price { get; set; }
        public string Acceleration_0_100 { get; set; }
        public string Acceleration_0_200 { get; set; }
        public string QuarterMileTime { get; set; }
        public string TopSpeed { get; set; }
        
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

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
            _isSelected = false;
        }
        
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
