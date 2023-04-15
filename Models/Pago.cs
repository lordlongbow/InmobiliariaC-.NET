namespace royectoInmobiliaria.net_MVC_.Models{

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pago{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PagoId {get;set;}
    [Required]
    public decimal Precio{get; set;}
    [Required]
    public DateTime Fecha{get; set;}
    [Required]
    public int nro_pago{get;set;}
}
}

//  dotnet-aspnet-codegenerator controller -name "InmuebleController" -outDir "Controllers" -namespace "royectoInmobiliaria.net_MVC_.Controllers" -f 