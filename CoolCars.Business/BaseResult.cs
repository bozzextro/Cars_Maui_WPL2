using System;

namespace CoolCars.Business
{
    public class BaseResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public BaseResult()
        {
            Success = true;
            Message = string.Empty;
            Exception = null;
        }
    }
}
