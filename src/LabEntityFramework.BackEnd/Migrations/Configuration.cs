namespace LabEntityFrameworkBackEnd.Migrations
{
    using LabEntityFrameworkBackEnd.Entidades;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        //A execu��o do conte�do do m�todo abaixo demora aproximadamente 1 hora
        //Portanto, para a execu��o dos exemplos dessa aplica��o de forma mais r�pida 
        //sugiro restaurar o backup da base dispon�vel em https://github.com/fabriciorissetto/ORM-CommonMistakes/blob/master/BancoDeDadosExemplo_Backup.rar
        protected override void Seed(Contexto context)
        {
            //for (int i = 0; i < 500; ++i)
            //    PopulaBaseDeDados(i);
        }

        private static void PopulaBaseDeDados(int i)
        {
            using (var contexto = new Contexto())
            {
                contexto.Configuration.AutoDetectChangesEnabled = false;

                var instituicao = new InstituicaoEnsino
                {
                    Nome = string.Concat("Institui��o ", i),
                    Contrato = new Contrato
                    {
                        NumeroContrato = i
                    }
                };

                for (int c = 0; c < 500; ++c)
                {
                    var curso = new Curso
                    {
                        Nome = string.Concat("Curso ", i + c)
                    };
                    instituicao.Cursos.Add(curso);
                }

                for (int a = 0; a < 20000; ++a)
                {
                    var posicaoCurso = new Random();

                    var aluno = new Aluno
                    {
                        Nome = string.Concat("Aluno ", i + a),
                        Matricula = (a + i).ToString().PadLeft(10, '0'),
                        Curso = instituicao.Cursos[posicaoCurso.Next(1, 499)]
                    };
                    instituicao.Alunos.Add(aluno);
                }

                contexto.InstituicoesEnsino.Add(instituicao);
                contexto.SaveChanges();
            }
        }
    }
}
