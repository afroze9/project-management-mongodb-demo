using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using MongoDB.Entities;
using ProjectManagement.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagement.Pages;

public class CompaniesModel : PageModel
{
    public List<Company> Companies { get; set; } = new List<Company>();

    public async Task OnGetAsync()
    {
        Companies = await DB.Queryable<Company>().ToListAsync();
    }
}