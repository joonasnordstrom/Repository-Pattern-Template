using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Resources
{
    public class OrderResource
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public CourierResource Courier { get; set; }
    }
}
