using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabEntityFrameworkBackEnd.Entidades
{
    public class Aluno 
    {
		public int Id { get; set; }
		public string Nome { get; set; }
        public string Matricula { get; set; }
        public virtual InstituicaoEnsino Instituicao { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
