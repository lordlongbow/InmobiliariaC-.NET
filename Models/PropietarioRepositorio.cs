using MySql.Data.MySqlClient;

namespace royectoInmobiliaria.net_MVC_.Models;

public class PropietarioReositorio
{
    string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";
    public PropietarioReositorio()
    {

    }

    public int Crear(Propietario propietario)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
           string query = " INSERT INTO propietario (nombre, apellido, dni, domicilio, telefono) VALUES (@nombre, @apellido, @dni, @domicilio, @telefono); SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@dni", propietario.Dni);
                command.Parameters.AddWithValue("@domicilio", propietario.Domicilio);
                command.Parameters.AddWithValue("@telefono", propietario.Telefono);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                propietario.Id = res;
                connection.Close();
            }
        }

        return res;
    }

    public List<Propietario> GetPropietarios()
    {
       List<Propietario> Propietarios = new List<Propietario>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
           string query ="SELECT `Id`, `nombre`, `apellido`, `dni`, `domicilio`, `telefono` FROM propietario;";
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Propietario propietario = new Propietario
                        {
                            Id = reader.GetInt32(nameof(Propietario.Id)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetString(nameof(Propietario.Dni)),
                            Domicilio = reader.GetString(nameof(Propietario.Domicilio)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono))
                        };
                        Propietarios.Add(propietario);
                    }
                }
            }
            connection.Close();
        }
        return Propietarios;
    }


public Propietario getPropietario(int id){
    Propietario propietario = null;

     using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
           string query ="SELECT `Id`, `nombre`, `apellido`, `dni`, `domicilio`, `telefono` FROM propietario WHERE Id = @id;";
            using (var command = new MySqlCommand(query, connection))
            {
                
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                  if (reader.Read())
                    {
                        propietario = new Propietario
                        {
                            Id = reader.GetInt32(nameof(Propietario.Id)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetString(nameof(Propietario.Dni)),
                            Domicilio = reader.GetString(nameof(Propietario.Domicilio)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono))
                        };
                    
                    }
                }
            }
            connection.Close();
        }
        return propietario;
}

 public int Modificar(Propietario propietario)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query ="UPDATE propietario SET nombre = @nombre, apellido = @apellido, dni = @dni, domicilio = @domicilio, telefono = @telefono WHERE Id = @Id;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", propietario.Id);
                command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@dni", propietario.Dni);
                command.Parameters.AddWithValue("@domicilio", propietario.Domicilio);
                command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();

            }
        }
        return res;
    }

 public int Borrar(int Id)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query ="DELETE FROM propietario WHERE Id = @Id;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", Id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }


}

