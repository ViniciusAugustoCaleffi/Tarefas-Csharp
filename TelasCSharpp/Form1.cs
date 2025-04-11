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
            flowLayoutPanel1.AutoScroll = true;
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
            PersonalizarBotao(button4);
            PersonalizarBotao(btnBuscar);

            // 🟤 Fundo do formulário principal
            this.BackColor = Color.FromArgb(40, 40, 40); // Cinza escuro

            // 🟤 Fundo do painel onde os cards são exibidos

            flowLayoutPanel1.BackColor = Color.FromArgb(40, 40, 40); // Ou o mesmo cinza do fundo, se quiser manter o contraste
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;

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
            flowLayoutPanel1.Refresh();

            foreach (var tarefa in tarefasAFazer)
            {
                flowLayoutPanel1.Controls.Add(CriarCard(tarefa));
            }
        }

        public void LimparFlowLayout(FlowLayoutPanel flowLayout)
        {
            flowLayout.Controls.Clear();
            
        }


        public void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private Panel CriarCard(Tarefa tarefa)
        {
            Panel card = new Panel
            {
                Width = 180,
                Height = 120,
                Margin = new Padding(10),
            };

            // Arredondamento visual
            card.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle bounds = new Rectangle(0, 0, card.Width, card.Height);
                int radius = 20;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
                    path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
                    path.CloseFigure();

                    card.Region = new Region(path);
                }
            };


            Label titulo = new Label
            {
                Text = tarefa.Titulo,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = false,
                Height = 25,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            // Define a cor do título com base na prioridade
            switch (tarefa.Prioridade.ToLower())
            {
                
                case "baixa":
                    card.BackColor = Color.SeaGreen;
                    break;
                case "media":
                    card.BackColor = Color.Goldenrod;
                    break;
                default:
                    card.BackColor = Color.Red;
                    break;
            }

            Label descricao = new Label
            {
                Text = tarefa.Descricao,
                Font = new Font("Segoe UI", 8),
                AutoSize = false,
                Height = 35,
                Dock = DockStyle.Top,
                ForeColor = Color.White
            };

            Label dataVencimento = new Label
            {
                Text = $"Vence em: {tarefa.DtVencimento:dd/MM/yyyy}",
                Font = new Font("Segoe UI", 7, FontStyle.Italic),
                ForeColor = Color.White,
                Height = 20,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label prioridade = new Label
            {
                Text = $"Prioridade: {tarefa.Prioridade}", // Ou apenas: tarefa.Prioridade
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                AutoSize = false,
                Height = 20,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 5)
            };


            Button deletarBtn = new Button
            {
                Size = new Size(20, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.trash,
                BackgroundImageLayout = ImageLayout.Zoom,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
            };
            deletarBtn.Location = new Point(5, card.Height - deletarBtn.Height - 0);





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

            Button editarBtn = new Button
            {
                Size = new Size(20, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.pencil,
                BackgroundImageLayout = ImageLayout.Zoom,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
            };

            editarBtn.Location = new Point(card.ClientSize.Width - editarBtn.Width - 5, card.ClientSize.Height - editarBtn.Height);
            editarBtn.BringToFront();

            editarBtn.Click += (s, e) =>
            {

                Form4 cad = new Form4(tarefa.Id);
                cad.Show();


                
            };


            card.Controls.Add(editarBtn);
            editarBtn.BringToFront(); // opcional, só pra garantir que fique visível


            // Adiciona controles ao card
            card.Controls.Add(deletarBtn);
            card.Controls.Add(prioridade);
            card.Controls.Add(dataVencimento);
            card.Controls.Add(descricao);
            card.Controls.Add(titulo);

            return card;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ObterTarefasEAtualizarTabelas();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            //var filtroForm = new FormFiltros();
            //
            //if (filtroForm.ShowDialog() == DialogResult.OK)
            //{
            //    var dao = new DAO();
            //    var tarefasFiltradas = dao.FiltrarTarefas(
            //        filtroForm.StatusSelecionado,
            //        filtroForm.PrioridadeSelecionada,
            //        filtroForm.TituloBusca,
            //        filtroForm.DataSelecionada
            //    );
            //
            //    flowLayoutPanel1.Controls.Clear();
            //
            //    foreach (var tarefa in tarefasFiltradas)
            //    {
            //
            //        flowLayoutPanel1.Controls.Add(CriarCard(tarefa));
            //        
            //    }
            //}
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear(); // limpa resultados anteriores

            string titulo = textBox1.Text.Trim();
            DAO dao = new DAO();
            List<Tarefa> tarefas = dao.BuscarPorTitulo(titulo);

            flowLayoutPanel1.Controls.Clear(); // 
            flowLayoutPanel1.Refresh();

            foreach (var tarefa in tarefas)
            {
                flowLayoutPanel1.Controls.Add(CriarCard(tarefa));
            }

        }
    }
}
