namespace royectoInmobiliaria.net_MVC_.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tipo
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int TipoId { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }

