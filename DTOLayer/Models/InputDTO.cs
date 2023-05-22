using DataModelLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Models
{
    public class InputDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Images> Images { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public Room? Room { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int RoomId { get; set; }
    }
}
