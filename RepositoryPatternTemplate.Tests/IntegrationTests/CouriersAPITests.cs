using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PikiouAPI.Domain.Models;
using PikiouAPI.Persistence.Contexts;
using PikiouAPI.Resources;
using PikiouAPI.Tests.Helpers;
using Xunit;
// ReSharper disable AccessToStaticMemberViaDerivedType


namespace PikiouAPI.Tests.IntegrationTests
{
    public class CouriersAPITests : IClassFixture<IntegrationTestFixture>
    {
        private const string COURIER_URL = "api/couriers";
        
        private readonly TestWebApplicationFactory<PikiouAPI.Startup> _webApplicationFactory;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CouriersAPITests(IntegrationTestFixture integrationTestFixture)
        {
            _context = integrationTestFixture.Context;
            _webApplicationFactory = integrationTestFixture.WebApplicationFactory;
            _mapper = integrationTestFixture.Mapper;
            
            integrationTestFixture.Dispose(); // clean data before each test
        }

        [Fact(DisplayName = "It should return empty array when no couriers exist")]
        public async Task Get_Couriers_Empty()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync(COURIER_URL);

            response.EnsureSuccessStatusCode();
            Expect.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            
            var responseString = await response.Content.ReadAsStringAsync();
            Expect.Equal("[]", responseString);
        }
        
        [Fact(DisplayName = "It should return all couriers")]
        public async Task Get_Couriers()
        {
            var courier1 = new Courier { Name = "Posti", APIKey = "posti-api-key", BussinessId = "1234-56" };
            var courier2 = new Courier { Name = "DHL", APIKey = "dhl-api-key", BussinessId = "1234-57" };

            await _context.Couriers.AddAsync(courier1);
            await _context.Couriers.AddAsync(courier2);
            await _context.SaveChangesAsync();
            
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync(COURIER_URL);

            response.EnsureSuccessStatusCode();
            Expect.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            
            var responseString = await response.Content.ReadAsStringAsync();

            var expectedResult = _mapper.Map<IEnumerable<Courier>, IEnumerable<CourierResource>>(new List<Courier> { courier1, courier2 });
            Expect.DeepEqualLowerCaseFields(expectedResult, responseString);
        }
    }
}