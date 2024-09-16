using System.ComponentModel.DataAnnotations;

public class PropertyImage
{
    [Key]
    public int IdPropertyImage { get; set; }
    public int? IdProperty { get; set; }
    public string? File { get; set; }
    public bool? Enabled { get; set; }

    /* Foreing Key */
    public Property? Property {get; set;}
    /* Foreing Key */

}