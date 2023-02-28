namespace MilsatIMS.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string receivermail, string subject, string body);
    }
}
