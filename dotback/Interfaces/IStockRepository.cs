using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotback.Dtos.Stock;
using dotback.Models;

namespace dotback.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAll();
        Task<Stock?> GetById(int id);
        Task<Stock> Create(Stock stock);
        Task<Stock?> Update(int id, UpdateStockDto stockDto);
        Task<Stock?> Delete(int id);
    }
}