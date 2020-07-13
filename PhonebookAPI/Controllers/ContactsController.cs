using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonebookAPI.Models;

// ReSharper disable once IdentifierTypo
namespace PhonebookAPI.Controllers
{
    [Route("api/Contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactContext _context;

        public ContactsController(ContactContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.Include(a => a.Number).ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(long id)
        {
            var contact = await _context.Contacts.Include(a => a.Number).FirstOrDefaultAsync(i => i.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(long id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            var existingParent = _context.Contacts
                .Where(p => p.Id == contact.Id)
                .Include(p => p.Number)
                .SingleOrDefault();

            if (existingParent != null)
            {
                // Update parent
                _context.Entry(existingParent).CurrentValues.SetValues(contact);

                // Delete children
                foreach (var existingChild in existingParent.Number.ToList())
                {
                    //if (!contact.Number.Any(c => c.Id == existingChild.Id))
                    //    _context.Numbers.Remove(existingChild);

                    _context.Numbers.Remove(existingChild);
                }

                // Update and Insert children
                foreach (var childModel in contact.Number)
                {
                    //var existingChild = existingParent.Number
                    //    .Where(c => c.Id == childModel.Id)
                    //    .SingleOrDefault();

                    //if (existingChild != null)
                    //    // Update child
                    //    _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                    //else
                    //{
                    //    // Insert child
                    //    var newChild = new Number
                    //    {
                    //        PhoneNumber = childModel.PhoneNumber,
                    //        ContactId = childModel.ContactId
                    //        //...
                    //    };
                    //    existingParent.Number.Add(newChild);
                    //}

                    var newChild = new Number
                    {
                        PhoneNumber = childModel.PhoneNumber,
                        ContactId = childModel.ContactId
                        //...
                    };
                    _context.Numbers.Add(newChild);
                }
            }

            //_context.Entry(contact).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!ContactExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            try {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                /*Empty, possible NULL Id*/
            }

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(long id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(long id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }

        [Route("updatedb")]
        public async void UpdateDb()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
