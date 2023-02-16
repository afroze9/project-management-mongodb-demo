using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using MongoDB.Entities;
using ProjectManagement.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagement.Pages;

public class PeopleModel : PageModel
{
    public List<PersonListingVM> People { get; set; }

    public async Task OnGetAsync()
    {
        var people = await DB.Queryable<Person>().ToListAsync();
        People = GetPersonListingVMs(people);
    }

    private List<PersonListingVM> GetPersonListingVMs(List<Person> people)
    {
        List<PersonListingVM> output = new List<PersonListingVM>();

        foreach (Person person in people)
        {
            output.Add(new PersonListingVM
            {
                Email = person.Email,
                DisplayName = person.DisplayName,
                TaskCount = person.Tasks.Count(),
            });
        }
        
        return output;
    }
}

public class PersonListingVM
{
    public string DisplayName { get; set; }
    
    public string Email { get; set; }

    public int TaskCount { get; set; } = 0;
}