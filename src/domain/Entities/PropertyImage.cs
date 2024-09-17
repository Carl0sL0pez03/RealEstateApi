using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents an image associated with a real estate property.
/// Contains details such as the file path, whether the image is enabled, and the associated property.
/// </summary>
public class PropertyImage
{
    /// <summary>
    /// The unique identifier for the property image.
    /// </summary>
    [Key]
    public int IdPropertyImage { get; set; }

    /// <summary>
    /// The foreign key representing the associated property.
    /// This field is optional.
    /// </summary>
    public int? IdProperty { get; set; }

    /// <summary>
    /// The file path or name of the image.
    /// This field is optional.
    /// </summary>
    public string? File { get; set; }

    /// <summary>
    /// Indicates whether the image is enabled (active or visible).
    /// This field is optional.
    /// </summary>
    public bool? Enabled { get; set; }

    /// <summary>
    /// The associated property for this image. This is a navigation property representing
    /// the relationship to the <see cref="Property"/> entity.
    /// </summary>
    public Property? Property { get; set; }
}
