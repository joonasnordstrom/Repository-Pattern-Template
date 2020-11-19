using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikiouAPI.Domain.Services.Communication
{

    // TODO may become fragile base class => composition over inheritance

    /// <summary>
    /// All responses should be derived from this class
    /// </summary>
    public abstract class BaseResponse
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
