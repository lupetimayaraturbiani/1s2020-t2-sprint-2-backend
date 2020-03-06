using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private string stringConexao = "Data Source=DEV601\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132";

        public void AtualizarTipoUsuario(TipoUsuarioDomain tipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "UPDATE TipoUsuario SET Titulo = @Titulo WHERE IdTipoUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", tipoUsuario.IdTipoUsuario);

                    cmd.Parameters.AddWithValue("@Titulo", tipoUsuario.Titulo);

                    

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public TipoUsuarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "SELECT IdTipoUsuario, Titulo FROM TipoUsuario WHERE IdTipoUsuario = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        TipoUsuarioDomain tipoUsuario = new TipoUsuarioDomain
                        {
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),

                            Titulo = rdr["Titulo"].ToString(),

                        };

                        return tipoUsuario;
                    }

                    return null;
                }
            }
        }

        public void CadastrarTipoUsuario(TipoUsuarioDomain tipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "INSERT INTO TipoUsuario (Titulo)VALUES (@Titulo)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Titulo", tipoUsuario.Titulo);
                
                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void DeletarTipoUsuario(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "DELETE FROM TipoUsuario WHERE IdTipoUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TipoUsuarioDomain> ListarTipoUsuario()
        {
            List<TipoUsuarioDomain> tiposUsuario = new List<TipoUsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "SELECT IdTipoUsuario, Titulo FROM TipoUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TipoUsuarioDomain tipoUsuario = new TipoUsuarioDomain
                        {
                            IdTipoUsuario = Convert.ToInt32((rdr[0])),
                            Titulo = rdr["Titulo"].ToString()
                        };

                        tiposUsuario.Add(tipoUsuario);

                    }
                }

            }

            return tiposUsuario;
        }
    }
}
