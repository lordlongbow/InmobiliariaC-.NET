namespace royectoInmobiliaria.net_MVC_.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contrato
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     [Display(Name = "Codigo de Contrato")]
    public int ContratoId { get; set; }
   
    [Display(Name = "Fecha de Firma")]
    public DateTime FechaInicio { get; set; }
  
    [Display(Name = "Fecha Finalizacion")]
    public DateTime FechaFinalizacion { get; set; }
  
    public Inmueble? Inmueble { get; set; }
    [Display(Name = "Inmueble")]
    [ForeignKey("InmuebleId")]
    public int InmuebleId { get; set; }
  
    public Inquilino? Inquilino { get; set; }
     [Display(Name = "Nombre Inquilino")]
    [ForeignKey("InquilinoId")]
    public int InquilinoId { get; set; }


 
}