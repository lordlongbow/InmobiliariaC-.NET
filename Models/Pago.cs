namespace royectoInmobiliaria.net_MVC_.Models{

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pago{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     [Display (Name="Codigo de Pago")]
    public int PagoId {get;set;}
    [Required]
    public decimal Precio{get; set;}
    [Required]
    public DateTime Fecha{get; set;}
    [Required]
     [Display (Name="Cuota")]
    public int nro_pago{get;set;}

    public Contrato Contrato{get;set;}

    [ForeignKey("Contrato")]
    [Display (Name="Codigo de Contrato")]
    public int ContratoId{get;set;}

    public Inmueble Inmueble{get;set;}

    [ForeignKey("Inmueble")]
     [Display (Name="Codigo de Inmueble")]
    public int InmuebleId{get;set;}

}
}

//  dotnet-aspnet-codegenerator controller -name "InmuebleController" -outDir "Controllers" -namespace "royectoInmobiliaria.net_MVC_.Controllers" -f 