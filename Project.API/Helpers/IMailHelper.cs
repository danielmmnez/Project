using Project.Shared.Responses;

namespace Project.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
