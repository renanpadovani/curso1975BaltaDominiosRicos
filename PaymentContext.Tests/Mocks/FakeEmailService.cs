using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repoasitories;
using PaymentContext.Domain.Services;

namespace PaymentContext.TestTools.Mocks
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string email, string subject, string body)
        {
            
        }
    }
}