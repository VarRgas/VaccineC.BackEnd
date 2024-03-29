﻿using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class AddBudgetProductCommand : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public Guid ID;
        public Guid BudgetId;
        public Guid ProductId;
        public Guid? BorrowerPersonId;
        public string ProductDose;
        public string? Details;
        public decimal EstimatedSalesValue;
        public string SituationProduct;
        public DateTime Register;

        public AddBudgetProductCommand(Guid id, Guid budgetId, Guid productId, Guid? borrowerPersonId, string productDose, string? details, decimal estimatedSalesValue, string situationProduct, DateTime register)
        {
            ID = id;
            BudgetId = budgetId;
            ProductId = productId;
            BorrowerPersonId = borrowerPersonId;
            ProductDose = productDose;
            Details = details;
            EstimatedSalesValue = estimatedSalesValue;
            SituationProduct = situationProduct;
            Register = register;
        }
    }
}
