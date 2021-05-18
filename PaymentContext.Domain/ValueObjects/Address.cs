using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neihgborhood, string city, string state, string contry, string zipCode)
        {
            Street = street;
            Number = number;
            Neihgborhood = neihgborhood;
            City = city;
            State = state;
            Contry = contry;
            ZipCode = zipCode;

            AddNotifications(new Contract<Address>()
                .Requires()
                .IsNullOrEmpty(Street,"Address.Street","A rua deve conter pelo menos 3 caracteres")
            );
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neihgborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Contry { get; private set; }
        public string ZipCode { get; private set; }
    }
}