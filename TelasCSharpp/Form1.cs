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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        } // Texto "MENU PRINCIPAL"

        private void button1_Click(object sender, EventArgs e)
        {

        } // Adicionar Tarefa

        private void button2_Click(object sender, EventArgs e)
        {

        } // Editar Tarefa

        private void button3_Click(object sender, EventArgs e)
        {

        } // Excluir Tarefa

        // 🔵 Método para arredondar botões
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

        // 🌟 Personalização no carregamento do formulário
        private void Form1_Load(object sender, EventArgs e)
        {
            PersonalizarBotao(button1);
            PersonalizarBotao(button2);
            PersonalizarBotao(button3);
        }

        private void PersonalizarBotao(Button botao)
        {
            int raio = 15;

            // Aplica o arredondamento
            ArredondarBotao(botao, raio);

            // Estilo moderno
            botao.BackColor = Color.FromArgb(125, 125, 125); // Cinza neutro (#7D7D7D)
            botao.ForeColor = Color.White; // Fonte branca


            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
            botao.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }
    }
}
