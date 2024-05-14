namespace royectoInmobiliaria.net_MVC_.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Inmueble
{
    //los decoradores decoran lo que esta abajo
    [Key] // indica que esta propiedad es la clave primaria de la tabla 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // indica que el valor de esta propiedad es generado automáticamente por la base de datos al insertar un nuevo registro
    public int InmuebleId { get; set; }
    
    [Required] // indica que este campo es requerido asi no le tengo que especificar con ?
    public string Direccion { get; set; }
    
    [Required]
    public decimal Precio { get; set; }
    
    [Required]
    public int CantAmbientes { get; set; }
    
    [Required]
    public int Latitud { get; set; }
    
    [Required]
    public int Longitud { get; set; }
    
    [ForeignKey("PropietarioId")] // llave foranea del propietario
    public Propietario Propietario { get; set; }
    
    [Required]
    public int PropietarioId {get;set;}
    
    [ForeignKey("TipoId")]// llave foranea del tipo
    public Tipo Tipo { get; set; }

    [Required]
    public int TipoId {get;set;}
    
    [ForeignKey("UsoId")]// llave foranea del uso
    public Uso Uso { get; set; }

    [Required]
    public int UsoId {get;set;}

    public bool Libre {get;set;}
}
