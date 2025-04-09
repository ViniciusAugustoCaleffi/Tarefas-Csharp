using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TelasCSharpp
{
    public partial class Form4 : Form
    {
        public int _id;
        Form1 form;
        public Form4(int id)
        {
            _id = id;
            DAO dao = new DAO();
            InitializeComponent();
            form = new Form1();
            var tarefa = dao.ObterTarefaPorId(id);

            textBox2.Text = tarefa.Titulo;
            textBox1.Text = tarefa.Descricao;
            comboBox1.Text = tarefa.Prioridade;
            maskedTextBoxDtVencimento.Text = tarefa.DtVencimento.ToString();
            checkBox1.Checked = tarefa.Concluida;

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }//titulo

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }//conteudo

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }//prioridade

        private void maskedTextBoxDtVencimento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }//DtVencimento

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }//Concluido

        public void button1_Click(object sender, EventArgs e)
        {
            DAO dao = new DAO();
            
            string descricao = (textBox2.Text);
            string titulo = (textBox1.Text);
            string prioridade = (comboBox1.Text);
            string concluida = (checkBox1.Checked.ToString());

            
            DateTime DtVencimento = DateTime.Now;
            DtVencimento = Convert.ToDateTime(maskedTextBoxDtVencimento.Text);
            string sqlFormattedDate = DtVencimento.ToString("yyyy-MM-dd HH:mm:ss.fff");

            dao.Atualizar(_id, "descricao", descricao);
            dao.Atualizar(_id, "titulo", titulo);
            dao.Atualizar(_id, "prioridade", prioridade);
            dao.Atualizar(_id, "dtVencimento", sqlFormattedDate);
            dao.Atualizar(_id, "concluida", concluida);

            

            


            MessageBox.Show("Dados Atualizados com sucesso!");




            Close();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
