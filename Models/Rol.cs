namespace royectoInmobiliaria.net_MVC_.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Rol{
    [Key]
    public int RolId { get; set; }
    [Required]
    [StringLength(50)]
    public string Descricion { get; set; }
}