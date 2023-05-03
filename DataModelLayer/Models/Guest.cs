
using DataModelLayer.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace DataModelLayer.Models
{
    public class Guest :IEntity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Nationatily { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
