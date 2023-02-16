using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Entities;
using ProjectManagement.Models;

namespace ProjectManagement.Pages;

public class CreateProjectModel : PageModel
{
    [BindProperty] public ProjectCreationVM ProjectCreationVM { get; set; }
    
    public async Task<IActionResult> OnGetAsync(string? companyId)
    {
        if (companyId == null)
        {
            return NotFound();
        }

        if (ProjectCreationVM == null)
        {
            ProjectCreationVM = new ProjectCreationVM();
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? companyId)
    {
        Project project = new Project { Name = ProjectCreationVM.Name };
        await project.SaveAsync();
        
        Company? company = await DB.Find<Company>().OneAsync(companyId);
        await company.Projects.AddAsync(project);
        
        return RedirectToPage("./Companies");
    }
}

public class ProjectCreationVM
{
    public string Name { get; set; }
}