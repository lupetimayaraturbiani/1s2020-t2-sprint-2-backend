using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = "Data Source=DEV601\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132";

        public List<UsuarioDomain> Listar()
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "SELECT IdUsuario, Email, Senha, IdTipoUsuario FROM Usuarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32((rdr[0])),
                            Email = rdr["Nome"].ToString(),
                            Senha = rdr["Sobrenome"].ToString(),
                            IdTipoUsuario = Convert.ToInt32(rdr[3])
                        };

                        usuarios.Add(usuario);

                    }
                }

            }

            return usuarios;
        }



        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "SELECT IdUsuario, Email, Senha, IdTipoUsuario FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        UsuarioDomain usuario = new UsuarioDomain();

                        while (rdr.Read())
                        {
                            usuario.IdUsuario = Convert.ToInt32(rdr["IdUsuario"]);

                            usuario.Email = rdr["Email"].ToString();

                            usuario.Senha = rdr["Senha"].ToString();

                            usuario.IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]);
                        }

                        return usuario;
                    }
                }

                return null;
            }
        }

        public void Cadastrar(UsuarioDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "INSERT INTO Usuarios (Email, Senha, IdTipoUsuario)VALUES (@Email, @Senha, @IdTipoUsuario)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@IdTipoUsuario", usuario.IdTipoUsuario);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(UsuarioDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "UPDATE Usuarios SET Email = @Email, Senha = @Senha, IdTipoUsuario = @IdTipoUsuario WHERE IdUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", usuario.IdUsuario);

                    cmd.Parameters.AddWithValue("@Email", usuario.Email);

                    cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

                    cmd.Parameters.AddWithValue("@IdTipoUsuario", usuario.IdTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "SELECT IdUsuario, Email, Senha, IdTipoUsuario FROM Usuarios WHERE IdUsuario = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),

                            Email = rdr["Email"].ToString(),

                            Senha = rdr["Senha"].ToString(),

                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])

                        };

                        return usuario;
                    }

                    return null;
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "DELETE FROM Usuario WHERE IdUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
