using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PikiouAPI.Controllers;
using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Services;
using PikiouAPI.Mapping;
using PikiouAPI.Resources;
using PikiouAPI.Tests.Helpers;
using Xunit;
// ReSharper disable AccessToStaticMemberViaDerivedType


namespace PikiouAPI.Tests.UnitTests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _mockedOrderService;
        private readonly IMapper _mapper;
        private readonly OrdersController _ordersController;

        public OrdersControllerTests()
        {
            _mockedOrderService = new Mock<IOrderService>();
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new ModelToRecourceProfile()); }).CreateMapper();
            _ordersController = new OrdersController(_mockedOrderService.Object, _mapper);
        }

        [Trait("Category", "OrdersController.ListAsync")]
        [Fact(DisplayName = "It should return all orders", Skip = "Unnecessary test")]
        public async void GetOrders()
        {
            var fakeOrders = GetFakeOrders();
            _mockedOrderService.Setup(x => x.ListAsync()).ReturnsAsync(fakeOrders);

            var controllerResponse = await _ordersController.ListAsync();

            var okResult = Expect.IsType<OkObjectResult>(controllerResponse);
            var ordersReturned = Expect.IsType<List<OrderResource>>(okResult.Value);
            var expectedResult = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(fakeOrders);
            Expect.DeepEqual(expectedResult, ordersReturned);
        }

        [Trait("Category", "OrdersController.ListAsync")]
        [Fact(DisplayName = "It should return empty array if no orders", Skip = "Unnecessary test")]
        public async void GetOrders_Empty()
        {
            _mockedOrderService.Setup(x => x.ListAsync()).ReturnsAsync(new List<Order>());

            var controllerResponse = await _ordersController.ListAsync();

            var okResult = Expect.IsType<OkObjectResult>(controllerResponse);
            var ordersReturned = Expect.IsType<List<OrderResource>>(okResult.Value);
            Expect.Empty(ordersReturned);
            Expect.DeepEqual("[]", ordersReturned);
        }

        private static IEnumerable<Order> GetFakeOrders()
        {
            return new List<Order>
            {
                new Order { Id = 1, OrdererId = 123, DeliveryPrice = (float) 1.23 },
                new Order { Id = 2, OrdererId = 234, DeliveryPrice = (float) 2.34 },
                new Order { Id = 3, OrdererId = 345, DeliveryPrice = (float) 3.45 }
            };
        }
    }
}