using DTOLayer.Models;

namespace ServiceLayer.Factories.Model
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public UniversalDTO? Model { get; set; }
        public IEnumerable<UniversalDTO>? Models { get; set; }

    }
}
