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
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            // await _context.Barber.ToListAsync();
            // await _context.Customer.ToListAsync();
            // await _context.Service.ToListAsync();

            var dateToday = DateTime.Now;
            
            var applicationDbContext = _context.Appointment.Include(a => a.Barber).Include(a => a.Customer).Include(a => a.Service).Where(a => a.DateAndTime >= dateToday).OrderBy(a => a.DateAndTime);
            // var data = await applicationDbContext.ToListAsync();
            //
            // return View(data);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Barber)
                .Include(a => a.Customer)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            ViewData["date"] = appointment.DateAndTime.ToString("yyyy-MM-dd HH:mm");

            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            ViewData["BarberId"] = new SelectList(_context.Barber, "BarberId", "BarberName");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName");
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName");
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,DateAndTime,BarberId,CustomerId,ServiceId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BarberId"] = new SelectList(_context.Barber, "BarberId", "BarberName", appointment.BarberId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", appointment.CustomerId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName", appointment.ServiceId);
            return View(appointment);
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["BarberId"] = new SelectList(_context.Barber, "BarberId", "BarberName", appointment.BarberId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", appointment.CustomerId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName", appointment.ServiceId);
            return View(appointment);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,DateAndTime,BarberId,CustomerId,ServiceId")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["BarberId"] = new SelectList(_context.Barber, "BarberId", "BarberName", appointment.BarberId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", appointment.CustomerId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName", appointment.ServiceId);
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Barber)
                .Include(a => a.Customer)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["date"] = appointment.DateAndTime.ToString("yyyy-MM-dd HH:mm");

            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.AppointmentId == id);
        }
    }
}
