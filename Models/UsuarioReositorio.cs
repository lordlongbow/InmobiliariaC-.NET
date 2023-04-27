namespace royectoInmobiliaria.net_MVC_.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UsuarioReositorio
{
    string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";

    public List<Usuario> GetUsuarios()
    {
        List<Usuario> usuarios = new List<Usuario>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"SELECT usuario.UsuarioId, usuario.Username, usuario.password as Contrasenia, usuario.Rolid, usuario.nombre, usuario.apellido, usuario.foto, rol.roid, rol.Descricion FROM usuario INNER JOIN rol ON rol.roid = usuario.RolId; ";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario()
                        {
                            UsuarioId = reader.GetInt32("UsuarioId"),
                            Username = reader.GetString("Username"),
                            Contrasenia = reader.GetString("Contrasenia"),
                            RolId = reader.GetInt32("Rolid"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            foto = reader.GetString("foto")
                        };
                        Rol rol = new Rol() { Descricion = reader.GetString("Descricion") };
                        usuario.Rol = rol.Descricion;
                        usuarios.Add(usuario);
                    }
                    return usuarios;
                }
            }
        }
    }

    public Usuario GetUsuario(int id)
    {
        Usuario usuario = new Usuario();

        
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"SELECT usuario.UsuarioId, usuario.Username, usuario.password as Contrasenia, usuario.Rolid, usuario.nombre, usuario.apellido, usuario.foto, rol.roid, rol.Descricion FROM usuario INNER JOIN rol ON rol.roid = usuario.RolId WHERE usuario.UsuarioId = @id; ";
            using (MySqlCommand command = new MySqlCommand(query, connection))

            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario()
                        {
                            UsuarioId = reader.GetInt32("UsuarioId"),
                            Username = reader.GetString("Username"),
                            Contrasenia = reader.GetString("Contrasenia"),
                            RolId = reader.GetInt32("Rolid"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            foto = reader.GetString("foto")
                        };
                        Rol rol = new Rol() { Descricion = reader.GetString("Descricion") };
                        usuario.Rol = rol.Descricion;
                    }
                    return usuario;
                }
            }
        }
    }

    /*  public Usuario GetUsuario(int UsuarioId)
      {
          Usuario usuario = new Usuario();
          using (MySqlConnection connection = new MySqlConnection(connectingString))
          {
              string query =
                  @"SELECT `UsuarioId`,`Username`,`password` as Contrasenia, `Rolid`,`nombre`,`apellido`,`foto`, rol.roid, rol.Descricion as Descricion FROM `usuario` INNER JOIN rol ON usuario.Rolid = rol.roid WHERE UsuarioId = @UsuarioId;";
              using (MySqlCommand command = new MySqlCommand(query, connection))
              {
                  command.Parameters.AddWithValue("@UsuarioId", UsuarioId);
                  connection.Open();
                  using (MySqlDataReader reader = command.ExecuteReader())
                  {
                      while (reader.Read())
                      {
                          usuario = new Usuario()
                          {
                              UsuarioId = reader.GetInt32(nameof(Usuario.UsuarioId)),
                              Username = reader.GetString(nameof(Usuario.Username)),
                              Contrasenia = reader.GetString(nameof(Usuario.Contrasenia)),
                              Nombre = reader.GetString(nameof(Usuario.Nombre)),
                              Apellido = reader.GetString(nameof(Usuario.Apellido)),
                              foto = reader.GetString(nameof(Usuario.foto)),
                              RolId = reader.GetInt32(nameof(Usuario.RolId)),
                          };
                      }
                      return usuario;
                  }
              }
          }
      }*/

    public Usuario GetUsuarioXUsername(string Username)
    {
        Usuario usuario = new Usuario();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
               @"SELECT usuario.UsuarioId, usuario.Username, usuario.password as Contrasenia, usuario.Rolid, usuario.nombre, usuario.apellido, usuario.foto, rol.roid, rol.Descricion FROM usuario INNER JOIN rol ON rol.roid = usuario.RolId WHERE usuario.Username = @Username; ";
              using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", Username);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario()
                        {
                            UsuarioId = reader.GetInt32(nameof(Usuario.UsuarioId)),
                            Username = reader.GetString(nameof(Usuario.Username)),
                            Contrasenia = reader.GetString(nameof(Usuario.Contrasenia)),
                            RolId = reader.GetInt32(nameof(Usuario.RolId)),
                            Nombre = reader.GetString(nameof(Usuario.Nombre)),
                            Apellido = reader.GetString(nameof(Usuario.Apellido)),
                            foto = reader.GetString(nameof(Usuario.foto))
                        };
                        Rol rol = new Rol() { Descricion = reader.GetString("Descricion") };
                        usuario.Rol = rol.Descricion;
                    }
                    return usuario;
                }
            }
        }
    }

    public int Crear(Usuario usuario)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
              @"INSERT INTO usuario (`Username`, `password`, `Rolid`, `nombre`, `apellido`, `foto`) 
							VALUES (@Username, @Contrasenia, @RolId, @Nombre, @Apellido, @RutaFoto); 
							SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", usuario.Username);
                command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                command.Parameters.AddWithValue("@RolId", usuario.RolId);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                if (String.IsNullOrEmpty(usuario.foto))
                    command.Parameters.AddWithValue("@RutaFoto", "");
                else
                    command.Parameters.AddWithValue("@RutaFoto", usuario.foto);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                usuario.UsuarioId = res;

            }
        }
        return res;
    }

    private string GuardarFotoEnServidor(IFormFile foto)
    {
        string rutaFoto = null;
        if (foto != null && foto.Length > 0)
        {
            string rutaCarpetaFotos = "./wwwroot/imagenes";
            string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            rutaFoto = Path.Combine(rutaCarpetaFotos, nombreArchivo);
            using (var stream = new FileStream(rutaFoto, FileMode.Create))
            {
                foto.CopyTo(stream);
            }
        }
        return rutaFoto;
    }

    public int Actualizar(int id, Usuario Usuario)
    {
        int res = 0;

        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"UPDATE usuario SET Username = @Username, `password` = @Contrasenia, Nombre = @Nombre, Apellido = @Apellido, foto = @foto WHERE UsuarioId = @UsuarioId;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UsuarioId", Usuario.UsuarioId);
                command.Parameters.AddWithValue("@Username", Usuario.Username);
                command.Parameters.AddWithValue("@Contrasenia", Usuario.Contrasenia);
                command.Parameters.AddWithValue("@Nombre", Usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", Usuario.Apellido);
                command.Parameters.AddWithValue("@foto", Usuario.foto);
                connection.Open();
                res = command.ExecuteNonQuery();
            }
        }
        return res;
    }

    public void Borrar(int UsuarioId)
    {
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query = @"DELETE FROM usuario WHERE UsuarioId = @UsuarioId;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UsuarioId", UsuarioId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    //esta sera X si en algun momento tengo que cambiar la contrasenia

    public int CambiarContrasenia(Usuario Usuario)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"UPDATE usuario SET `password` as Contrasenia = @Contrasenia WHERE UsuarioId = @UsuarioId;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UsuarioId", Usuario.UsuarioId);
                command.Parameters.AddWithValue("@Contrasenia", Usuario.Contrasenia);
                connection.Open();
                res = command.ExecuteNonQuery();
            }
        }
        return res;
    }

    /* public string GenerarHash(string contrasenia)
     {
         using (SHA256 sha256Hash = SHA256.Create())
         {
             byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contrasenia));
             StringBuilder builder = new StringBuilder();
             for (int i = 0; i < bytes.Length; i++)
             {
                 builder.Append(bytes[i].ToString("x2"));
             }
             return builder.ToString();
         }
         funcion ara generar hashes de contraseñas, la idea era llamarla desde el controlador asique queda de ejemlo nada mas
     }*/
}
