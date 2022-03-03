using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using barber_mvc.Data;
using barber_mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: API/Appointment/{id}
        [Route("/API/Appointment/{id}")]
        public async Task<List<Appointment>> Get(int id)
        {
            return await _context.Appointment.ToListAsync();
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
