using MongoDB.Entities;

namespace ProjectManagement.Models;

[Collection("company")]
public class Company : Entity, ICreatedOn, IModifiedOn
{
    public Company()
    {
        this.InitOneToMany(() => Projects);
    }

    required public string Name { get; set; }

    [OwnerSide] public Many<Project> Projects { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}