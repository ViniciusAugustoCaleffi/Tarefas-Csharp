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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.Load += Form3_Load; // Adiciona o evento de carregamento
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        } // Título Tarefa da página "Excluir tarefa"

        private void button1_Click(object sender, EventArgs e)
        {

        } // Botão Excluir

        private void button2_Click(object sender, EventArgs e)
        {

        } // Botão Voltar da página "Excluir"

        //Método para arredondar botão
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

        //Personalização de estilo
        private void PersonalizarBotao(Button botao)
        {
            int raio = 15;

            ArredondarBotao(botao, raio);

            botao.BackColor = Color.FromArgb(255, 0, 0); // Vermelho
            botao.ForeColor = Color.Black;
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
            botao.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            PersonalizarBotao(button1); // Botão Excluir
            PersonalizarBotao(button2); // Botão Voltar
        }
    }
}
