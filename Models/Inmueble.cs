namespace royectoInmobiliaria.net_MVC_.Models;

    public class Inmueble
    {

        public int Id {get;set;}
        public String? Domicilio {get; set;}
        public Propietario propietario {get; set;}
        public Inquilino? inquilino{get; set;}
        public double precio{get;set;}

        public Contrato? Contrato{get;set;}

        public Boolean Disponible{get; set;}
        public Inmueble(int id, string? domicilio, Propietario propietario, Inquilino? inquilino, double precio, Boolean Disponible)
        {
            Id = id;
            Domicilio = domicilio;
            this.propietario = propietario;
            this.inquilino = inquilino;
            this.precio = precio;
            this.Disponible = Disponible;
          
        }
    }
