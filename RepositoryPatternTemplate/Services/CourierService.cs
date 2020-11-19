using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PikiouAPI.Domain.Models;
using PikiouAPI.Domain.Repositories;
using PikiouAPI.Domain.Services;
using PikiouAPI.Domain.Services.Communication;

namespace PikiouAPI.Services
{
    /// <summary>
    /// Implementations for handling database functions for Courier
    /// </summary>
    public class CourierService : ICourierService
    {
        private readonly ICourierRepository _courierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourierService(ICourierRepository courierRepository, IUnitOfWork unitOfWork)
        {
            _courierRepository = courierRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all couriers
        /// </summary>
        public async Task<IEnumerable<Courier>> ListAsync()
        {
            return await _courierRepository.ListAsync();
        }

        /// <summary>
        /// Save courier
        /// </summary>
        public async Task<CourierResponse> SaveAsync(Courier courier)
        {
            try
            {
                await _courierRepository.AddAsync(courier);
                await _unitOfWork.CompleteAsync();

                return new CourierResponse(courier);
            }
            catch(Exception e)
            {
                // TODO logging
                return new CourierResponse($"An error occurred when trying to save courier: {e.Message}");
            }
            //TODO catch all exceptions separetely
        }

        /// <summary>
        /// Update courier
        /// </summary>
        public async Task<CourierResponse> UpdateAsync(int id, Courier courier)
        {
            var courierToBeUpdated = await _courierRepository.FindByIdAsync(id);

            if (courierToBeUpdated == null)
                return new CourierResponse("Courier not found.");

            courierToBeUpdated.Name = courier.Name;

            try
            {
                _courierRepository.Update(courierToBeUpdated);
                await _unitOfWork.CompleteAsync();

                return new CourierResponse(courierToBeUpdated);
            }
            catch (Exception e)
            {
                // TODO logging
                return new CourierResponse($"An error occurred when updating the courier: {e.Message}");
            }
        }

        /// <summary>
        /// Delete courier
        /// </summary>
        public async Task<CourierResponse> DeleteAsync(int id)
        {
            var courierToBeDeleted = await _courierRepository.FindByIdAsync(id);

            if (courierToBeDeleted == null)
                return new CourierResponse("Courier not found.");

            try
            {
                _courierRepository.Remove(courierToBeDeleted);
                await _unitOfWork.CompleteAsync();

                return new CourierResponse(courierToBeDeleted);
            }
            catch (Exception ex)
            {
                // TODO Logging
                return new CourierResponse($"An error occurred when deleting the courier: {ex.Message}");
            }
        }
    }
}
