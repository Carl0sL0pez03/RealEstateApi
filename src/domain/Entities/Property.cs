using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Property
{
    [Key]
    public int IdProperty { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? Price { get; set; }
    public string? CodeInternal { get; set; }
    public int? Year { get; set; }

    /* Foreign Key for Owner */
    public int IdOwner { get; set; }
    public Owner? Owner { get; set; }
    /* Foreign Key for Owner */

    /* Relationships */
    public ICollection<PropertyImage>? PropertyImages { get; set; }
    public ICollection<PropertyTrace>? PropertyTraces { get; set; }
    /* Relationships */

    public Property()
    {
        PropertyImages = new List<PropertyImage>();
    }
}