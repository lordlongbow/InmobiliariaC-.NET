using MySql.Data.MySqlClient;

namespace royectoInmobiliaria.net_MVC_.Models;

public class PagoReositorio
{
    string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";

    public List<Pago> GetPagos()
    {
        List<Pago> pagos = new List<Pago>();
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            MySqlCommand comando = new MySqlCommand(
                "SELECT `id_pago` as PagoId,`nroPago`as nro_pago,`importe` as Precio,`fecha` FROM `pago`",
                conexion
            );
            using (MySqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Pago pago = new Pago();
                    pago.PagoId = reader.GetInt32(nameof(Pago.PagoId));
                    pago.Precio = reader.GetDecimal(nameof(Pago.Precio));
                    pago.Fecha = reader.GetDateTime(nameof(Pago.Fecha));
                    pago.nro_pago = reader.GetInt32(nameof(Pago.nro_pago));
                    pagos.Add(pago);
                }
            }
        }
        return pagos;
    }
    public Pago GetPago()
    {
        Pago pago = new Pago();
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            MySqlCommand comando = new MySqlCommand("SELECT * FROM pago", conexion);
            using (MySqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    pago.PagoId = reader.GetInt32(nameof(Pago.PagoId));
                    pago.Precio = reader.GetDecimal(nameof(Pago.Precio));
                    pago.Fecha = reader.GetDateTime(nameof(Pago.Fecha));
                    pago.nro_pago = reader.GetInt32(nameof(Pago.nro_pago));
                }
            }
        }
        return pago;
    }
    public int Crear()
    {
        Pago pago = new Pago();
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            var query =
                "INSERT INTO `pago`(`nroPago`,`importe`,`fecha`) VALUES (@nroPago,@importe,@fecha);";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nroPago", pago.nro_pago);
            comando.Parameters.AddWithValue("@importe", pago.Precio);
            comando.Parameters.AddWithValue("@fecha", pago.Fecha);
            comando.ExecuteNonQuery();
        }
        return 0;
    }
    public int Modificar(int id)
    {
        Pago pago = new Pago();
        using (MySqlConnection conexion = new MySqlConnection(connectingString))
        {
            conexion.Open();
            var query =
                "UPDATE `pago` SET `nroPago`=@nroPago,`importe`=@importe,`fecha`=@fecha WHERE `id_pago`=@id";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@nroPago", pago.nro_pago);
            comando.Parameters.AddWithValue("@importe", pago.Precio);
            comando.Parameters.AddWithValue("@fecha", pago.Fecha);
            comando.Parameters.AddWithValue("@id_pago", pago.PagoId);
            comando.ExecuteNonQuery();
        }
        return 0;
    }
    public int Borrar(int id){

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
