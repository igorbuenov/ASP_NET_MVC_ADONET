using ASP_NET_MVC_ADONET.Models;
using System.Data;
using System.Data.SqlClient;

namespace ASP_NET_MVC_ADONET.Data
{
    public class DataAccess
    {


        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }


        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString("DefaultConnection");
        }


        public List<Usuario> ListarUsuarios()
        {

            List<Usuario> usuarios = new List<Usuario>();

            using(_connection = new SqlConnection(GetConnectionString()))
            {
                // Criação de comando
                _command = _connection.CreateCommand();

                // Modo que o comando será executado
                _command.CommandType = CommandType.StoredProcedure;

                // Identificação do comando no DATABASE
                _command.CommandText = "[dbo].[pr_ListarUsuarios]";

                // Abrindo conexão com o banco
                _connection.Open();

                // ResultSet
                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Sobrenome = reader["Sobrenome"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Cargo = reader["Cargo"].ToString();

                    usuarios.Add(usuario);

                }

                _connection.Close();
            }

            return usuarios;

        }

        public bool Cadastrar(Usuario usuario)
        {
            // Número de linhas afetadas no DATABASE ao realizar a operação
            int rowscount = 0;

            using(_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[pr_InserirUsuario]";

                // Passando valores informado pelo usuário para os parametros no DATABASE
                _command.Parameters.AddWithValue("@Nome", usuario.Nome);
                _command.Parameters.AddWithValue("@Sobrenome", usuario.Sobrenome);
                _command.Parameters.AddWithValue("@Email", usuario.Email);
                _command.Parameters.AddWithValue("@Cargo", usuario.Cargo);

                _connection.Open();

                rowscount = _command.ExecuteNonQuery();

                _connection.Close();


            }

            return rowscount > 0 ? true : false;


        }

        public Usuario BuscarUsuarioPorId(int id)
        {
            Usuario usuario = new Usuario();

            using(_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[pr_ListarUsuarioPorId]";

                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Sobrenome= reader["Sobrenome"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Cargo = reader["Cargo"].ToString();
                }

                _connection.Close(); 
            }

            return usuario;
        }

        public bool Editar(Usuario usuario)
        {
            // Número de linhas afetadas no DATABASE ao realizar a operação
            int rowscount = 0;

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[pr_EditarUsuario]";

                // Passando valores informado pelo usuário para os parametros no DATABASE
                _command.Parameters.AddWithValue("@Id", usuario.Id);
                _command.Parameters.AddWithValue("@Nome", usuario.Nome);
                _command.Parameters.AddWithValue("@Sobrenome", usuario.Sobrenome);
                _command.Parameters.AddWithValue("@Email", usuario.Email);
                _command.Parameters.AddWithValue("@Cargo", usuario.Cargo);

                _connection.Open();

                rowscount = _command.ExecuteNonQuery();

                _connection.Close();


            }

            return rowscount > 0 ? true : false;
        }

        public bool Remover(int id)
        {
            int rowscount = 0;

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[pr_RemoverUsuario]";

                // Passando valores informado pelo usuário para os parametros no DATABASE
                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();

                rowscount = _command.ExecuteNonQuery();

                _connection.Close();

            }

            return rowscount > 0 ? true : false;
        }




    }
}
