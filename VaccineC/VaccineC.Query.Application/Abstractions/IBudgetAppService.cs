﻿using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IBudgetAppService
    {
        Task<IEnumerable<BudgetViewModel>> GetAllAsync();
        Task<IEnumerable<BudgetViewModel>> GetByName(string personName);
    }
}