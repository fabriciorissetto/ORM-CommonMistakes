using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabEntityFrameworkBackEnd.Entidades.Mappings
{
	public class InstituicaoEnsinoMapping : EntityTypeConfiguration<InstituicaoEnsino>
	{
		public InstituicaoEnsinoMapping()
		{
			this.HasKey(t => t.Id);

			this.HasMany(t => t.Cursos)
				.WithOptional()
				.Map(t => t.MapKey("IdInstituicao"));

			this.HasOptional(t => t.Contrato)
				.WithMany()
				.Map(t => t.MapKey("IdContrato"));
		}
	}
}
