
using DataModelLayer.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace DataModelLayer.Models
{
    public class Room : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoomNumber { get; set; }

        public int Capacity { get; set; }

        [Required]
        public bool IsAvailable { get; set; }


        public decimal PricePerNight { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public List<Reservation> Reservations { get; set; }

        public List<RoomImage> Images { get; set; }
    }
}
