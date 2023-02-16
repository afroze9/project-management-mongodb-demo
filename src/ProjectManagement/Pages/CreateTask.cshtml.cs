using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using MongoDB.Entities;
using ProjectManagement.Models;
using Task = ProjectManagement.Models.Task;

namespace ProjectManagement.Pages;

public class CreateTaskModel : PageModel
{
    [BindProperty] public TaskCreationVM TaskCreationVM { get; set; }

    public List<PersonDropdownVM> People { get; set; }

    public async Task<IActionResult> OnGetAsync(string? projectId)
    {
        if (projectId == null)
        {
            return NotFound();
        }

        var people = await DB.Queryable<Person>().ToListAsync();

        if (People == null)
        {
            People = new List<PersonDropdownVM>();
        }
        else
        {
            People.Clear();
        }

        foreach (Person person in people)
        {
            People.Add(new PersonDropdownVM
            {
                Id = person.ID,
                DisplayName = person.DisplayName,
            });
        }
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(string? projectId)
    {
        Task task = new Task { Summary = TaskCreationVM.Summary, Description = TaskCreationVM.Description};
        await task.SaveAsync();

        if (TaskCreationVM.AsignedTo != null)
        {
            Person? person = await DB.Find<Person>().OneAsync(TaskCreationVM.AsignedTo);

            if (person != null)
            {
                await person.Tasks.AddAsync(task);
            }
        }
        
        Project? project = await DB.Find<Project>().OneAsync(projectId);
        await project.Tasks.AddAsync(task);

        return RedirectToPage("./Companies");
    }
}

public class TaskCreationVM
{
    public required string Summary { get; set; }
    public string Description { get; set; }
    
    public string? AsignedTo { get; set; }
}

public class PersonDropdownVM
{
    public string? Id { get; set; }
    public string DisplayName { get; set; }
}