using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TelasCSharpp
{
    class DAO
    {
        public int i = 0;
        public int contador = 0;


        public MySqlConnection conexao;

        public DAO() //constructor
        {
            conexao = new MySqlConnection("server=localhost;Database=tarefasCSharp;Uid=root;password=");
            try
            {
                conexao.Open();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Algo deu errado!!\n\n\n" + erro);
            }



        }//fim constructor

        public List<Tarefa> PreencherVetor()
        {

            string query = "select * from tarefa";

            //prepara o comando para o banco
            var sql = new MySqlCommand(query, conexao);
            //chamar o leitor do BD

            var tarefas = new List<Tarefa>();
            tarefas.Clear(); // Limpa a lista antes de carregar

            using (var leitura = sql.ExecuteReader())
            {


                while (leitura.Read())
                {
                    var tarefa = new Tarefa
                    {
                        Id = leitura.GetInt32("id"),
                        Titulo = leitura.GetString("titulo"),
                        Descricao = leitura.GetString("descricao"),
                        Prioridade = leitura.GetString("prioridade"),
                        DtVencimento = leitura.GetDateTime("dtVencimento"),
                        Concluida = leitura.GetBoolean("concluida"),
                    };
                    tarefas.Add(tarefa);

                }//fim while
                leitura.Close();



                tarefas = tarefas
                          .OrderBy(p => p.DtVencimento)
                          .ThenBy(p =>
                          {
                              var prioridade = p.Prioridade?.Trim().ToLowerInvariant();
                              if (prioridade == "alta")
                                  return 1;
                              if (prioridade == "media")
                                  return 2;
                              if (prioridade == "baixa")
                                  return 3;
                              return 99;
                          })
                          .ToList();

                return (tarefas);
            }
        }

        


        public string Inserir(string Titulo, string Descricao, string Prioridade, string DtVencimento)
        {
            string inserir = $"insert into tarefa(id,titulo,descricao,prioridade,dtVencimento,concluida)value('','{Titulo}','{Descricao}','{Prioridade}','{DtVencimento}','')";
            MySqlCommand sql = new MySqlCommand(inserir, conexao);
            string resultado = sql.ExecuteNonQuery() + "Executados";
            
            return resultado;
        }//fim inserir

        public string Atualizar(int id, string campo, string dado)
        {
            string query = "";
            if (dado == "True")
            {
                query = $"update tarefa set {campo} = {dado} where id = '{id}'";
            }
            else
            {
                query = $"update tarefa set {campo} = '{dado}' where id = '{id}'";
            }
            MySqlCommand sql = new MySqlCommand(query, conexao);
            String resultado = sql.ExecuteNonQuery() + " Atualizados!";
            return resultado;
        }//fim mt atualizar

        public Tarefa ObterTarefaPorId(int id)
        {
            var query = $"SELECT * FROM Tarefa WHERE Id = {id}";
            var sql = new MySqlCommand(query, conexao);

            var tarefa = new Tarefa();

            using (var resultado = sql.ExecuteReader())
            {
                while (resultado.Read())
                {
                    tarefa = new Tarefa()
                    {
                        Id = resultado.GetInt32("Id"),
                        Titulo = resultado.GetString("Titulo"),
                        Descricao = resultado.GetString("Descricao"),
                        Prioridade = resultado.GetString("Prioridade"),
                        Concluida = resultado.GetBoolean("concluida"),
                        DtVencimento = resultado.GetDateTime("DtVencimento"),
                    };
                }
            }

            return tarefa;
        }
        

        public void ExcluirTarefa(int id)
        { /* Implementação */

            var query = $"DELETE FROM Tarefa WHERE Id = {id}";
            var sql = new MySqlCommand(query, conexao);
            sql.ExecuteNonQuery();


        }

        public List<Tarefa> BuscarPorTitulo(string titulo)
        {
            List<Tarefa> tarefas = new List<Tarefa>();

            try
            {

                string sql = "SELECT * FROM Tarefa WHERE Titulo LIKE @titulo";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@titulo", $"%{titulo}%");

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tarefa tarefa = new Tarefa
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Titulo = reader["Titulo"].ToString(),
                        Descricao = reader["Descricao"].ToString(),
                        DtVencimento = Convert.ToDateTime(reader["dtVencimento"]),
                        Prioridade = reader["Prioridade"].ToString(),
                        Concluida = Convert.ToBoolean(reader["Concluida"])
                    };

                    tarefas.Add(tarefa);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
            }

            return tarefas;
        }


        public List<Tarefa> FiltrarTarefas(string status, string prioridade, DateTime? data)
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            

                var query = new StringBuilder("SELECT * FROM tarefa WHERE 1=1");
            
                if (status != "Todos")
                {
                    if (status == "concluidas")
                    {
                        query.Append(" AND Concluida = 'TRUE'");
                    }else
                    {
                        query.Append(" AND Concluida = 'FALSE'");
                    }
                }

                if (prioridade != "Todas")
                    query.Append(" AND Prioridade = @prioridade");

                if (data.HasValue)
                    query.Append(" AND DtVencimento = @data");
                using (var comando = new MySqlCommand(query.ToString(), conexao))
                {
                    if (prioridade != "Todas")
                        comando.Parameters.AddWithValue("@prioridade", prioridade);

                    if (data.HasValue)
                        comando.Parameters.AddWithValue("@data", data.Value.Date);

                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tarefas.Add(new Tarefa
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Titulo = reader["Titulo"].ToString(),
                                Descricao = reader["Descricao"].ToString(),
                                Prioridade = reader["Prioridade"].ToString(),
                                DtVencimento = Convert.ToDateTime(reader["DtVencimento"]),
                                Concluida = Convert.ToBoolean(reader["Concluida"])
                            });
                        }
                    }
                }
            
            return tarefas;
        }




    }//fim Class DAO
}
