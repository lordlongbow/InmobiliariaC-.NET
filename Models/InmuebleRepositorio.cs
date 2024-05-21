using MySql.Data.MySqlClient;

namespace royectoInmobiliaria.net_MVC_.Models;

public class InmuebleRepositorio
{
    string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";

 
    public InmuebleRepositorio() { }

    public List<Inmueble> GetInmuebles()
    {
        List<Inmueble> inmuebles = new List<Inmueble>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"SELECT inmueble.id_inmueble as InmuebleId, inmueble.direccion, inmueble.precio, inmueble.cantAmbientes, inmueble.latitud, inmueble.longitud, inmueble.libre, propietario.nombre, propietario.apellido, tipos.descripcion,usos.descripcion as DescripcionUso FROM `inmueble`
            INNER JOIN propietario on inmueble.id_propietario = propietario.id 
            INNER JOIN tipos on inmueble.id_tipo = tipos.id_tipo 
            INNER JOIN usos on inmueble.id_uso = usos.id_uso; ";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmueble inmueble = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(nameof(inmueble.InmuebleId)),
                            Direccion = reader.GetString(nameof(inmueble.Direccion)),
                            Precio = reader.GetDecimal(nameof(inmueble.Precio)),
                            CantAmbientes = reader.GetInt32(nameof(inmueble.CantAmbientes)),
                            Latitud = reader.GetInt32(nameof(inmueble.Latitud)),
                            Longitud = reader.GetInt32(nameof(inmueble.Longitud)),
                            Libre = reader.GetBoolean(nameof(inmueble.Libre)),
                            Propietario = new Propietario
                            {
                                Nombre = reader.GetString(nameof(inmueble.Propietario.Nombre)),
                                Apellido = reader.GetString(nameof(inmueble.Propietario.Apellido)),
                            },
                            Tipo = new Tipo
                            {
                                Descripcion = reader.GetString(nameof(inmueble.Tipo.Descripcion))
                            },
                            Uso = new Uso
                            {
                                DescripcionUso = reader.GetString(
                                    nameof(inmueble.Uso.DescripcionUso)
                                )
                            }
                        };
                        inmuebles.Add(inmueble);
                    }
                    return inmuebles;
                }
            }
        }
    }

    public Inmueble getInmueble(int id)
    {
        Inmueble inmueble = new Inmueble();

        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"SELECT inmueble.id_inmueble as InmuebleId, inmueble.direccion, inmueble.precio, inmueble.cantAmbientes, inmueble.latitud, inmueble.longitud, inmueble.libre, propietario.id as PropietarioId, propietario.nombre, propietario.apellido, tipos.id_tipo as TipoId, tipos.descripcion, usos.id_uso as UsoId, usos.descripcion as DescripcionUso FROM inmueble
            INNER JOIN propietario on inmueble.id_propietario = propietario.id 
            INNER JOIN tipos on inmueble.id_tipo = tipos.id_tipo 
            INNER JOIN usos on inmueble.id_uso = usos.id_uso WHERE inmueble.id_inmueble = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inmueble.InmuebleId = reader.GetInt32("InmuebleId");
                        inmueble.Direccion = reader.GetString("direccion");
                        inmueble.Precio = reader.GetDecimal("precio");
                        inmueble.CantAmbientes = reader.GetInt32("cantAmbientes");
                        inmueble.Latitud = reader.GetInt32("latitud");
                        inmueble.Longitud = reader.GetInt32("longitud");
                        inmueble.PropietarioId = reader.GetInt32("PropietarioId");
                        inmueble.Propietario = new Propietario
                        {
                            Id = reader.GetInt32("PropietarioId"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                        };
                        inmueble.TipoId = reader.GetInt32("TipoId");
                        inmueble.Tipo = new Tipo
                        {
                            TipoId = reader.GetInt32("TipoId"),
                            Descripcion = reader.GetString("descripcion")
                        };
                        inmueble.UsoId = reader.GetInt32("UsoId");
                        inmueble.Uso = new Uso
                        {
                            UsoId = reader.GetInt32("UsoId"),
                            DescripcionUso = reader.GetString("DescripcionUso")
                        };
                        inmueble.Libre = reader.GetBoolean("libre");
                    }
                    else
                    {
                        inmueble = null;
                    }

                    connection.Close();
                    return inmueble;
                }
            }
        }
    }

    public int Crear(Inmueble inmueble)
    {
        int res = 0;

        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"INSERT INTO inmueble (direccion, precio, cantAmbientes, latitud, longitud, id_propietario, id_tipo, id_uso, libre)
        VALUES (@direccion, @precio, @cantAmbientes, @latitud, @longitud, @id_propietario, @id_tipo, @id_uso, @libre); SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@precio", inmueble.Precio);
                command.Parameters.AddWithValue("@cantAmbientes", inmueble.CantAmbientes);
                command.Parameters.AddWithValue("@latitud", inmueble.Latitud);
                command.Parameters.AddWithValue("@longitud", inmueble.Longitud);
                command.Parameters.AddWithValue("@id_propietario", inmueble.PropietarioId);
                command.Parameters.AddWithValue("@id_tipo", inmueble.TipoId);
                command.Parameters.AddWithValue("@id_uso", inmueble.UsoId);
                command.Parameters.AddWithValue("@libre", inmueble.Libre);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }

        return res;
    }

    public int Modificar(Inmueble inmueble)
    {
        int res = 0;
        int id = inmueble.InmuebleId;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"UPDATE inmueble SET direccion = @direccion, precio = @precio, cantAmbientes = @cantAmbientes, 
                latitud = @latitud, longitud = @longitud, id_propietario = @id_propietario, id_tipo = @id_tipo, 
                id_uso = @id_uso, libre = @libre WHERE id_inmueble = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@precio", inmueble.Precio);
                command.Parameters.AddWithValue("@cantAmbientes", inmueble.CantAmbientes);
                command.Parameters.AddWithValue("@latitud", inmueble.Latitud);
                command.Parameters.AddWithValue("@longitud", inmueble.Longitud);
                command.Parameters.AddWithValue("@id_propietario", inmueble.PropietarioId);
                command.Parameters.AddWithValue("@id_tipo", inmueble.TipoId);
                command.Parameters.AddWithValue("@id_uso", inmueble.UsoId);
                command.Parameters.AddWithValue("@libre", inmueble.Libre);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

    public void Borrar(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query = @"DELETE FROM inmueble WHERE id_inmueble = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public List<Inmueble> verLibres()
    {
        List<Inmueble> inmuebles = new List<Inmueble>();

        string query =
            @"SELECT inmueble.id_inmueble,`direccion`, `precio`, `cantAmbientes`, `latitud`, `longitud`, `libre`, propietario.nombre, propietario.apellido, tipos.descripcion as descripcion, usos.descripcion as DescripcionUso FROM `inmueble` JOIN propietario JOIN tipos JOIN usos WHERE libre = 1 AND inmueble.id_propietario = propietario.id AND tipos.id_tipo = inmueble.id_tipo AND inmueble.id_uso = usos.id_uso;";
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmueble inmueble = new Inmueble();

                        inmueble.InmuebleId = reader.GetInt32("id_inmueble");
                        inmueble.Direccion = reader.GetString("direccion");
                        inmueble.Precio = reader.GetDecimal("precio");
                        inmueble.CantAmbientes = reader.GetInt32("cantAmbientes");
                        inmueble.Latitud = reader.GetInt32("latitud");
                        inmueble.Longitud = reader.GetInt32("longitud");
                        inmueble.Libre = reader.GetBoolean("libre");
                        inmueble.Propietario = new Propietario
                        {
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                        };
                        inmueble.Tipo = new Tipo { Descripcion = reader.GetString("descripcion") };
                        inmueble.Uso = new Uso
                        {
                            DescripcionUso = reader.GetString("DescripcionUso")
                        };
                        inmuebles.Add(inmueble);
                    }
                }
            }
            return inmuebles;
        }
    }

    public List<Inmueble> verNoLibres()
    {
        List<Inmueble> inmuebles = new List<Inmueble>();

        string query = @"SELECT * FROM inmueble WHERE libre = 0";

        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmueble inmueble = new Inmueble();
                        inmueble.InmuebleId = reader.GetInt32("id_inmueble");
                        inmueble.Direccion = reader.GetString("direccion");
                        inmueble.Precio = reader.GetDecimal("precio");
                        inmueble.CantAmbientes = reader.GetInt32("cantAmbientes");
                        inmueble.Latitud = reader.GetInt32("latitud");
                        inmueble.Longitud = reader.GetInt32("longitud");
                        inmueble.Libre = reader.GetBoolean("libre");
                        inmueble.PropietarioId = reader.GetInt32("id_propietario");
                        inmueble.TipoId = reader.GetInt32("id_tipo");
                        inmueble.UsoId = reader.GetInt32("id_uso");
                        inmuebles.Add(inmueble);
                    }
                }
            }
            return inmuebles;
        }
    }

    public Inmueble GetInmuebleXDireccion(string direccion)
    {
        Inmueble inmueble = new Inmueble();

        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"SELECT inmueble.id_inmueble as InmuebleId, inmueble.direccion, inmueble.precio, inmueble.cantAmbientes, inmueble.latitud, inmueble.longitud, inmueble.libre, propietario.id as PropietarioId, propietario.nombre, propietario.apellido, tipos.id_tipo as TipoId, tipos.descripcion, usos.id_uso as UsoId, usos.descripcion as DescripcionUso FROM inmueble
            INNER JOIN propietario on inmueble.id_propietario = propietario.id 
            INNER JOIN tipos on inmueble.id_tipo = tipos.id_tipo 
            INNER JOIN usos on inmueble.id_uso = usos.id_uso WHERE inmueble.direccion = @direccion";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@direccion", direccion);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inmueble.InmuebleId = reader.GetInt32("InmuebleId");
                        inmueble.Direccion = reader.GetString("direccion");
                        inmueble.Precio = reader.GetDecimal("precio");
                        inmueble.CantAmbientes = reader.GetInt32("cantAmbientes");
                        inmueble.Latitud = reader.GetInt32("latitud");
                        inmueble.Longitud = reader.GetInt32("longitud");
                        inmueble.PropietarioId = reader.GetInt32("PropietarioId");
                        inmueble.Propietario = new Propietario
                        {
                            Id = reader.GetInt32("PropietarioId"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                        };
                        inmueble.TipoId = reader.GetInt32("TipoId");
                        inmueble.Tipo = new Tipo
                        {
                            TipoId = reader.GetInt32("TipoId"),
                            Descripcion = reader.GetString("descripcion")
                        };
                        inmueble.UsoId = reader.GetInt32("UsoId");
                        inmueble.Uso = new Uso
                        {
                            UsoId = reader.GetInt32("UsoId"),
                            DescripcionUso = reader.GetString("DescripcionUso")
                        };
                        inmueble.Libre = reader.GetBoolean("libre");
                    }
                    else
                    {
                        inmueble = null;
                    }

                    connection.Close();
                    return inmueble;
                }
            }
        }
    }

    public List<Inmueble> getInmueblesPorPropietario(int id)
    {
        List<Inmueble> InmueblesDelPropetario = new List<Inmueble>();

        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            var query =
                @"SELECT DISTINCT inmueble.id_inmueble, inmueble.direccion, 
       inmueble.cantAmbientes, 
       inmueble.libre,
       inmueble.precio,
       inmueble.latitud,
       inmueble.longitud,
       usos.descripcion AS DescripcionUso, 
       tipos.descripcion AS descripcion 
FROM inmueble 
JOIN propietario ON inmueble.id_propietario = propietario.id 
JOIN usos ON inmueble.id_uso = usos.id_uso 
JOIN tipos ON inmueble.id_tipo = tipos.id_tipo 
WHERE inmueble.id_propietario = @id;
";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmueble inmueble = new Inmueble();
                        inmueble.InmuebleId = reader.GetInt32("id_inmueble");
                        inmueble.Direccion = reader.GetString("direccion");
                        inmueble.Precio = reader.GetDecimal("precio");
                        inmueble.Latitud = reader.GetInt32("latitud");
                        inmueble.Longitud = reader.GetInt32("longitud");
                        inmueble.CantAmbientes = reader.GetInt32("cantAmbientes");
                        inmueble.Libre = reader.GetBoolean("libre");
                        inmueble.Uso = new Uso
                        {
                            DescripcionUso = reader.GetString("DescripcionUso")
                        };
                        inmueble.Tipo = new Tipo { Descripcion = reader.GetString("descripcion") };
                        InmueblesDelPropetario.Add(inmueble);
                    }
                }
            }

            return InmueblesDelPropetario;
        }
    }


    public List<Inmueble> ObtenerInmueblesLibres(DateTime? Desde, DateTime? Hasta)
{
    List<Inmueble> inmueblesLibres = new List<Inmueble>();

    string query = @"
        SELECT i.id_inmueble, i.direccion, i.cantAmbientes, i.precio, p.nombre, p.apellido
        FROM inmueble i
        INNER JOIN propietario p ON i.id_propietario = p.id
        WHERE i.id_inmueble NOT IN (
            SELECT c.id_inmueble
            FROM contrato c
            WHERE @Desde < c.fechaFinalizacion
            AND @Hasta > c.fechaInicio
        )
    ";

    using (MySqlConnection connection = new MySqlConnection(connectingString))
    {
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Desde", Desde);
            command.Parameters.AddWithValue("@Hasta", Hasta);

            connection.Open();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Inmueble inmueble = new Inmueble();
                    inmueble.InmuebleId = reader.GetInt32("id_inmueble");
                    inmueble.Direccion = reader.GetString("direccion");
                    inmueble.CantAmbientes = reader.GetInt32("cantAmbientes");
                    inmueble.Precio = reader.GetDecimal("precio");

                    inmueble.Propietario = new Propietario
                    {
                        Nombre = reader.GetString("nombre"),
                        Apellido = reader.GetString("apellido")
                    };

                    inmueblesLibres.Add(inmueble);
                }
            }
        }
    }

    return inmueblesLibres;
}


}
