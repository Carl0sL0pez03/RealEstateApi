using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PropertyTrace
{
    [Key]
    public int IdPropertyTrace { get; set; }
    public DateTime? DateSale { get; set; }
    public string? Name { get; set; }
    
    [Column(TypeName = "decimal(18, 4)")]
    public decimal? Value { get; set; }
    
    [Column(TypeName = "decimal(18, 4)")]
    public decimal? Tax { get; set; }

    /* Foreign Key */
    public int? IdProperty { get; set; }
    public Property? Property { get; set; }
    /* Foreign Key */
}