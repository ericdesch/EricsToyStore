using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Utilities;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToysController : ControllerBase
	{
		private readonly DBInteractor _context;

		public ToysController(DBInteractor context)
		{
			_context = context;
		}

		// GET: api/Toy
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ToyViewModel>>> GetToy()
		{
			var toys = await _context.Toy.ToListAsync();

			var results = from toy in toys
						  select new ToyViewModel
						  {
							  Id = toy.Id,
							  Name = toy.Name,
							  Price = Currency.Format(toy.Price)
						  };

			return Ok(results);
		}

		// GET: api/Toy/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ToyViewModel>> GetToy(int id)
		{
			var toy = await _context.Toy.FindAsync(id);

			if (toy == null)
			{
				return NotFound();
			}

			var result = new ToyViewModel
			{
				Id = toy.Id,
				Name = toy.Name,
				Price = Currency.Format(toy.Price)
			};

			return Ok(result);
		}

		// POST: api/Toy
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<IActionResult> PostLendTableEntry(ToyViewModel toyModel)
		{
			var toy = new Toy
			{
				Name = toyModel.Name,
				Price = Currency.Parse(toyModel.Price)
			};

			_context.Toy.Add(toy);
			await _context.SaveChangesAsync();

			return Ok();
		}

		// PUT: api/Toy/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutToy(int id, ToyViewModel toyModel)
		{
			if (id != toyModel.Id)
			{
				return BadRequest();
			}

			var toy = new Toy
			{
				Id = id,
				Name = toyModel.Name,
				Price = Currency.Parse(toyModel.Price)
			};

			_context.Entry(toy).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ToyExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok();
		}

		// DELETE: api/Toy/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteToy(int id)
		{
			var toy = await _context.Toy.FindAsync(id);
			if (toy == null)
			{
				return NotFound();
			}

			_context.Toy.Remove(toy);
			await _context.SaveChangesAsync();

			return Ok();
		}

		private bool ToyExists(int id)
		{
			return _context.Toy.Any(e => e.Id == id);
		}
	}
}
