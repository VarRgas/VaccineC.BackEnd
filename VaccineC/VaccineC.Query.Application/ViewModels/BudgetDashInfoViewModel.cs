using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class BudgetDashInfoViewModel
    {
        public int budgetAprovedNumber { get; set; }
        public int budgetPendingNumber { get; set; }
        public int budgetCanceledNumber { get; set; }
        public int budgetOverduedNumber { get; set; }
        public int budgetFinalizedNumber { get; set; }
        public int budgetNegotiationNumber { get; set; }
        public int personPhysicalNumber { get; set; }
        public int personJuridicalNumber { get; set; }
        public int totalBudgetNumber { get; set; }
        public int totalBudgetNumberPrevious { get; set; }
        public decimal totalBudgetAmount { get; set; }
        public decimal totalBudgetAmountPrevious { get; set; }
        public decimal totalBudgetAmountLost { get; set; }
        public decimal totalBudgetAmountLostPrevious { get; set; }
        public decimal totalBudgetAmountIncrease { get; set; }
        public decimal totalBudgetAmountDecrease { get; set; }
        public decimal totalBudgetAmountIncreasePercent { get; set; }
        public decimal totalBudgetAmountDecreasePercent { get; set; }

        public decimal totalBudgetDiscount { get; set; }
        public decimal totalBudgetDiscountPrevious { get; set; }
        public int year { get; set; }
        public int month { get; set; }

        public List<ProductBudgetDashInfoViewModel> listProductBudgetDashInfoViewModel = new List<ProductBudgetDashInfoViewModel>();
        public List<BudgetProfitMonthViewModel> listBudgetProfitMonthViewModel = new List<BudgetProfitMonthViewModel>();

    }
}
