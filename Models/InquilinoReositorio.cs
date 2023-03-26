using MySql.Data.MySqlClient;

namespace royectoInmobiliaria.net_MVC_.Models;

public class InquilinoReositorio
{
    string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";
    public InquilinoReositorio()
    {

    }

    public int Crear(Inquilino inquilino)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query = "INSERT INTO inquilino (nombre, apellido, dni, domicilio, telefono) VALUES (@nombre, @apellido, @dni, @domicilio, @telefono); SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
                command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
                command.Parameters.AddWithValue("@dni", inquilino.Dni);
                command.Parameters.AddWithValue("@domicilio", inquilino.Domicilio);
                command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                inquilino.Id = res;
                connection.Close();
            }
        }

        return res;
    }

    public List<Inquilino> GetInquilinos()
    {
        List<Inquilino> Inquilinos = new List<Inquilino>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query = @"SELECT `Id`, `nombre`, `apellido`, `dni`, `domicilio`, `telefono` FROM Inquilino;";
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inquilino inquilino = new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                            Dni = reader.GetString(nameof(Inquilino.Dni)),
                            Domicilio = reader.GetString(nameof(Inquilino.Domicilio)),
                            Telefono = reader.GetString(nameof(Inquilino.Telefono))
                        };
                        Inquilinos.Add(inquilino);
                    }
                }
            }
            connection.Close();
        }
        return Inquilinos;
    }


public Inquilino getInquilino(int Id){
    Inquilino inquilino = null;

     using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query = @"SELECT `Id`, `nombre`, `apellido`, `dni`, `domicilio`, `telefono` FROM Inquilino WHERE Id = @Id;";
            using (var command = new MySqlCommand(query, connection))
            {
                
                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                  if (reader.Read())
                    {
                        inquilino = new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                            Dni = reader.GetString(nameof(Inquilino.Dni)),
                            Domicilio = reader.GetString(nameof(Inquilino.Domicilio)),
                            Telefono = reader.GetString(nameof(Inquilino.Telefono))
                        };
                    
                    }
                }
            }
            connection.Close();
        }
        return inquilino;
}

 public int Modificar(Inquilino inquilino)
    {
        int res = 0;
        var id = inquilino.Id;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
           string query = @"UPDATE inquilino SET nombre = @nombre, apellido = @apellido, dni = @dni, domicilio = @domicilio, telefono = @telefono WHERE Id =@id ;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
                command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
                command.Parameters.AddWithValue("@dni", inquilino.Dni);
                command.Parameters.AddWithValue("@domicilio", inquilino.Domicilio);
                command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
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
            string query = @"DELETE FROM inquilino WHERE ID = @Id;";
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