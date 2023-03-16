using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
