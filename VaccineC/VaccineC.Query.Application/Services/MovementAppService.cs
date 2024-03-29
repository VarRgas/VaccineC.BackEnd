﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Services
{
    public class MovementAppService : IMovementAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;


        public MovementAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<MovementViewModel>> GetAllAsync()
        {
            var movements = await _queryContext.AllMovements.ToListAsync();
            var movementsViewModel = movements.Select(r => _mapper.Map<MovementViewModel>(r)).ToList();

            foreach (var movementViewModel in movementsViewModel)
            {
                var movementsProducts = await _queryContext.AllMovementsProducts.ToListAsync();
                var movementsProductsViewModel = movementsProducts
                    .Select(r => _mapper.Map<MovementProductViewModel>(r))
                    .Where(r => r.MovementId == movementViewModel.ID)
                    .ToList();

                if (movementsProductsViewModel.Count() == 0)
                {
                    movementViewModel.ProductsInfo = "";
                }
                else if (movementsProductsViewModel.Count() == 1)
                {
                    movementViewModel.ProductsInfo = movementsProductsViewModel[0].Product.Name;
                }
                else
                {
                    movementViewModel.ProductsInfo = movementsProductsViewModel[0].Product.Name + " + " + (movementsProductsViewModel.Count() - 1).ToString();
                }


            }
            return movementsViewModel;
        }

        public async Task<IEnumerable<MovementViewModel>> GetAllByMovementNumber(int movementNumber)
        {
            var movements = await _queryContext.AllMovements.ToListAsync();
            var movementsViewModel = movements
                .Select(r => _mapper.Map<MovementViewModel>(r))
                .Where(r => r.MovementNumber == movementNumber)
                .ToList();

            foreach (var movementViewModel in movementsViewModel)
            {
                var movementsProducts = await _queryContext.AllMovementsProducts.ToListAsync();
                var movementsProductsViewModel = movementsProducts
                    .Select(r => _mapper.Map<MovementProductViewModel>(r))
                    .Where(r => r.MovementId == movementViewModel.ID)
                    .ToList();

                if (movementsProductsViewModel.Count() == 0)
                {
                    movementViewModel.ProductsInfo = "";
                }
                else if (movementsProductsViewModel.Count() == 1)
                {
                    movementViewModel.ProductsInfo = movementsProductsViewModel[0].Product.Name;
                }
                else
                {
                    movementViewModel.ProductsInfo = movementsProductsViewModel[0].Product.Name + " + " + (movementsProductsViewModel.Count() - 1).ToString();
                }


            }


            return movementsViewModel;
        }

        public async Task<IEnumerable<MovementViewModel>> GetAllByProductName(string productName)
        {

            List<Movement> movements = (from m in _context.Movements
                                        join mp in _context.MovementsProducts on m.ID equals mp.MovementId
                                        join p in _context.Products on mp.ProductId equals p.ID into _mp
                                        from x in _mp.DefaultIfEmpty()
                                        where x.Name.Contains(productName)
                                        select m
                                        ).ToList();

            var response = _mapper.Map<IEnumerable<MovementViewModel>>(movements);

            foreach (var movementViewModel in response)
            {
                var movementsProducts = await _queryContext.AllMovementsProducts.ToListAsync();
                var movementsProductsViewModel = movementsProducts
                    .Select(r => _mapper.Map<MovementProductViewModel>(r))
                    .Where(r => r.MovementId == movementViewModel.ID)
                    .ToList();

                if (movementsProductsViewModel.Count() == 0)
                {
                    movementViewModel.ProductsInfo = "";
                }
                else if (movementsProductsViewModel.Count() == 1)
                {
                    movementViewModel.ProductsInfo = movementsProductsViewModel[0].Product.Name;
                }
                else
                {
                    movementViewModel.ProductsInfo = movementsProductsViewModel[0].Product.Name + " + " + (movementsProductsViewModel.Count() - 1).ToString();
                }

            }

            return await Task.FromResult(response);
        }
    }
}
