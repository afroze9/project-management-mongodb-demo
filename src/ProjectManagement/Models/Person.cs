using MongoDB.Entities;

namespace ProjectManagement.Models;

[Collection("person")]
public class Person : Entity, ICreatedOn, IModifiedOn
{
    public Person()
    {
        this.InitOneToMany(() => Tasks);
    }

    public string DisplayName { get; set; }

    public string Email { get; set; }

    [InverseSide] public Many<Task> Tasks { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}