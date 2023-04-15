using MySql.Data.MySqlClient;
namespace royectoInmobiliaria.net_MVC_.Models;

public class TipoRepositorio {

      string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";
    public List<Tipo> GetTipos() {
        List<Tipo> tipos = new List<Tipo>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query = @"SELECT `id_tipo` as TipoId,`descripcion` FROM `tipos`;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        Tipo tipo = new Tipo
                        {
                            TipoId = reader.GetInt32(nameof(tipo.TipoId)),
                            Descripcion = reader.GetString(nameof(tipo.Descripcion))
                        };
                        tipos.Add(tipo);

                    }
                    return tipos;
                }
            }
        }
    }
}
