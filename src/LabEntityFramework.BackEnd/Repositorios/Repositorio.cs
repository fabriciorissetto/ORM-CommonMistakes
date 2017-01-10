using LabEntityFrameworkBackEnd.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFramework.BulkInsert.Extensions;

namespace LabEntityFrameworkBackEnd.Repositorios
{
    public class Repositorio
    {
        private TimeSpan StartTime { get; set; }
        private TimeSpan EndTime { get; set; }

        public TimeSpan Um_N_Mais_Problema(int quantidadeRegistros)
        {
            using (var context = new Contexto())
            {
                context.Configuration.LazyLoadingEnabled = true;
                context.Configuration.ProxyCreationEnabled = true;

                RegisterStartTime();

                var alunos = context.Alunos
                                    .AsNoTracking()
                                    .Take(quantidadeRegistros)
                                    .ToList();

                foreach (var aluno in alunos)
                {
                    var instituicao = aluno.Instituicao;
                    var cursos = aluno.Curso;
                }

                RegisterEndTime();
            }
            return this.GetExecutionTime;
        }

        public TimeSpan Um_N_Mais_Solucao(int quantidadeRegistros)
        {
            using (var context = new Contexto())
            {
                RegisterStartTime();

                var alunos = context.Alunos
                                    .Include("Instituicao")
                                    .Include("Curso")
                                    .AsNoTracking()
                                    .Take(quantidadeRegistros).ToList();

                foreach (var aluno in alunos)
                {
                    var instituicao = aluno.Instituicao;
                    var cursos = aluno.Curso;
                }

                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }

        public TimeSpan Dois_Projecao_Sem_Projecao(int quantidadeRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                var instituicoes = contexto.InstituicoesEnsino
                                            .Include(x => x.Cursos)
                                            .Take(quantidadeRegistros)
                                            .ToList();

                foreach (var instituicao in instituicoes)
                {
                    var nomeInstituicao = instituicao.Nome;
                    var quantidadeCursos = instituicao.Cursos.Count();
                }

                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }

        public TimeSpan Dois_Projecao_Com_Projecao(int quantidadeRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                var instituicoes = contexto.InstituicoesEnsino
                        //.Include(x => x.Cursos)    <-- Não precisa pois está na projeção
                        //.AsNoTracking()            <-- Não precisa pois a projeção não é adicionada no contexto
                        .Take(quantidadeRegistros)
                        .Select(p => new
                        {
                            NomeInstituicao = p.Nome,
                            Cursos = p.Cursos.Count()
                        }).ToList();
                
                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }

        public TimeSpan Tres_UsoDetectChangesHabilitado(int quantidadeRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                var instituicao = contexto.InstituicoesEnsino.Include("Cursos").First();

                for (int i = 0; i < quantidadeRegistros; i++)
                {
                    var aluno = new Aluno
                    {
                        Instituicao = instituicao,
                        Curso = instituicao.Cursos.First(),
                        Matricula = (i + 1).ToString().PadLeft(6, '0')
                    };
                    contexto.Alunos.Add(aluno);
                }

                contexto.SaveChanges();

                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }

        public TimeSpan Tres_UsoDetectChangesDesabilitado(int quantidadeRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                contexto.Configuration.AutoDetectChangesEnabled = false;
                var instituicao = contexto.InstituicoesEnsino.Include("Cursos").First();

                for (int i = 0; i < quantidadeRegistros; i++)
                {
                    var aluno = new Aluno
                    {
                        Instituicao = instituicao,
                        Curso = instituicao.Cursos.First(),
                        Matricula = (i + 1).ToString().PadLeft(6, '0')
                    };
                    contexto.Alunos.Add(aluno);
                }
                
                contexto.SaveChanges();

                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }


        public TimeSpan Quatro_Objetos_Desnecessarios_Contexto(int quantidadeRegistros)
        {
            using (var context = new Contexto())
            {
                RegisterStartTime();

                var alunos = context.Alunos
                                    .Include("Curso")
                                    .Take(quantidadeRegistros)
                                    .ToList();

                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }

        public TimeSpan Quatro_Objetos_Desnecessarios_Contexto_Com_AsNoTracking(int quantidadeRegistros)
        {
            using (var context = new Contexto())
            {
                RegisterStartTime();

                var alunos = context.Alunos
                                    .Include("Curso")
                                    .AsNoTracking()
                                    .Take(quantidadeRegistros)
                                    .ToList();

                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }

        public TimeSpan InsertSemBulkInsert(int quantidadeRegistros)
        {
            using (var contexto = new Contexto())
            {
                contexto.Configuration.AutoDetectChangesEnabled = false;
                RegisterStartTime();

                var instituicao = contexto.InstituicoesEnsino.Include("Cursos").First();

                for (int i = 0; i < quantidadeRegistros; i++)
                {
                    var aluno = new Aluno
                    {
                        Nome = string.Concat("Aluno inserido sem bulkInsert ", i),
                        Instituicao = instituicao,
                        Curso = instituicao.Cursos.First(),
                        Matricula = (i + 1).ToString().PadLeft(6, '0')
                    };
                    contexto.Alunos.Add(aluno);
                }
                contexto.SaveChanges();

                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }

        public TimeSpan InsertComBulkInsert(int quantidadeRegistros)
        {
            using (var contexto = new Contexto())
            {
                contexto.Configuration.AutoDetectChangesEnabled = false;
                RegisterStartTime();

                var instituicao = contexto.InstituicoesEnsino.Include("Cursos").First();

                var alunosNovos = new List<Aluno>();
                for (int i = 0; i < quantidadeRegistros; i++)
                {
                    var aluno = new Aluno
                    {
                        Nome = string.Concat("Aluno inserido com bulkInsert ", i),
                        Instituicao = instituicao,
                        Curso = instituicao.Cursos.First(),
                        Matricula = (i + 1).ToString().PadLeft(6, '0')
                    };
                    alunosNovos.Add(aluno);
                }
                contexto.BulkInsert(alunosNovos);

                RegisterEndTime();
            }

            return this.GetExecutionTime;
        }

        public void InicializaEFMigrationsNaoAfetarTempo()
        {
            using (var context = new Contexto())
            {
                var aluno = context.Alunos.First();
            }
        }

        private TimeSpan GetTime
        {
            get
            {
                return new TimeSpan(0, 0, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            }
        }

        public void RegisterStartTime()
        {
            StartTime = this.GetTime;
        }

        public void RegisterEndTime()
        {
            EndTime = this.GetTime;
        }

        private TimeSpan GetExecutionTime
        {
            get
            {
                return EndTime.Subtract(StartTime);
            }
        }
    }
}
