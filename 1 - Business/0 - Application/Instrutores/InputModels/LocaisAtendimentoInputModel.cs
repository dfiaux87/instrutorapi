using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Instrutores.InputModels
{
    public class LocaisAtendimentoInputModel
    {
        public int? IndInstrutor { get; set; }
        public int? IdLocalAtendimento { get; set; }
        public string UF { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get;  set; }
    }
}
