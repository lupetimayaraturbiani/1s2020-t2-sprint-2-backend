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

        public List<FuncionarioDomain> ListarPorNomeCompleto()
        {
            // Cria uma lista funcionarios onde serão armazenados os dados
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdFuncionario, CONCAT (Nome, ' ', Sobrenome), DataNascimento FROM Funcionarios";

                // Outra forma
                //string querySelectAll = "SELECT IdFuncionario, Nome, Sobrenome, DataNascimento FROM Funcionarios";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto funcionario do tipo FuncionarioDomain
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            // Atribui à propriedade IdFuncionario o valor da coluna IdFuncionario da tabela do banco de dados
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"])

                            // Atribui à propriedade Nome os valores das colunas Nome e Sobrenome da tabela do banco de dados
                            ,
                            Nome = rdr[1].ToString()

                            // Outra forma
                            //,Nome = rdr["Nome"].ToString() + ' ' + rdr["Sobrenome"].ToString()

                            // Atribui à propriedade DataNascimento o valor da coluna DataNascimento da tabela do banco de dados
                            ,
                            DataNascimento = Convert.ToDateTime(rdr["DataNascimento"])
                        };

                        // Adiciona o filme criado à lista funcionarios
                        funcionarios.Add(funcionario);
                    }
                }
            }

            // Retorna a lista de funcionarios
            return funcionarios;
        }

        public List<FuncionarioDomain> ListarOrdenado(string ordem)
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdFuncionario, Nome, Sobrenome, DataNascimento " +
                                        $"FROM Funcionarios ORDER BY Nome {ordem}";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto funcionario 
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            // Atribui à propriedade IdFuncionario o valor da coluna "IdFuncionario" da tabela do banco
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"])

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,
                            Nome = rdr["Nome"].ToString()

                            // Atribui à propriedade Sobrenome o valor da coluna "Sobrenome" da tabela do banco
                            ,
                            Sobrenome = rdr["Sobrenome"].ToString()

                            // Atribui à propriedade DataNascimento o valor da coluna "DataNascimento" da tabela do banco
                            ,
                            DataNascimento = Convert.ToDateTime(rdr["DataNascimento"])
                        };

                        // Adiciona o funcionario criado à lista funcionarios
                        funcionarios.Add(funcionario);
                    }
                }
            }

            // Retorna a lista de funcionarios
            return funcionarios;
        }
    }
}
