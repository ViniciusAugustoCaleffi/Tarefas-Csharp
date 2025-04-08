using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelasCSharpp
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DtVencimento { get; set; }
        public string Prioridade { get; set; } // Baixa, Média, Alta
        public bool Concluida { get; set; }

    }
}
