using MongoDB.Entities;

namespace ProjectManagement.Models;

[Collection("task")]
public class Task : Entity, ICreatedOn, IModifiedOn
{
    public Task()
    {
        this.InitOneToMany(() => AssignedTo);
    }

    required public string Summary { get; set; }

    public string? Description { get; set; }

    public bool IsComplete { get; set; }

    [InverseSide] public One<Project> Project { get; set; }

    [OwnerSide] public Many<Person> AssignedTo { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}