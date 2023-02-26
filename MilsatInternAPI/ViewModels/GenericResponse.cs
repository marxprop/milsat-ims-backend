using MilsatInternAPI.Enums;

namespace MilsatInternAPI.ViewModels
{
    public class GenericResponse<T>
    {
        public bool Successful { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; } 
    }
}
