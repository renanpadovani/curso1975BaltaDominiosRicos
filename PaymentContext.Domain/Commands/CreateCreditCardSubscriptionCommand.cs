using System;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Domain.Commands
{
    public class CreateCreditCardSubscriptionCommand
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Documment { get; set; }
        public string Email { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactioNumber { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get;  set; }
        public DateTime ExpiredDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get;  set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neihgborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Contry { get; set; }
        public string ZipCode { get; set; }
    }
}