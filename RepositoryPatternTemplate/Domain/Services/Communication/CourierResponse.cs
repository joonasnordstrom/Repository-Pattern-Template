using PikiouAPI.Domain.Models;

namespace PikiouAPI.Domain.Services.Communication
{
    /// <summary>
    /// Clinet-side response for Courier 
    /// </summary>
    public class CourierResponse : BaseResponse
    {
        public Courier Courier { get; private set; }

        private CourierResponse(bool success, string message, Courier courier) : base(success, message)
        {
            Courier = courier;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="courier">Saved courier.</param>
        /// <returns>Response.</returns>
        public CourierResponse(Courier courier) : this(true, string.Empty, courier)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public CourierResponse(string message) : this(false, message, null)
        { }
    }
}
