using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents the owner of one or more properties.
/// Contains personal details and a collection of properties related to the owner.
/// </summary>
public class Owner
{
    /// <summary>
    /// The unique identifier for the owner.
    /// </summary>
    [Key]
    public int IdOwner { get; set; }

    /// <summary>
    /// The name of the owner. This field is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The address of the owner. This field is optional.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// The URL or path to the owner's photo. This field is optional.
    /// </summary>
    public string? Photo { get; set; }

    /// <summary>
    /// The owner's birthday. This field is optional.
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// The collection of properties owned by this owner. This relationship is optional.
    /// </summary>
    public ICollection<Property>? Properties { get; set; }
}
