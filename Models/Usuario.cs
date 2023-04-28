namespace royectoInmobiliaria.net_MVC_.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UsuarioId { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Contrasenia { get; set; }
    [Required]
    [ForeignKey("RolId")]
    public string Rol { get; set; }
    
    public int RolId { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Apellido { get; set; }
  
  
    public string? foto { get; set; }

    public IFormFile? Fotofisica { get; set; }
}
