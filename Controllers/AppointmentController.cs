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

        // GET: Available dates
        // public async Task<IActionResult> Available(int add, int barber)
        // {
        //      
        //     var dateStart = DateTime.Now;
        //     var dateEnd = dateStart.AddDays(7);
        //
        //     if (add > 0)
        //     {
        //         dateStart = DateTime.Now.AddDays(add);
        //         dateEnd = dateStart.AddDays(7);
        //     }
        //     
        //     var times = new List<DateTime>();
        //
        //     var unavailable = await _context.Appointment.Where(appointment =>
        //         appointment.DateAndTime > dateStart && appointment.DateAndTime < dateEnd && appointment.BarberId == barber).ToListAsync();
        //
        //     for (int i = 0; i < 7; i++)
        //     {
        //         //dates.Add(dateStart.AddDays(i));
        //         var day = dateStart.AddDays(i);
        //
        //         for (int j = 9; j < 18; j++)
        //         {
        //             var dateAndTime = DateTime.Parse($"{day.Year}-{day.Month:00}-{day.Day:00}T{j:00}:00");
        //
        //             // Om det finns bokade tider
        //             if (unavailable.Count > 0)
        //             {
        //                 bool found = false;
        //                 foreach (var appointment in unavailable)
        //                 {
        //                     // Finns datumet i bokade tider?
        //                     if (appointment.DateAndTime == dateAndTime)
        //                     {
        //                         found = true;
        //                         break;
        //                         // times.Add(dateAndTime);
        //                     }
        //                 }
        //                 
        //                 // Om den fanns 
        //                 if (!found)
        //                 {
        //                     times.Add(dateAndTime);
        //                 }
        //             }
        //             else // Lägg till alla tider
        //             {
        //                 times.Add(dateAndTime);
        //             }
        //         }
        //     }
        //     //return times;
        //
        //     ViewData["AvailableDates"] = new SelectList(times, "DateAndTime", "DateAndTime");
        // }
        
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
