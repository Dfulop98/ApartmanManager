using System.ComponentModel.DataAnnotations;

namespace DataModelLayer.Models
{
    public class OutSideImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
