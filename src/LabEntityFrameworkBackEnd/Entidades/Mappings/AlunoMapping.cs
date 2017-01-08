using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabEntityFrameworkBackEnd.Entidades.Mappings
{
	public class AlunoMapping : EntityTypeConfiguration<Aluno>
	{
		public AlunoMapping()
		{
			this.HasKey(t => t.Id);

			this.HasOptional(t => t.Instituicao)
				.WithMany(t => t.Alunos)
				.Map(t => t.MapKey("IdInstituicao"));

			this.HasOptional(t => t.Curso)
				.WithMany()
				.Map(t => t.MapKey("IdCurso"));
		}
	}
}
