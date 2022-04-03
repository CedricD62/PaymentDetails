using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentApi2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public PaymentDetailController(PaymentDetailContext context)
        {
            _context = context;
        }

        //Get: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            return await _context.paymentDetails.ToListAsync();
        }

        //GET: api//PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.paymentDetails.FindAsync(id);
            if(paymentDetail == null)
            {
                return NotFound();
            }
            return paymentDetail;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail (int id, PaymentDetail paymentDetail)
        {
            if(id != paymentDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExist(id)){
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        //POST: api/PaimentDetail
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.paymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.Id }, paymentDetail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.paymentDetails.FindAsync(id);
            if(paymentDetail == null)
            {
                return NotFound();
            }

            _context.paymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PaymentDetailExist(int id)
        {
            return _context.paymentDetails.Any(p => p.Id == id);
        }
    }
}
