using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using MongoDB.Entities;
using ProjectManagement.Models;

namespace ProjectManagement.Pages;

public class CompanyDetailsModel : PageModel
{
    public CompanyDetailsVM Company { get; set; }
    
    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Company? company = await DB.Find<Company>().OneAsync(id);
        List<Project> projects = company.Projects.ToList();

        if (company == null)
        {
            return NotFound();
        }

        Company = new CompanyDetailsVM
        {
            ID = company.ID,
            Name = company.Name,
            Projects = GetProjectListingVMs(company.Projects.ToList()),
        };

        return Page();
    }

    private List<ProjectListingVM> GetProjectListingVMs(List<Project> projects)
    {
        List<ProjectListingVM> output = new List<ProjectListingVM>();

        foreach (Project project in projects)
        {
            output.Add(new ProjectListingVM
            {
                ID = project.ID,
                Name = project.Name,
                CreatedOn = project.CreatedOn,
                ModifiedOn = project.ModifiedOn,
            });
        }
        
        return output;
    }
}

public class CompanyDetailsVM
{
    public string ID { get; set; }
    
    public string Name { get; set; }
    
    public List<ProjectListingVM> Projects { get; set; }
}

public class ProjectListingVM
{
    public string ID { get; set; }

    public required string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}