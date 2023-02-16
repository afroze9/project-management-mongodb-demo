using MongoDB.Entities;

namespace ProjectManagement.Models;

[Collection("project")]
public class Project : Entity, ICreatedOn, IModifiedOn
{
    public Project()
    {
        this.InitOneToMany(() => Tasks);
    }

    required public string Name { get; set; }

    [InverseSide] public One<Company> Company { get; set; }

    [OwnerSide] public Many<Task> Tasks { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}