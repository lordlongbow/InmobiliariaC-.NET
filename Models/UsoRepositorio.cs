using MySql.Data.MySqlClient;
namespace royectoInmobiliaria.net_MVC_.Models;

public class UsoRepositorio {

      string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";
    public List<Uso> GetUsos() {
        List<Uso> usos = new List<Uso>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query = @"SELECT `id_uso` as UsoId,`descripcion` as DescripcionUso FROM `usos`;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                       Uso uso = new Uso()
                        {
                           UsoId = reader.GetInt32(nameof(Uso.UsoId)),
                           DescripcionUso = reader.GetString(nameof(Uso.DescripcionUso))
                        };
                        usos.Add(uso);

                    }
                    return usos;
                }
            }
        }
    }
}
