using MediatR;


namespace VaccineC.Command.Application.Commands.PaymentForm
{

    public class UpdatePaymentFormCommand : IRequest
    {
        public Guid ID;
        public string Name;
        public int MaximumInstallments;
        public DateTime Register;

        public UpdatePaymentFormCommand(Guid id, string name, int maximumInstallments, DateTime register)
        {
            ID = id;
            Name = name;
            MaximumInstallments = maximumInstallments;
            Register = register;
        }
    }
}
