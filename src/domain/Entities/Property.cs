using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a real estate property, containing details such as the name, address, price, and ownership information.
/// Also includes relationships to property images and traces.
/// </summary>
public class Property
{
    /// <summary>
    /// The unique identifier for the property.
    /// </summary>
    [Key]
    public int IdProperty { get; set; }

    /// <summary>
    /// The name of the property. This field is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The address of the property. This field is optional.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// The price of the property. Stored as a decimal with precision (18, 4).
    /// This field is optional.
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal? Price { get; set; }

    /// <summary>
    /// The internal code for the property. This field is optional.
    /// </summary>
    public string? CodeInternal { get; set; }

    /// <summary>
    /// The year the property was built or registered. This field is optional.
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// The foreign key for the owner of the property.
    /// </summary>
    public int IdOwner { get; set; }

    /// <summary>
    /// The owner of the property. This is a navigation property representing the relationship to the <see cref="Owner"/> entity.
    /// </summary>
    public Owner? Owner { get; set; }

    /// <summary>
    /// The collection of images related to the property. This is a one-to-many relationship.
    /// </summary>
    public ICollection<PropertyImage>? PropertyImages { get; set; }

    /// <summary>
    /// The collection of traces related to the property, such as price changes or sales history.
    /// </summary>
    public ICollection<PropertyTrace>? PropertyTraces { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Property"/> class with default values.
    /// Initializes the <see cref="PropertyImages"/> collection.
    /// </summary>
    public Property()
    {
        PropertyImages = new List<PropertyImage>();
    }
}
