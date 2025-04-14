using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TelasCSharpp
{
    public partial class FormFiltros : Form
    {
        public string FiltroStatus => comboBoxStatus.SelectedItem?.ToString() ?? "Todos";
        public string FiltroPrioridade => comboBoxPrioridade.SelectedItem?.ToString() ?? "Todas";
        public DateTime? FiltroData => checkBoxFiltrarData.Checked ? dateTimePicker1.Value.Date : (DateTime?)null;


        public FormFiltros()
        {
            InitializeComponent();
            comboBoxStatus.Items.AddRange(new string[] { "Todos", "Concluidas", "Pendentes" });
            comboBoxPrioridade.Items.AddRange(new string[] { "Todas", "Alta", "Média", "Baixa" });
            comboBoxStatus.SelectedIndex = 0;
            comboBoxPrioridade.SelectedIndex = 0;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            
            DialogResult = DialogResult.OK;
            Close();
        }
               

        private void comboBoxPrioridade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxFiltrarData_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
