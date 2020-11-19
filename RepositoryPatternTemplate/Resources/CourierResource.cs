using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Resources
{
    /// <summary>
    /// Client-side data transfer object (DTO) for Courier
    /// </summary>
    public class CourierResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
