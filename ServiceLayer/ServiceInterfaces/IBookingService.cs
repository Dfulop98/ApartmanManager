using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;
namespace ServiceLayer.ServiceInterfaces
{
    public interface IBookingService
    {
        public void AddReservation();
        public void RemoveReservation();
        public void UpdateReservation();

        public List<Reservation> GetReservations();
        public Reservation GetReservationById(int id);
    }
}
