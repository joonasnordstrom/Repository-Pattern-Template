using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Repositories;
using PikiouAPI.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _orderRepository.ListAsync();
        }
    }
}
