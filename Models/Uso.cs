namespace royectoInmobiliaria.net_MVC_.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    public class Uso {

        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsoId { get; set; }
        [Required]
        public string DescripcionUso { get; set; }
    }
