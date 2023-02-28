using MilsatIMS.Enums;

namespace MilsatIMS.ViewModels
{
    public class GenericResponse<T>
    {
        public bool Successful { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; } 
    }
}
