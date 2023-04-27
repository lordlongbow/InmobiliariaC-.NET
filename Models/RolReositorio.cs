using MySql.Data.MySqlClient;

namespace royectoInmobiliaria.net_MVC_.Models;

public class RolReositorio
{

string queryString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";

public RolReositorio() { }

public List<Rol> GetRoles()
{
    List<Rol> roles = new List<Rol>();
    using (MySqlConnection connection = new MySqlConnection(queryString))
    {
        string query =
            @"SELECT `roid`,`Descricion` FROM `rol`";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Rol rol = new Rol();
                    rol.RolId = reader.GetInt32("roid");
                    rol.Descricion = reader.GetString("Descricion");
                    roles.Add(rol);
                }
            }
        }
    }
    return roles;
}


public Rol GetRol(int rolId)
{
    Rol rol = new Rol();
    using (MySqlConnection connection = new MySqlConnection(queryString))
    {
        string query =
            @"SELECT `roid`,`Descricion` FROM `rol` WHERE roid = @rolId";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rol.RolId = reader.GetInt32("roid");
                    rol.Descricion = reader.GetString("Descricion");
                }
            }
        }
        return rol;
    }




}
}