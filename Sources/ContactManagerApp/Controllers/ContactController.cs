using Microsoft.AspNetCore.Mvc;
using Contact = ContactManagerApp.Models.Contact;

namespace ContactManagerApp.Controllers
{
    public class ContactController : Controller
    {
        private static List<Contact> contacts = new();

        public IActionResult Index()
        {
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Id = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
                contacts.Add(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public IActionResult Edit(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact == null) return NotFound();

            if (ModelState.IsValid)
            {
                existingContact.Name = contact.Name;
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.Email = contact.Email;
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public IActionResult Delete(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null) contacts.Remove(contact);
            return RedirectToAction("Index");
        }
    }
}
