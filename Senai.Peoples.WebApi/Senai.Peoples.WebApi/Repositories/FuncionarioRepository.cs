using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository: IFuncionarioRepository
    {
        private string StringConexao = "Data Source=DEV601\\SQLEXPRESS;initial catalog=T_Peoples;user id=sa;pwd=sa@132;";

        public void Atualizar(FuncionarioDomain funcionario)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome, DataNascimento = @DataNascimento WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", funcionario.IdFuncionario);

                    cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);

                    cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);

                    cmd.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FuncionarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT IdFuncionario, Nome, Sobrenome, DataNascimento FROM Funcionarios WHERE IdFuncionario = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"]),

                            Nome = rdr["Nome"].ToString(),

                            Sobrenome = rdr["Sobrenome"].ToString(),

                            DataNascimento = Convert.ToDateTime(rdr["DataNascimento"])

                        };

                        return funcionario;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(FuncionarioDomain funcionario)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "INSERT INTO Funcionarios (Nome, Sobrenome, DataNascimento)VALUES (@Nome, @Sobrenome, @DataNascimento)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);
                cmd.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "DELETE FROM Funcionarios WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT IdFuncionario, Nome, Sobrenome, DataNascimento FROM Funcionarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32((rdr[0])),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            DataNascimento = Convert.ToDateTime(rdr["DataNascimento"])
                        };

                        funcionarios.Add(funcionario);

                    }
                }

            }

            return funcionarios;
        }

        public FuncionarioDomain BuscarPorNome(string Nome)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = $"SELECT IdFuncionario, Nome, Sobrenome, DataNascimento FROM Funcionarios WHERE Nome  LIKE '%{Nome}%'";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", Nome);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"]),

                            Nome = rdr["Nome"].ToString(),

                            Sobrenome = rdr["Sobrenome"].ToString(),

                            DataNascimento = Convert.ToDateTime(rdr["DataNascimento"])

                        };

                        return funcionario;
                    }

                    return null;
                }
            }
        }


    }
}
