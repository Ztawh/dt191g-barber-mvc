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
        [Route("/API/AvailableAppointments/{add?}/{barber?}")]
        public async Task<List<DateTime>> GetAvailableAppointments(int add, int barber)
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
                appointment.DateAndTime > dateStart && appointment.DateAndTime < dateEnd && appointment.BarberId == barber).ToListAsync();

            for (int i = 0; i < 7; i++)
            {
                //dates.Add(dateStart.AddDays(i));
                var day = dateStart.AddDays(i);

                for (int j = 9; j < 18; j++)
                {
                    var dateAndTime = DateTime.Parse($"{day.Year}-{day.Month:00}-{day.Day:00}T{j:00}:00");

                    // Om det finns bokade tider
                    if (unavailable.Count > 0)
                    {
                        bool found = false;
                        foreach (var appointment in unavailable)
                        {
                            // Finns datumet i bokade tider?
                            if (appointment.DateAndTime == dateAndTime)
                            {
                                found = true;
                                break;
                                // times.Add(dateAndTime);
                            }
                        }
                        
                        // Om den fanns 
                        if (!found)
                        {
                            times.Add(dateAndTime);
                        }
                    }
                    else // Lägg till alla tider
                    {
                        times.Add(dateAndTime);
                    }
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
        
        // POST Customer
        [HttpPost]
        [Route("/API/Customer")]
        public async Task<ActionResult<Customer>>  PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostCustomer", customer);
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
