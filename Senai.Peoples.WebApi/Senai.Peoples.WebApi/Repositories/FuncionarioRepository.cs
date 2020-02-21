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

        public void Cadastrar(FuncionarioDomain funcionario)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "INSERT INTO Funcionarios (Nome, Sobrenome)VALUES ('Joana', 'Morais')";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios";

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
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        funcionarios.Add(funcionario);

                    }
                }

            }

            return funcionarios;
        }


    }
}
