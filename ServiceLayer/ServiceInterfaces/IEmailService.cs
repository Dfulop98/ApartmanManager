

using DataModelLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IEmailService
    {
        public Result<Reservation> SendRequestEmail
            (
            Reservation incomingReservation,
            string sender,
            string receipt,
            string server,
            int port,
            string username,
            string password
            );
       
    }
}
