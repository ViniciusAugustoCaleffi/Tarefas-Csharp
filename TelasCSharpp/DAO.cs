using System;
using System.Collections.Generic;
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

                var prioridadeOrdem = new Dictionary<string, int>
                    {
                        { "Alta", 1 },
                        { "Media", 2 },
                        { "Baixa", 3 }
                    };

                tarefas = tarefas
                    .OrderBy(p => p.DtVencimento)
                    .ThenBy(p => prioridadeOrdem.ContainsKey(p.Prioridade) ? prioridadeOrdem[p.Prioridade] : 999)
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
            string query = $"update tarefa set {campo} = '{dado}' where id = '{id}'";
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


    }//fim Class DAO
}
