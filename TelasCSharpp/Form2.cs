using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelasCSharpp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load; // Adiciona o evento de carregamento
        }

        private void label1_Click(object sender, EventArgs e)
        {

        } // Texto "Adicionar Tarefa"

        private void label1_Click_1(object sender, EventArgs e)
        {

        } // A

        private void label7_Click(object sender, EventArgs e)
        {

        } // Seletor de prioridade

        private void label8_Click(object sender, EventArgs e)
        {

        } // Data Vencimento "Texto"

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        } // Título Tarefa

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        } // Conteúdo

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        } // Seletor Prioridade

        private void maskedTextBoxDtVencimento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }// Data vencimento

        private void button1_Click(object sender, EventArgs e)
        {
            DAO inserir = new DAO();

            string Titulo = textBox2.Text;
            string Descricao = textBox1.Text;
            string Prioridade = comboBox1.Text;

            DateTime DtVencimento = DateTime.Now;
            DtVencimento = Convert.ToDateTime(maskedTextBoxDtVencimento.Text);
            string sqlFormattedDate = DtVencimento.ToString("yyyy-MM-dd HH:mm:ss.fff");


            MessageBox.Show(inserir.Inserir(Titulo, Descricao, Prioridade, sqlFormattedDate));
            this.Close();
        } // Botão Adicionar Tarefa

        private void button2_Click(object sender, EventArgs e)
        {

        } // Voltar da página "Adicionar Tarefa"

        // Arredondamento do botão
        private void ArredondarBotao(Button botao, int raio)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, raio, raio, 180, 90);
            path.AddArc(botao.Width - raio, 0, raio, raio, 270, 90);
            path.AddArc(botao.Width - raio, botao.Height - raio, raio, raio, 0, 90);
            path.AddArc(0, botao.Height - raio, raio, raio, 90, 90);
            path.CloseFigure();

            botao.Region = new Region(path);
        }

        // Estilização moderna
        private void PersonalizarBotao(Button botao)
        {
            int raio = 15;

            ArredondarBotao(botao, raio);

            botao.BackColor = Color.FromArgb(0, 128, 0); //Verde 
            botao.ForeColor = Color.Black;
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
            botao.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PersonalizarBotao(button1); // Botão Adicionar Tarefa
            PersonalizarBotao(button2); // Botão Voltar
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
    }
}
