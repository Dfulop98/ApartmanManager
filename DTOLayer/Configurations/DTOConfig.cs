using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Configurations
{
    public static class DTOConfig
    {
        public static List<string> RoomProperties { get; } = new List<string>
        {
            "Id",
            "RoomNumber",
            "Capacity",
            "IsAvailable",
            "PricePerNight",
            "Description",
            "Images"
        };
        
        public static List<string> ImagesProperties { get; } = new List<string>
        {
            "Id",
            "Url",
            "Type"
        };

        public static Dictionary<string, IEnumerable<string>> RoomIncludedProperties { get; } = new Dictionary<string, IEnumerable<string>>
        {
            { "Images", new List<string> { "Url" } }
        };


    }
}
