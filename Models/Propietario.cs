namespace royectoInmobiliaria.net_MVC_.Models{
   public class Propietario{
    public int Id{get; set;}
    public String? Nombre{get; set;}
    public String? Apellido {get; set;}
    public String? Dni {get; set;}
    public String? Domicilio{get; set;}
    public String? Telefono{get; set;}
    public List<Inmueble>? ListaInmuebles{get; set;}
   
    
   
}
 
}

