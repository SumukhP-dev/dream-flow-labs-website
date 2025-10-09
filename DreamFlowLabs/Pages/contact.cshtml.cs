using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DreamflowLabs.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Message { get; set; }

        public bool Submitted { get; set; }

        public void OnGet() { }

        public void OnPost()
        {
            // TODO: You could integrate email sending logic here
            Submitted = true;
        }
    }
}