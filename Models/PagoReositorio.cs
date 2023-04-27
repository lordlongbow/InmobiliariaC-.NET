using MySql.Data.MySqlClient;

namespace royectoInmobiliaria.net_MVC_.Models;

public class PagoReositorio
{
    string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";

    //SELECT pago.id_pago, pago.nroPago, pago.importe, pago.fecha, pago.ContratoId, pago.InmuebleId , contrato.id_contrato, inmueble.precio, inmueble.direccion, contrato.id_inquilino, inquilino.nombre, inquilino.apellido FROM `pago` INNER JOIN contrato On contrato.id_contrato = pago.ContratoId INNER JOIN inmueble ON inmueble.id_inmueble = pago.InmuebleId INNER JOIN inquilino ON inquilino.Id = contrato.id_inquilino;

    public List<Pago> GetPagosAumentado()
    {
        List<Pago> lista = new List<Pago>();

        string query =
            @"SELECT pago.id_pago, pago.nroPago, pago.importe, pago.fecha, pago.ContratoId, pago.InmuebleId , contrato.id_contrato as ContratoID, inmueble.precio, inmueble.direccion, contrato.id_inquilino, inquilino.nombre, inquilino.apellido FROM `pago` 
        INNER JOIN contrato On contrato.id_contrato = pago.ContratoId 
        INNER JOIN inmueble ON inmueble.id_inmueble = pago.InmuebleId 
        INNER JOIN inquilino ON inquilino.Id = contrato.id_inquilino; 
";
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            using (MySqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Inmueble inmueble = new Inmueble();
                    inmueble.InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId));
                    inmueble.Precio = reader.GetDecimal(nameof(Inmueble.Precio));
                    inmueble.Direccion = reader.GetString(nameof(Inmueble.Direccion));
                    Pago pago = new Pago();
                    pago.PagoId = reader.GetInt32(nameof(Pago.PagoId));
                    pago.Precio = reader.GetDecimal(nameof(Pago.Precio));
                    pago.Fecha = reader.GetDateTime(nameof(Pago.Fecha));
                    pago.nro_pago = reader.GetInt32(nameof(Pago.nro_pago));
                    pago.ContratoId = reader.GetInt32(nameof(Pago.ContratoId));
                    pago.InmuebleId = reader.GetInt32(nameof(Pago.InmuebleId));
                    Contrato contrato = new Contrato();
                    contrato.ContratoId = reader.GetInt32(nameof(Pago.ContratoId));
                    contrato.InquilinoId = reader.GetInt32(nameof(Pago.ContratoId));
                    Inquilino inquilino = new Inquilino();
                    inquilino.Id = reader.GetInt32(nameof(Pago.ContratoId));
                    inquilino.Nombre = reader.GetString(nameof(Inquilino.Nombre));
                    inquilino.Apellido = reader.GetString(nameof(Inquilino.Nombre));
                    lista.Add(pago);
                }
            }
        }
        return lista;
    }

    public Pago GetPagoAumentado(int id)
    {
        Pago pago = new Pago();

        string query =
            @"SELECT `id_pago` as PagoId,`nroPago` as nro_pago,`importe` as Precio,`fecha`,`ContratoId`,`InmuebleId`  FROM pago INNER JOIN contrato On 
        WHERE pago.id_pago = @id";
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id", id);
            using (MySqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Inmueble inmueble = new Inmueble();
                    inmueble.InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId));
                    inmueble.Precio = reader.GetDecimal(nameof(Inmueble.Precio));
                    inmueble.Direccion = reader.GetString(nameof(Inmueble.Direccion));
                    pago.PagoId = reader.GetInt32(nameof(Pago.PagoId));
                    pago.Precio = reader.GetDecimal(nameof(Pago.Precio));
                    pago.Fecha = reader.GetDateTime(nameof(Pago.Fecha));
                    pago.nro_pago = reader.GetInt32(nameof(Pago.nro_pago));
                    pago.ContratoId = reader.GetInt32(nameof(Pago.ContratoId));
                    pago.InmuebleId = reader.GetInt32(nameof(Pago.InmuebleId));
                    Contrato contrato = new Contrato();
                    contrato.ContratoId = reader.GetInt32(nameof(Pago.ContratoId));
                    contrato.InquilinoId = reader.GetInt32(nameof(Pago.ContratoId));
                    Inquilino inquilino = new Inquilino();
                    inquilino.Id = reader.GetInt32(nameof(Pago.ContratoId));
                    inquilino.Nombre = reader.GetString(nameof(Inquilino.Nombre));
                    inquilino.Apellido = reader.GetString(nameof(Inquilino.Nombre));
                }

                return pago;
            }
        }
    }

    public List<Pago> GetPagos()
    {
        List<Pago> lista = new List<Pago>();

        string query =
            @"SELECT  `id_pago` as PagoId,`nroPago` as nro_pago,`importe` as Precio,`fecha`,`ContratoId`,`InmuebleId` FROM pago 
        INNER JOIN contrato ON contrato.id_contrato = pago.ContratoId 
        INNER JOIN inmueble ON inmueble.id_inmueble = pago.InmuebleId";
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            using (MySqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Pago pago = new Pago();
                    pago.PagoId = reader.GetInt32(nameof(Pago.PagoId));
                    pago.Precio = reader.GetDecimal(nameof(Pago.Precio));
                    pago.Fecha = reader.GetDateTime(nameof(Pago.Fecha));
                    pago.nro_pago = reader.GetInt32(nameof(Pago.nro_pago));
                    pago.ContratoId = reader.GetInt32(nameof(Pago.ContratoId));
                    pago.InmuebleId = reader.GetInt32(nameof(Pago.InmuebleId));
                    lista.Add(pago);
                }
            }
        }
        return lista;
    }

   public Pago GetPago(int id)
{
    Pago pago = null; 
    string query =
        @"SELECT pago.id_pago, pago.nroPago, pago.importe, pago.fecha, pago.ContratoId, pago.InmuebleId , contrato.id_contrato, inmueble.precio, inmueble.direccion, contrato.id_inquilino, inquilino.nombre, inquilino.apellido FROM `pago` INNER JOIN contrato On contrato.id_contrato = pago.ContratoId INNER JOIN inmueble ON inmueble.id_inmueble = pago.InmuebleId INNER JOIN inquilino ON inquilino.Id = contrato.id_inquilino WHERE pago.id_pago = @id OR pago.id_pago = pago.ContratoId;";
    using (MySqlConnection conexion = new MySqlConnection(connectingString))
    {
        conexion.Open();
        MySqlCommand comando = new MySqlCommand(query, conexion);
        comando.Parameters.AddWithValue("@id", id);
        using (MySqlDataReader reader = comando.ExecuteReader())
        {
            while (reader.Read())
            {
                pago = new Pago();
                pago.PagoId = reader.GetInt32("id_pago");
                pago.Precio = reader.GetDecimal("importe");
                pago.Fecha = reader.GetDateTime("fecha");
                pago.nro_pago = reader.GetInt32("nroPago");
                pago.ContratoId = reader.GetInt32("ContratoId");
                pago.InmuebleId = reader.GetInt32("InmuebleId");
            }
        }
    }
    return pago;
}
public int CrearAumentado(Pago pago)
{
    using (MySqlConnection conexion = new MySqlConnection(connectingString))
    {
        conexion.Open();

        // Obtener el valor del inmueble del contrato si el ContratoId no es nulo
        decimal precioInmueble = 0;
        if (pago.ContratoId != null)
        {
            var queryContrato = "SELECT inmueble.precio FROM inmueble INNER JOIN contrato ON inmueble.id_inmueble = contrato.id_inmueble INNER JOIN pago ON contrato.id_contrato= pago.ContratoId; ";
            MySqlCommand comandoContrato = new MySqlCommand(queryContrato, conexion);
            comandoContrato.Parameters.AddWithValue("@ContratoId", pago.ContratoId);
            precioInmueble = Convert.ToDecimal(comandoContrato.ExecuteScalar());
        }

        // Obtener la fecha actual
        DateTime fechaActual = DateTime.Now;

        // Calcular el número de cuota actual y total si el ContratoId no es nulo
        int cuotaActual = 0;
        int cuotaTotal = 0;
        if (precioInmueble != 0)
        {
            var queryCuota = "SELECT COUNT(*) FROM pago WHERE ContratoId = @ContratoId;";
            MySqlCommand comandoCuota = new MySqlCommand(queryCuota, conexion);
            comandoCuota.Parameters.AddWithValue("@ContratoId", pago.ContratoId);
            cuotaActual = Convert.ToInt32(comandoCuota.ExecuteScalar()) + 1;
          
        }

        

        // Configurar los parámetros del comando para la inserción
        var query =
            "INSERT INTO `pago`(`nroPago`, `importe`, `fecha`, `ContratoId`, `InmuebleId`) VALUES (@nroPago, @importe, @fecha, @ContratoId, @InmuebleId); SELECT LAST_INSERT_ID();";
        MySqlCommand comando = new MySqlCommand(query, conexion);
        comando.Parameters.AddWithValue("@nroPago", pago.nro_pago);
        comando.Parameters.AddWithValue("@importe", precioInmueble);
        comando.Parameters.AddWithValue("@fecha", fechaActual);
        comando.Parameters.AddWithValue("@ContratoId", pago.ContratoId);
        comando.Parameters.AddWithValue("@InmuebleId", pago.InmuebleId);

        // Ejecutamos el comando y obtenemos el ID generado automáticamente
        int pagoId = Convert.ToInt32(comando.ExecuteScalar());

        return pagoId; 
    }
}














    public int Crear(Pago pago)
    {
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            var query =
                "INSERT INTO `pago`(`nroPago` as nro_pago,`importe` as Precio,`fecha`,`ContratoId`,`InmuebleId` ) VALUES (@nroPago,@importe,@fecha, @ContratoId, @InmuebleId); SELECT LAST_INSERT_ID();";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nroPago", pago.nro_pago);
            comando.Parameters.AddWithValue("@importe", pago.Precio);
            comando.Parameters.AddWithValue("@fecha", pago.Fecha);
            comando.Parameters.AddWithValue("@ContratoId", pago.ContratoId);
            comando.Parameters.AddWithValue("@InmuebleId", pago.InmuebleId);

            // Ejecutamos el comando y obtenemos el ID generado automáticamente
            int pagoId = Convert.ToInt32(comando.ExecuteScalar());

            return pagoId;
        }
    }

    public int Modificar(int id)
    {
        Pago pago = new Pago();
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            var query =
                "UPDATE `pago` SET `nroPago`=@nroPago,`importe`=@importe,`fecha`=@fecha, `ContratoId`=@ContratoId, `InmuebleId`=@InmuebleId  WHERE `pago.id_pago`=@id";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nroPago", pago.nro_pago);
            comando.Parameters.AddWithValue("@importe", pago.Precio);
            comando.Parameters.AddWithValue("@fecha", pago.Fecha);
            //comando.Parameters.AddWithValue("@id_pago", pago.PagoId);
            comando.Parameters.AddWithValue("@ContratoId", pago.ContratoId);
            comando.Parameters.AddWithValue("@InmuebleId", pago.InmuebleId);
            comando.ExecuteNonQuery();
        }
        return 0;
    }

    public int Borrar(int id)
    {
        Pago pago = new Pago();
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            var query = "DELETE FROM `pago` WHERE `id_pago`=@id";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        return 0;
    }
}
