using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a trace or history record for a property, including details like sale date, value, and taxes.
/// This class is used to track changes in property prices and transactions over time.
/// </summary>
public class PropertyTrace
{
    /// <summary>
    /// The unique identifier for the property trace.
    /// </summary>
    [Key]
    public int IdPropertyTrace { get; set; }

    /// <summary>
    /// The date the property was sold or when the trace was created.
    /// This field is optional.
    /// </summary>
    public DateTime? DateSale { get; set; }

    /// <summary>
    /// The name or description of the trace or sale event.
    /// This field is optional.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The value associated with the trace, such as the sale price.
    /// Stored as a decimal with precision (18, 4).
    /// This field is optional.
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal? Value { get; set; }

    /// <summary>
    /// The tax associated with the transaction or trace.
    /// Stored as a decimal with precision (18, 4).
    /// This field is optional.
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal? Tax { get; set; }

    /// <summary>
    /// The foreign key representing the associated property for this trace.
    /// This field is optional.
    /// </summary>
    public int? IdProperty { get; set; }

    /// <summary>
    /// The associated property for this trace.
    /// This is a navigation property representing the relationship to the <see cref="Property"/> entity.
    /// </summary>
    public Property? Property { get; set; }
}