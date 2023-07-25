

using DataModelLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IEmailService
    {
        public Result<Reservation> SendRequestEmail(Reservation incomingReservation);
       
    }
}
