using DataModelLayer.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModelLayer.Models
{
    public class Payment: IEntity
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int ReservationId { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }
    }
}
