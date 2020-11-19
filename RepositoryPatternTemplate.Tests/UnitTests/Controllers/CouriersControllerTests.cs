using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PikiouAPI.Controllers;
using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Services;
using PikiouAPI.Domain.Services.Communication;
using PikiouAPI.Mapping;
using PikiouAPI.Resources;
using PikiouAPI.Tests.Helpers;
using Xunit;
// ReSharper disable AccessToStaticMemberViaDerivedType


namespace PikiouAPI.Tests.UnitTests.Controllers
{
    public class CouriersControllerTests
    {
        private readonly Mock<ICourierService> _mockedCourierService;
        private readonly IMapper _mapper;
        private readonly CouriersController _couriersController;

        public CouriersControllerTests()
        {
            _mockedCourierService = new Mock<ICourierService>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToRecourceProfile());
                cfg.AddProfile(new ResourceToModelProfile());
            }).CreateMapper();
            _couriersController = new CouriersController(_mockedCourierService.Object, _mapper);
        }

        [Trait("Category", "CouriersController.ListAsync")]
        [Fact(DisplayName = "It should return all couriers", Skip = "Unnecessary test")]
        public async void GetCouriers()
        {
            var fakeCouriers = GetFakeCouriers();
            _mockedCourierService.Setup(x => x.ListAsync()).ReturnsAsync(fakeCouriers);

            var controllerResponse = await _couriersController.GetAllAsync();

            var expectedResult = _mapper.Map<IEnumerable<Courier>, IEnumerable<CourierResource>>(fakeCouriers);
            Expect.IsType<List<CourierResource>>(controllerResponse);
            Expect.DeepEqual(expectedResult, controllerResponse);
        }

        [Trait("Category", "CouriersController.ListAsync")]
        [Fact(DisplayName = "It should return empty array if no couriers", Skip = "Unnecessary test")]
        public async void GetCouriers_Empty()
        {
            _mockedCourierService.Setup(x => x.ListAsync()).ReturnsAsync(new List<Courier>());

            var controllerResponse = await _couriersController.GetAllAsync();

            Expect.Empty(controllerResponse);
            Expect.DeepEqual("[]", controllerResponse);
        }
        
        [Trait("Category", "CouriersController.PostAsync")]
        [Fact(DisplayName = "It should fail when ModelState is invalid", Skip = "Unnecessary test")]
        public async void PostCourier_InvalidModelState()
        {
            _couriersController.ModelState.AddModelError("Name", "Name is required");
            var controllerResponse = await _couriersController.PostAsync(new SaveCourierResource { Name = ""});

            var badRequestResult = Expect.IsType<BadRequestObjectResult>(controllerResponse);
            var errorReturned = Expect.IsType<List<string>>(badRequestResult.Value);
            Expect.DeepEqual(new List<string> { "Name is required" }, errorReturned);
        }
        
        [Trait("Category", "CouriersController.PostAsync")]
        [Fact(DisplayName = "It should fail when SaveAsync is not successful", Skip = "Unnecessary test")]
        public async void PostCourier_SaveAsyncFailed()
        {
            var saveAsyncResponse = new CourierResponse("Failed to save courier");
            _mockedCourierService.Setup(x => x.SaveAsync(It.IsAny<Courier>())).ReturnsAsync(saveAsyncResponse);

            var controllerResponse = await _couriersController.PostAsync(new SaveCourierResource { Name = "FedEx" });

            var badRequestResult = Expect.IsType<BadRequestObjectResult>(controllerResponse);
            var errorReturned = Expect.IsType<string>(badRequestResult.Value);
            Expect.Equal("Failed to save courier", errorReturned);
        }

        [Trait("Category", "CouriersController.PostAsync")]
        [Fact(DisplayName = "It should add a new courier", Skip = "Unnecessary test")]
        public async void PostCourier()
        {
            var saveAsyncResponse = new CourierResponse(
                new Courier { Id = 3, Name = "FedEx", BussinessId = "3456-78", APIKey = "fedex-api-key" }
            );
            _mockedCourierService.Setup(x => x.SaveAsync(It.IsAny<Courier>())).ReturnsAsync(saveAsyncResponse);

            var controllerResponse = await _couriersController.PostAsync(new SaveCourierResource { Name = "FedEx" });

            var okResult = Expect.IsType<OkObjectResult>(controllerResponse);
            var courierReturned = Expect.IsType<CourierResource>(okResult.Value);
            var expectedReturnValue = _mapper.Map<Courier, CourierResource>(saveAsyncResponse.Courier); 
            Expect.DeepEqual(expectedReturnValue, courierReturned);
        }
        
        [Trait("Category", "CouriersController.PutAsync")]
        [Fact(DisplayName = "It should fail when ModelState is invalid", Skip = "Unnecessary test")]
        public async void PutCourier_InvalidModelState()
        {
            _couriersController.ModelState.AddModelError("Name", "Name is required");
            var controllerResponse = await _couriersController.PutAsync(1, new SaveCourierResource { Name = ""});

            var badRequestResult = Expect.IsType<BadRequestObjectResult>(controllerResponse);
            var errorReturned = Expect.IsType<List<string>>(badRequestResult.Value);
            Expect.DeepEqual(new List<string> { "Name is required" }, errorReturned);
        }
        
        [Trait("Category", "CouriersController.PutAsync")]
        [Fact(DisplayName = "It should fail when PutAsync is not successful", Skip = "Unnecessary test")]
        public async void PutCourier_PutAsyncFailed()
        {
            var updateAsyncResponse = new CourierResponse("Failed to update courier");
            _mockedCourierService.Setup(x => x.UpdateAsync(1, It.IsAny<Courier>())).ReturnsAsync(updateAsyncResponse);

            var controllerResponse = await _couriersController.PutAsync(1, new SaveCourierResource { Name = "FedEx" });

            var badRequestResult = Expect.IsType<BadRequestObjectResult>(controllerResponse);
            var errorReturned = Expect.IsType<string>(badRequestResult.Value);
            Expect.Equal("Failed to update courier", errorReturned);
        }

        [Trait("Category", "CouriersController.PutAsync")]
        [Fact(DisplayName = "It should update a courier", Skip = "Unnecessary test")]
        public async void PutCourier()
        {
            var updatedCourier = GetFakeCouriers().First();
            updatedCourier.Name = "Updated name";
            var updateAsyncResponse = new CourierResponse(updatedCourier);
            _mockedCourierService.Setup(x => x.UpdateAsync(1, It.IsAny<Courier>())).ReturnsAsync(updateAsyncResponse);

            var controllerResponse = await _couriersController.PutAsync(1, new SaveCourierResource { Name = "Updated name" });

            var okResult = Expect.IsType<OkObjectResult>(controllerResponse);
            var courierReturned = Expect.IsType<CourierResource>(okResult.Value);
            var expectedReturnValue = _mapper.Map<Courier, CourierResource>(updatedCourier); 
            Expect.DeepEqual(expectedReturnValue, courierReturned);
        }
        
        [Trait("Category", "CouriersController.DeleteAsync")]
        [Fact(DisplayName = "It should fail when DeleteAsync is not successful", Skip = "Unnecessary test")]
        public async void DeleteCourier_DeleteAsyncFailed()
        {
            var deleteAsyncResponse = new CourierResponse("Failed to delete courier");
            _mockedCourierService.Setup(x => x.DeleteAsync(1)).ReturnsAsync(deleteAsyncResponse);

            var controllerResponse = await _couriersController.DeleteAsync(1);

            var badRequestResult = Expect.IsType<BadRequestObjectResult>(controllerResponse);
            var errorReturned = Expect.IsType<string>(badRequestResult.Value);
            Expect.Equal("Failed to delete courier", errorReturned);
        }

        [Trait("Category", "CouriersController.DeleteAsync")]
        [Fact(DisplayName = "It should delete a courier", Skip = "Unnecessary test")]
        public async void DeleteCourier()
        {
            var deletedCourier = GetFakeCouriers().First();
            var deleteAsyncResponse = new CourierResponse(deletedCourier);
            _mockedCourierService.Setup(x => x.DeleteAsync(1)).ReturnsAsync(deleteAsyncResponse);

            var controllerResponse = await _couriersController.DeleteAsync(1);

            var okResult = Expect.IsType<OkObjectResult>(controllerResponse);
            var courierReturned = Expect.IsType<CourierResource>(okResult.Value);
            var expectedReturnValue = _mapper.Map<Courier, CourierResource>(deletedCourier); 
            Expect.DeepEqual(expectedReturnValue, courierReturned);
        }
        
        private static IEnumerable<Courier> GetFakeCouriers()
        {
            return new List<Courier>
            {
                new Courier { Id = 1, Name = "Posti", BussinessId = "1234-56", APIKey = "posti-api-key" },
                new Courier { Id = 2, Name = "DHL", BussinessId = "2345-67", APIKey = "dhl-api-key" }
            };
        }
    }
}