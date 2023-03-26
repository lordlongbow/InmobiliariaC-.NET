namespace royectoInmobiliaria.net_MVC_.Models;

public class Contrato
{
  //Consultar sobre los nulos en Inmueble, Inquilino y propietario, + errores del constructor
    public int Id { get; set; }
   // public Inmueble Inmueble { get; set; }
    public Inquilino? Inquilino { get; set; }
    public Propietario? propietario { get; set; }


}