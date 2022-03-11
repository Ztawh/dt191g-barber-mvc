using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using barber_mvc.Data;
using barber_mvc.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace barber_mvc.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: API/Appointment
        [HttpGet]
        [Route("/API/Appointment")]
        public async Task<List<Appointment>> GetAppointment()
        {
            return await _context.Appointment.ToListAsync();
        }
        
        [HttpGet]
        [Route("/API/AvailableAppointments/{add?}")]
        public async Task<List<DateTime>> GetAvailableAppointments(int add)
        {
            
            var dateStart = DateTime.Now;
            var dateEnd = dateStart.AddDays(7);

            if (add > 0)
            {
                dateStart = DateTime.Now.AddDays(add);
                dateEnd = dateStart.AddDays(7);
            }
            
            var times = new List<DateTime>();
            
            var unavailable = await _context.Appointment.Where(appointment =>
                appointment.DateAndTime > dateStart && appointment.DateAndTime < dateEnd).ToListAsync();

            for (int i = 0; i < 7; i++)
            {
                //dates.Add(dateStart.AddDays(i));
                var day = dateStart.AddDays(i);

                for (int j = 9; j < 18; j++)
                {
                    var dateAndTime = DateTime.Parse($"{day.Year}-{day.Month:00}-{day.Day:00}T{j:00}:00");

                    foreach (var appointment in unavailable)
                    {
                        //var unavailableTime = DateTime.Parse($"{appointment.DateAndTime.Year}-{appointment.DateAndTime.Month:00}-{appointment.DateAndTime.Day:00}T{j:00}");
                        if (appointment.DateAndTime != dateAndTime)
                        {
                            times.Add(dateAndTime);
                        }
                    }
                    
                    // Sortera bort unavailable tider, lägg till allt som inte är unavailable i times
                }
            }
            return times;
        }

        // GET: API/Appointment/{id}
        [HttpGet]
        [Route("/API/Appointment/{id}")]
        public async Task<List<Appointment>> Get(int id)
        {
            return await _context.Appointment.ToListAsync();
        }
        
        // POST
        [HttpPost]
        [Route("/API/Appointment")]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            // Finns tiden redan bokad? Då får du inte boka
            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostAppointment", appointment);
        }
        
        // GET: API/Customer
        [HttpGet]
        [Route("/API/Customer")]
        public async Task<List<Customer>> GetCustomer()
        {
            return await _context.Customer.ToListAsync();
        }
        
        // GET: API/Customer/{id}
        [Route("/API/Customer/{id}")]
        public async Task<List<Customer>> GetCustomer(int id)
        {
            return await _context.Customer.ToListAsync();
        }
        
        // GET: API/Barber
        [HttpGet]
        [Route("/API/Barber")]
        public async Task<List<Barber>> GetBarber()
        {
            return await _context.Barber.ToListAsync();
        }
        
        // GET: API/Barber/{id}
        [HttpGet]
        [Route("/API/Barber/{id}")]
        public async Task<List<Barber>> GetBarber(int id)
        {
            return await _context.Barber.ToListAsync();
        }
        
        // GET: API/Service
        [HttpGet]
        [Route("/API/Service")]
        public async Task<List<Service>> GetService()
        {
            return await _context.Service.ToListAsync();
        }
        
        // GET: API/Service/{id}
        [Route("/API/Service/{id}")]
        public async Task<List<Service>> GetService(int id)
        {
            return await _context.Service.ToListAsync();
        }


    }
}
