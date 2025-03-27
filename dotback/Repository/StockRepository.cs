using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotback.Data;
using dotback.Dtos.Stock;
using dotback.Interfaces;
using dotback.Models;
using Microsoft.EntityFrameworkCore;

namespace dotback.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAll()
        {
            // ToList() to make it executed bc its deferred execution
            return await _context.Stock.ToListAsync();
        }

        public async Task<Stock?> GetById(int id)
        {
            return await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock> Create(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> Update(int id, UpdateStockDto stockDto)
        {
            var existingStock = await GetById(id);
            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<Stock?> Delete(int id)
        {
            var existingStock = await GetById(id);
            if (existingStock == null)
            {
                return null;
            }

            // Source: YouTube comments
            // The reason why Delete() does not allow await is:
            // - It does not involve any immediate database interaction when called, merely a state change in memory
            // - does not involve waiting and is a quick in-memory operation, making it asynchronous would not provide any benefits and could even lead to less efficient resource utilization.
            _context.Stock.Remove(existingStock);
            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}