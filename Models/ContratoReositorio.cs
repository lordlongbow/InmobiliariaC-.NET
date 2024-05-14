using MySql.Data.MySqlClient;

namespace royectoInmobiliaria.net_MVC_.Models;

public class ContratoReositorio
{
    string connectingString = "server=localhost; user=root; Password=; database=inmobiliaria_cs;";

    private InmuebleRepositorio InmuebleRepositorio = new InmuebleRepositorio();

    public ContratoReositorio() { }

    public List<Contrato> GetContratos()
    {
        List<Contrato> contratos = new List<Contrato>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"SELECT c.id_contrato as ContratoId, c.fechaInicio, c.fechaFinalizacion, c.id_inmueble as InmuebleId, c.id_inquilino as InquilinoId, 
                i.id_inmueble, i.direccion as Direccion, 
                iq.id, iq.nombre, iq.apellido              
                 FROM contrato c
                 INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble
                 INNER JOIN inquilino iq ON c.id_inquilino = iq.id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contrato contrato = new Contrato();
                        contrato.ContratoId = reader.GetInt32(nameof(Contrato.ContratoId));
                        contrato.FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio));
                        contrato.FechaFinalizacion = reader.GetDateTime(
                            nameof(Contrato.FechaFinalizacion)
                        );
                        contrato.InmuebleId = reader.GetInt32(nameof(Contrato.InmuebleId));
                        contrato.InquilinoId = reader.GetInt32(nameof(Contrato.InquilinoId));

                        contrato.Inmueble = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion))
                        };
                        contrato.Inquilino = new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido))
                        };

                        contratos.Add(contrato);
                    }
                }
            }
        }
        return contratos;
    }

    public Contrato GetContrato(int id)
    {
        Contrato contrato = new Contrato();

        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"SELECT c.id_contrato as ContratoId, c.fechaInicio, c.fechaFinalizacion, c.id_inmueble as InmuebleId, c.id_inquilino as InquilinoId, 
                i.id_inmueble, i.direccion as Direccion, 
                iq.id, iq.nombre, iq.apellido               
                 FROM contrato c
                 INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble
                 INNER JOIN inquilino iq ON c.id_inquilino = iq.id
                 WHERE c.id_contrato = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contrato.ContratoId = reader.GetInt32(nameof(Contrato.ContratoId));
                        contrato.FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio));
                        contrato.FechaFinalizacion = reader.GetDateTime(
                            nameof(Contrato.FechaFinalizacion)
                        );
                        contrato.InmuebleId = reader.GetInt32(nameof(Contrato.InmuebleId));
                        contrato.InquilinoId = reader.GetInt32(nameof(Contrato.InquilinoId));

                        contrato.Inmueble = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion))
                        };
                        contrato.Inquilino = new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido))
                        };
                    }
                }
            }
        }
        return contrato;
    }

    public int Crear(Contrato contrato)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"INSERT INTO contrato (fechaInicio, fechaFinalizacion,  id_inmueble, id_inquilino)
             VALUES (@fechaInicio, @fechaFinalizacion,  @id_inmueble, @id_inquilino);
             SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                command.Parameters.AddWithValue("@fechaFinalizacion", contrato.FechaFinalizacion);

                command.Parameters.AddWithValue("@id_inmueble", contrato.InmuebleId);
                command.Parameters.AddWithValue("@id_inquilino", contrato.InquilinoId);

                try
                {
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());

                    if (res > 0)
                    {
                        Inmueble i = InmuebleRepositorio.getInmueble(contrato.InmuebleId);
                        i.Libre = false;
                        InmuebleRepositorio.Modificar(i);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return res;
        }
    }

    public int Modificar(Contrato contrato)
    {
        int res = 0;
        int id = contrato.ContratoId;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query =
                @"UPDATE contrato SET fechaInicio = @fechaInicio, fechaFinalizacion = @fechaFinalizacion,
                 id_inmueble = @id_inmueble, id_inquilino = @id_inquilino
                WHERE contrato.id_contrato = @id;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                command.Parameters.AddWithValue("@fechaFinalizacion", contrato.FechaFinalizacion);

                command.Parameters.AddWithValue("@id_inmueble", contrato.InmuebleId);
                command.Parameters.AddWithValue("@id_inquilino", contrato.InquilinoId);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return res;
        }
    }

    public int Borrar(int id)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            string query = @"DELETE FROM contrato WHERE contrato.id_contrato = @id;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return res;
        }
    }

    public List<Contrato> ContratosVigentes()
    {
        List<Contrato> contratosVigentes = new List<Contrato>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            var query =
                @"SELECT c.id_contrato as ContratoId, c.fechaInicio, c.fechaFinalizacion, c.id_inmueble as InmuebleId, c.id_inquilino as InquilinoId, i.id_inmueble, i.direccion as Direccion, iq.id, iq.nombre AS InquilinoNombre, iq.apellido AS Inquilinoapellido, p.nombre AS NombrePropietario, p.apellido AS ApellidoPropietario FROM contrato c INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble INNER JOIN inquilino iq ON c.id_inquilino = iq.id INNER JOIN propietario p ON i.id_propietario = p.id WHERE c.fechaFinalizacion >= CURDATE();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contrato contrato = new Contrato();
                        contrato.ContratoId = reader.GetInt32(nameof(Contrato.ContratoId));
                        contrato.InmuebleId = reader.GetInt32(nameof(Contrato.InmuebleId));
                        contrato.InquilinoId = reader.GetInt32(nameof(Contrato.InquilinoId));
                        contrato.FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio));
                        contrato.FechaFinalizacion = reader.GetDateTime(
                            nameof(Contrato.FechaFinalizacion)
                        );
                        contrato.Inmueble = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32(nameof(Propietario.Id)),
                                Nombre = reader.GetString("NombrePropietario"),
                                Apellido = reader.GetString("ApellidoPropietario")
                            }
                        };
                        contrato.Inquilino = new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString("InquilinoNombre"),
                            Apellido = reader.GetString("Inquilinoapellido")
                        };
                        contratosVigentes.Add(contrato);
                    }
                }
            }
        }
        return contratosVigentes;
    }

    public List<Contrato> ContratosPorInmueble(int id)
    {
        List<Contrato> ContratosDelInmueble = new List<Contrato>();
        using (MySqlConnection connection = new MySqlConnection(connectingString))
        {
            var query =
                @"SELECT c.id_contrato as ContratoId, c.fechaInicio, c.fechaFinalizacion, c.id_inmueble as InmuebleId, c.id_inquilino as InquilinoId, i.id_inmueble, i.direccion as Direccion, iq.id, iq.nombre AS InquilinoNombre, iq.apellido AS Inquilinoapellido, p.nombre AS NombrePropietario, p.apellido AS ApellidoPropietario FROM contrato c INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble INNER JOIN inquilino iq ON c.id_inquilino = iq.id INNER JOIN propietario p ON i.id_propietario = p.id WHERE i.id_inmueble = @id;";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contrato contrato = new Contrato();
                        contrato.ContratoId = reader.GetInt32(nameof(Contrato.ContratoId));
                        contrato.InmuebleId = reader.GetInt32(nameof(Contrato.InmuebleId));
                        contrato.InquilinoId = reader.GetInt32(nameof(Contrato.InquilinoId));
                        contrato.FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio));
                        contrato.FechaFinalizacion = reader.GetDateTime(
                            nameof(Contrato.FechaFinalizacion)
                        );
                        contrato.Inmueble = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32(nameof(Propietario.Id)),
                                Nombre = reader.GetString("NombrePropietario"),
                                Apellido = reader.GetString("ApellidoPropietario")
                            }
                        };
                        contrato.Inquilino = new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString("InquilinoNombre"),
                            Apellido = reader.GetString("Inquilinoapellido")
                        };
                        ContratosDelInmueble.Add(contrato);
                    }
                }
            }
        }
        return ContratosDelInmueble;
    }

    public Contrato Renovar(Contrato contrato)
    {
        int idContrato = contrato.ContratoId;
        DateTime hoy = DateTime.Now;
        Contrato contratoRenovable = GetContrato(idContrato);

        if (
            contratoRenovable.FechaFinalizacion < hoy
            && contrato.InquilinoId == contratoRenovable.InquilinoId
        )
        {
            contratoRenovable.FechaFinalizacion = contrato.FechaFinalizacion;
            Modificar(contratoRenovable);
            return contratoRenovable;
        }
        else
        {
            return null;
        }
    }
}
