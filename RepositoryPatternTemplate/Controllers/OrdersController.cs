using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Services;
using PikiouAPI.Resources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PikiouAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<OrderResource>), 200)]
        public async Task<IActionResult> ListAsync()
        {
            var orders = await _orderService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
            return Ok(resources);
        }
    }
}
