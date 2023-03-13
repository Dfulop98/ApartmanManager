using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime  TimeStamp{ get; set; }

        public object? Model { get; set; }
        public ResponseModel(string status, string message) 
        {
            Status = status;
            Message = message;
            TimeStamp = DateTime.Now;
        }
        public ResponseModel(string status, string message, object model) 
        {
            Status = status;
            Message = message;
            TimeStamp = DateTime.Now;
            Model = model;
        }
    }
}
