using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repoasitories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{

    public class SubscriptionHandler : 
        Notifiable<Notification>, 
        IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        //Injeção de dependência do repositorio
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        //Cria assinatura com boleto
        public ICommandResult Handle(CreateBoletoSubscriptionCommand Command)
        {
            //Fail Fast Validation
            Command.Validate();
            
            if (!Command.IsValid)
            {
                AddNotifications(Command);
                return new CommandResult(false,"Não foi possível realizar sua assinatura");
            }

            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(Command.Documment))
                AddNotification("Document","Este CPF já está em uso");

            //Verificar se email ja esta cadastrado
            if (_repository.EmailExists(Command.Email))
                AddNotification("Email","Este E-mail já está em uso");

            //Gerar os Vos
            var name = new Name(Command.FirstName, Command.LastName);
            var document = new Document(Command.Documment, (Enums.EDocumentType)Command.PayerDocumentType);
            var email = new Email(Command.Email);
            var address = new Address(Command.Street, Command.Number, Command.Neihgborhood, Command.City, Command.State, Command.Contry, Command.ZipCode);

            //Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(Command.Barcode, 
                Command.BoletoNumber, 
                Command.PaidDate, 
                Command.ExpiredDate, 
                Command.Total, 
                Command.TotalPaid, 
                Command.Payer, 
                new Document(Command.PayerDocument, (Enums.EDocumentType)Command.PayerDocumentType),
                address, 
                email
            );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as notificações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Balta.io","Sua assinatura foi criada");

            //Retornar informações
            return new CommandResult(true,"Assinatura realizada com sucesso");
        }

        //Cria assinatura com PayPal
        public ICommandResult Handle(CreatePayPalSubscriptionCommand Command)
        {
            //Fail Fast Validation (não está implementado)
            /*Command.Validate();
            
            if (!Command.IsValid)
            {
                AddNotifications(Command);
                return new CommandResult(false,"Não foi possível realizar sua assinatura");
            }*/

            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(Command.Documment))
                AddNotification("Document","Este CPF já está em uso");

            //Verificar se email ja esta cadastrado
            if (_repository.EmailExists(Command.Email))
                AddNotification("Email","Este E-mail já está em uso");

            //Gerar os Vos
            var name = new Name(Command.FirsName, Command.LastName);
            var document = new Document(Command.Documment, (Enums.EDocumentType)Command.PayerDocumentType);
            var email = new Email(Command.Email);
            var address = new Address(Command.Street, Command.Number, Command.Neihgborhood, Command.City, Command.State, Command.Contry, Command.ZipCode);

            //Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(Command.TransactionCode, 
                Command.PaidDate, 
                Command.ExpiredDate, 
                Command.Total, 
                Command.TotalPaid, 
                Command.Payer, 
                new Document(Command.PayerDocument, (Enums.EDocumentType)Command.PayerDocumentType),
                address, 
                email
            );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as notificações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as notificações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Balta.io","Sua assinatura foi criada");

            //Retornar informações
            return new CommandResult(true,"Assinatura realizada com sucesso");
        }
    }
}