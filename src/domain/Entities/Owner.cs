using System.ComponentModel.DataAnnotations;

public class Owner
{
    [Key]
    public int IdOwner { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? Photo { get; set; }
    public DateTime? Birthday { get; set; }


    /* Relationship */
    public ICollection<Property>? Properties { get; set; }
    /* Relationship */
}