using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Entities;
using ProjectManagement.Models;

namespace ProjectManagement.Pages;

public class CreateCompanyModel : PageModel
{
    [BindProperty]
    public CompanyVM CompanyVM { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Company company = new Company { Name = CompanyVM.Name };
        await company.SaveAsync();
        return RedirectToPage("./Companies");
    }
}

public class CompanyVM
{
    public required string Name { get; set; }
}