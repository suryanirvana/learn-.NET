using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotback.Data;
using dotback.Dtos.Stock;
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

        // [FromBody] to get the request body
        // TODO: add exceptions
        [HttpPost]
        public IActionResult Create([FromBody] CreateStockDto stockDto)
        {
            var stock = stockDto.ToStock();
            _context.Stock.Add(stock);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        // TODO: add exceptions and validation
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
        {
            var stock = _context.Stock.FirstOrDefault(s => s.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Symbol = stockDto.Symbol;
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;
            _context.SaveChanges();

            return Ok(stock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stock = _context.Stock.FirstOrDefault(s => s.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Stock.Remove(stock);
            _context.SaveChanges();

            return NoContent();
        }
    }
}