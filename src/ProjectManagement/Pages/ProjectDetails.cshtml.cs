using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Entities;
using ProjectManagement.Models;
using Task = ProjectManagement.Models.Task;

namespace ProjectManagement.Pages;

public class ProjectDetailsModel : PageModel
{
    public ProjectDetailsVM Project { get; set; }
    
    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Project? project = await DB.Find<Project>().OneAsync(id);
        List<Task> tasks = project.Tasks.ToList();

        if (project == null)
        {
            return NotFound();
        }

        Project = new ProjectDetailsVM
        {
            ID = project.ID,
            Name = project.Name,
            Tasks = GetTaskListingVMs(project.Tasks.ToList()),
        };

        return Page();
    }
    
    private List<TaskListingVM> GetTaskListingVMs(List<Task> tasks)
    {
        List<TaskListingVM> output = new List<TaskListingVM>();

        foreach (Task task in tasks)
        {
            output.Add(new TaskListingVM
            {
                ID = task.ID,
                Summary = task.Summary,
                IsComplete = task.IsComplete,
                CreatedOn = task.CreatedOn,
                ModifiedOn = task.ModifiedOn,
            });
        }
        
        return output;
    }
}

public class ProjectDetailsVM
{
    public string ID { get; set; }
    
    public string Name { get; set; }
    
    public List<TaskListingVM> Tasks { get; set; }
}

public class TaskListingVM
{
    public string ID { get; set; }

    public required string Summary { get; set; }
    
    public bool IsComplete { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}