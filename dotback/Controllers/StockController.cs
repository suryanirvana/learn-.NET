using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotback.Data;
using dotback.Dtos.Stock;
using dotback.Interfaces;
using dotback.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace dotback.Controllers
{
    [Route("api/v1/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAll();
            var stocksDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocksDto);
        }

        // [FromRoute] to get the parameter
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetById(id);
            if (stock == null)
            {
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        // [FromBody] to get the request body
        // TODO: add exceptions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            var stock = stockDto.ToStock();
            await _stockRepository.Create(stock);

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        // TODO: add exceptions and validation
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
        {
            var stock = await _stockRepository.Update(id, stockDto);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepository.Delete(id);
            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}