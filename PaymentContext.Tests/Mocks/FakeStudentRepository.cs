using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repoasitories;

namespace PaymentContext.TestTools.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            
        }

        public bool DocumentExists(string document)
        {
            if (document == "99999999999")
                return true;
            
            return false;
        }

        public bool EmailExists(string email)
        {
            if (email == "hello@balta.io")
                return true;
            
            return false;
        }
    }
}