using System;
using System.Data;

namespace CoolCars.Business
{
    public class SelectResult : BaseResult
    {
        public DataTable Data { get; set; }

        public SelectResult() : base()
        {
            Data = new DataTable();
        }
    }
}
