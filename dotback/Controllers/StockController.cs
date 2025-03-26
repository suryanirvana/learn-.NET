using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotback.Data;
using dotback.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace dotback.Controllers
{
    [Route("api/v1/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // ToList() to make it executed bc its deferred execution
            var stocks = _context.Stock.ToList()
                            .Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        // [FromRoute] to get the parameter
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Find is better if we get by id
            var stock = _context.Stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }
    }
}