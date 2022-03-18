#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using barber_mvc.Data;
using barber_mvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace barber_mvc.Controllers
{
    [Authorize]
    public class BarberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarberController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Barber
        public async Task<IActionResult> Index()
        {
            return View(await _context.Barber.OrderBy(m => m.BarberName).ToListAsync());
        }

        // GET: Barber/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barber = await _context.Barber
                .FirstOrDefaultAsync(m => m.BarberId == id);
            if (barber == null)
            {
                return NotFound();
            }

            return View(barber);
        }

        // GET: Barber/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Barber/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BarberId,BarberName,Role,PayRate")] Barber barber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(barber);
        }

        // GET: Barber/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barber = await _context.Barber.FindAsync(id);
            if (barber == null)
            {
                return NotFound();
            }
            return View(barber);
        }

        // POST: Barber/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BarberId,BarberName,Role,PayRate")] Barber barber)
        {
            if (id != barber.BarberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarberExists(barber.BarberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(barber);
        }

        // GET: Barber/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barber = await _context.Barber
                .FirstOrDefaultAsync(m => m.BarberId == id);
            if (barber == null)
            {
                return NotFound();
            }

            return View(barber);
        }

        // POST: Barber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barber = await _context.Barber.FindAsync(id);
            _context.Barber.Remove(barber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarberExists(int id)
        {
            return _context.Barber.Any(e => e.BarberId == id);
        }
    }
}
