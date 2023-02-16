using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Entities;
using ProjectManagement.Models;

namespace ProjectManagement.Pages;

public class CreatePersonModel : PageModel
{
    [BindProperty]
    public PersonVM PersonVM { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Person person = new Person { DisplayName = PersonVM.DisplayName, Email = PersonVM.Email };
        await person.SaveAsync();
        return RedirectToPage("./People");
    }
}

public class PersonVM
{
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
}