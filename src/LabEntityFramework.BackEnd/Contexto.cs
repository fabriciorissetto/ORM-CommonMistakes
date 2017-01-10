using LabEntityFrameworkBackEnd.Entidades;
using LabEntityFrameworkBackEnd.Entidades.Mappings;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LabEntityFrameworkBackEnd
{
	public class Contexto : DbContext
	{
		public DbSet<InstituicaoEnsino> InstituicoesEnsino { get; set; }
		public DbSet<Aluno> Alunos { get; set; }
		public DbSet<Curso> Cursos { get; set; }

		public Contexto()
			: base("Name=ProdutoraContext")
		{
			Configuration.LazyLoadingEnabled = false;
			Configuration.ProxyCreationEnabled = false;
			Configuration.AutoDetectChangesEnabled = true;
			Configuration.ValidateOnSaveEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			modelBuilder.Configurations.Add(new AlunoMapping());
			modelBuilder.Configurations.Add(new InstituicaoEnsinoMapping());
		}

		public void Commit()
		{
			base.SaveChanges();
		}
	}
}
