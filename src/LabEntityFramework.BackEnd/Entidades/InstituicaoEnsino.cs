using System.Collections.Generic;

namespace LabEntityFrameworkBackEnd.Entidades
{
    public class InstituicaoEnsino 
    {
		public InstituicaoEnsino()
		{
			Cursos = new List<Curso>();
			Alunos = new List<Aluno>();
		}

		public int Id { get; set; }
		public string Nome { get; set; }
        public string Descricao { get; set; }        
        public virtual Contrato Contrato { get; set; }

        public virtual IList<Curso> Cursos { get; set; }
        public virtual IList<Aluno> Alunos { get; set; }        
    }
}
