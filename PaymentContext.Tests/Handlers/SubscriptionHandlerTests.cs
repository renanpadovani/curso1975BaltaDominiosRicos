using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.TestTools.Mocks;

namespace PaymentContext.TestTools
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
    
            command.FirstName  = "Bruce";
            command.LastName  = "Wayne";
            command.Documment  = "99999999999";
            command.Email  = "hello@balta.io2";
            command.Barcode  = "123456789";
            command.BoletoNumber  = "12345789789";
            command.PaymentNumber  = "12312";
            command.PaidDate = System.DateTime.Now;
            command.ExpiredDate  = System.DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid  = 60;
            command.Payer  = "Wayne Corp";
            command.PayerDocument  = "1234567891011";
            command.PayerDocumentType  = Domain.Enums.EDocumentType.CPF;
            command.PayerEmail  = "batman@dc.com";
            command.Street  = "qweqwe";
            command.Number  = "qweqw";
            command.Neihgborhood  = "qweqwe";
            command.City  = "qweqwe";
            command.State  = "as";
            command.Contry  = "sads";
            command.ZipCode  = "12345678";

            handler.Handle(command);
            Assert.AreEqual(false, handler.IsValid);
        }
    }
}