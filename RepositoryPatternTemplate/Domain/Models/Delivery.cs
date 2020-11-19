using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Domain.Models
{
    public class Delivery
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public EStateOfDelivery State { get; set; }
        [Required]
        public int CourierId { get; set; }
        public Courier Courier { get; set; }
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string TrackingCode { get; set; }
        public IList<Package> Packages { get; set; }
    }

    //TODO states of delivery
    public enum EStateOfDelivery : byte
    {
        [Description("Order has been received and passed forward to courier.")]
        Received = 1,
        [Description("Order is been processed in warehouse.")]
        Warehouse = 2,
        [Description("Order has been successfully delivered to orderer.")]
        Delivered = 3
    }
}
