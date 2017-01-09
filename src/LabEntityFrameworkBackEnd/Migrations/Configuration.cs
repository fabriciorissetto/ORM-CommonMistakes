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

        protected override void Seed(Contexto context)
        {
            for (int i = 0; i < 500; ++i)
            {
                PopulaDados(i);
            }
        }

        private static void PopulaDados(int i)
        {
            using (var contexto = new Contexto())
            {
                var instituicao = new InstituicaoEnsino
                {
                    Nome = string.Concat("Instituição ", i),
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
