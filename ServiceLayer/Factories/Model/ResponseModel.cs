using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Factories.Model
{
    public class ResponseModel<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public T? Model { get; set; }
        public List<T>? Models { get; set; }

    }
}
