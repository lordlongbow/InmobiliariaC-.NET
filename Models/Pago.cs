namespace royectoInmobiliaria.net_MVC_.Models{


public class Pago{
    
    public int Id{get;set;}
    public double Precio{get; set;}
    public Inmueble? Inmueble{get; set;}
    public Contrato? Contrato {get;set;}
}
}