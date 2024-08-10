using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using payroll.Models;
using payroll.Data;
using ServiceReference1; // Namespace for the SOAP service reference

namespace Payroll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingEntriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly SSWSSoapClient _soapClient; // Inject SOAP client

        public AccountingEntriesController(DataContext context, SSWSSoapClient soapClient)
        {
            _context = context;
            _soapClient = soapClient; // Initialize SOAP client
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountingEntries>>> GetAccountingEntries()
        {
            return await _context.AccountingEntries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountingEntries>> GetAccountingEntry(int id)
        {
            var accountingEntry = await _context.AccountingEntries.FindAsync(id);
            if (accountingEntry == null)
            {
                return NotFound();
            }
            return accountingEntry;
        }

        [HttpPost]
        public async Task<ActionResult<AccountingEntries>> PostAccountingEntry(AccountingEntries accountingEntry)
        {
            _context.AccountingEntries.Add(accountingEntry);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAccountingEntry), new { id = accountingEntry.EntryId }, accountingEntry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountingEntry(int id, AccountingEntries accountingEntry)
        {
            if (id != accountingEntry.EntryId)
            {
                return BadRequest();
            }

            _context.Entry(accountingEntry).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.AccountingEntries.Any(e => e.EntryId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountingEntry(int id)
        {
            var accountingEntry = await _context.AccountingEntries.FindAsync(id);
            if (accountingEntry == null)
            {
                return NotFound();
            }

            _context.AccountingEntries.Remove(accountingEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // New method to send data to the SOAP service
        [HttpPost("send-to-soap")]
        public async Task<IActionResult> SendToSoapService([FromBody] AccountingEntries accountingEntry)
        {
            try
            {
                // Map AccountingEntries to AsientoContableRequestBody
                var soapRequest = new AsientoContableRequestBody
                {
                    idAuxiliarOrigen = accountingEntry.EmployeeId, // Map EmployeeId to idAuxiliarOrigen
                    descripcion = accountingEntry.Description,
                    cuentaDB = int.Parse(accountingEntry.Account), // Adjust mapping as necessary
                    cuentaCR = int.Parse(accountingEntry.Account), // Adjust mapping as necessary
                    monto = (double)accountingEntry.EntryAmount
                };

                // Call the SOAP service
                var response = await _soapClient.AsientoContableAsync(
                    soapRequest.idAuxiliarOrigen,
                    soapRequest.descripcion,
                    soapRequest.cuentaDB,
                    soapRequest.cuentaCR,
                    soapRequest.monto
                );

                if (response.Body != null) // Or another success condition
                {
                    return Ok(new { message = "Asiento Contable was successful." });
                }
                else
                {
                    return BadRequest(new { message = "Operation failed." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
