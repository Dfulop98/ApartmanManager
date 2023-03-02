
using System.ComponentModel.DataAnnotations;


namespace DataModelLayer.Models
{
    public class Room
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
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
