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
            ObterTarefasEAtualizarTabelas();
            this.Load += Form1_Load;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        } // Texto "MENU PRINCIPAL"

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 cad = new Form2();
            cad.ShowDialog();
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



        public void ObterTarefasEAtualizarTabelas()
        {
            DAO dao = new DAO();

            var tarefasAFazer = dao.PreencherVetor();

            flowLayoutPanel1.Controls.Clear(); //

            foreach (var tarefa in tarefasAFazer)
            {
                flowLayoutPanel1.Controls.Add(CriarCard(tarefa));
            }
        }



        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private Panel CriarCard(Tarefa tarefa)
        {
            Panel card = new Panel
            {
                Width = 220,


                Height = 180,


                BackColor = Color.FromArgb(235, 245, 255), // Azul bem clarinho


                Margin = new Padding(10),


                BorderStyle = BorderStyle.FixedSingle


            };

            Label titulo = new Label
            {
                Text = tarefa.Titulo,


                Font = new Font("Segoe UI", 10, FontStyle.Bold),


                AutoSize = false,


                Height = 25,


                Dock = DockStyle.Top,


                TextAlign = ContentAlignment.MiddleCenter,


                ForeColor = Color.FromArgb(0, 90, 158)
            };

            Label descricao = new Label
            {
                Text = tarefa.Descricao,


                Font = new Font("Segoe UI", 9),


                AutoSize = false,


                Height = 40,


                Dock = DockStyle.Top
            };

            Label dataVencimento = new Label
            {
                Text = $"Vence em: {tarefa.DtVencimento.ToString("dd/MM/yyyy")}",


                Font = new Font("Segoe UI", 8, FontStyle.Italic),


                ForeColor = Color.DarkBlue,


                Height = 20,


                Dock = DockStyle.Top,


                TextAlign = ContentAlignment.MiddleCenter
            };


            Label prioridade = new Label
            {
                Text = $"Prioridade: {tarefa.Prioridade}",


                Font = new Font("Segoe UI", 9, FontStyle.Bold),


                AutoSize = false,


                Height = 20,


                Dock = DockStyle.Top,


                TextAlign = ContentAlignment.MiddleCenter,
            };

            switch (tarefa.Prioridade.ToLower())
            {
                case "alta":
                    prioridade.BackColor = Color.Orange;

                    prioridade.ForeColor = Color.White;
                    break;

                case "média":


                case "media":
                    prioridade.BackColor = Color.Gold;


                    prioridade.ForeColor = Color.Black;


                    break;

                case "baixa":
                    prioridade.BackColor = Color.LightGreen;


                    prioridade.ForeColor = Color.Black;


                    break;

                default:
                    prioridade.BackColor = Color.LightGray;


                    prioridade.ForeColor = Color.Black;


                    break;
            }


            Button deletarBtn = new Button
            {
                Text = "Deletar",

                Dock = DockStyle.Bottom,

                Height = 25,

                BackColor = Color.FromArgb(255, 100, 100),

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat
            };


            deletarBtn.FlatAppearance.BorderSize = 0;

            deletarBtn.Click += (s, e) =>
            {
                var resultado = MessageBox.Show("Deseja realmente excluir?", "Confirmação", MessageBoxButtons.YesNo);


                if (resultado == DialogResult.Yes)
                {
                    DAO dao = new DAO();


                    dao.ExcluirTarefa(tarefa.Id);

                    ObterTarefasEAtualizarTabelas();
                }
            };

            Button atualizarBtn = new Button
            {
                Text = "Atualizar",

                Dock = DockStyle.Bottom,

                Height = 25,

                BackColor = Color.Gray,

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat
            };


            atualizarBtn.FlatAppearance.BorderSize = 0;

            atualizarBtn.Click += (s, e) =>
            {
                Form2 cad = new Form2();                
                cad.Show();
            };

            card.Controls.Add(atualizarBtn);

            card.Controls.Add(deletarBtn);

            card.Controls.Add(prioridade);


            card.Controls.Add(descricao);


            card.Controls.Add(titulo);

            return card;


        }
    }
}
