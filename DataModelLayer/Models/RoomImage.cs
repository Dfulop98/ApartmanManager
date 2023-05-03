using System.ComponentModel.DataAnnotations;

namespace DataModelLayer.Models
{
    public class RoomImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }


        [Required]
        public Room Room { get; set; }
    }
}
