using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PikiouAPI.Domain.Models;
using PikiouAPI.Persistence.Contexts;
using PikiouAPI.Tests.Helpers;
using Xunit;
// ReSharper disable AccessToStaticMemberViaDerivedType


namespace PikiouAPI.Tests.IntegrationTests
{
    public class OrdersAPITests : IClassFixture<IntegrationTestFixture>
    {
        private readonly TestWebApplicationFactory<PikiouAPI.Startup> _webApplicationFactory;
        private readonly AppDbContext _context;

        public OrdersAPITests(IntegrationTestFixture integrationTestFixture)
        {
            _context = integrationTestFixture.Context;
            _webApplicationFactory = integrationTestFixture.WebApplicationFactory;
            
            integrationTestFixture.Dispose(); // clean data before each test
        }

        [Fact(DisplayName = "It should return empty array when no orders exist")]
        public async Task Get_Orders_Empty()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync("api/orders");

            response.EnsureSuccessStatusCode();
            Expect.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            
            var responseString = await response.Content.ReadAsStringAsync();
            Expect.Equal("[]", responseString);
        }
        
        // TODO: FIX tests
//        [Fact(DisplayName = "It should return all orders")]
//        public async Task Get_Orders()
//        {
//            var order1 = new Order { DeliveryPrice = (float) 1.2, OrdererId = 44, ECommerce = null };
//            var order2 = new Order { DeliveryPrice = (float) 1.5, OrdererId = 45, ECommerce = null };
//
//            await _context.Orders.AddAsync(order1);
//            await _context.Orders.AddAsync(order2);
//            await _context.SaveChangesAsync();
//            
//            var client = _webApplicationFactory.CreateClient();
//
//            var response = await client.GetAsync("api/orders");
//
//            response.EnsureSuccessStatusCode();
//            Expect.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
//            
//            var responseString = await response.Content.ReadAsStringAsync();
//            Expect.Equal("[]", responseString);
//        }
    }
}