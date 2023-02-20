﻿
using System.ComponentModel.DataAnnotations;


namespace DataAccessLayer.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoomNumber { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public decimal PricePerNight { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
