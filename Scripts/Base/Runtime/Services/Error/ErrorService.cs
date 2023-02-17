using System;
using Base.Runtime.EventServices;
using Base.Runtime.EventServices.Resources.Misc;

namespace Base.Runtime.Services.Error
{
    public static class ErrorService
    {
        public static void DispatchError(string message, bool throwError = false)
        {
            ErrorEvent errorEvent = new ErrorEvent()
            {
                Message = message
            };
            
            EventService.DispatchEvent(errorEvent);

            if (throwError)
                throw new Exception(message);
        }
    }
}
