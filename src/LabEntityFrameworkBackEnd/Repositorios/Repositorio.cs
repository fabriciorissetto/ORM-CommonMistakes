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
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
   

        public TimeSpan Um_N_Mais_Problema(int numRegistros)
        {
            using (var context = new Contexto())
            {
                context.Configuration.LazyLoadingEnabled = true;
                context.Configuration.ProxyCreationEnabled = true;

                RegisterStartTime();

                var alunos = context.Alunos
                    .AsNoTracking().Take(numRegistros).ToList();

                foreach (var aluno in alunos)
                {
                    var instituicao = aluno.Instituicao;
                    var cursos = aluno.Curso;
                }

                RegisterEndTime();

                
            }
            return GetExecutionTime();
        }

        public TimeSpan Um_N_Mais_Solucao(int numRegistros)
        {
            using (var context = new Contexto())
            {
                RegisterStartTime();

                var alunos = context.Alunos
                    .Include("Instituicao")
                    .Include("Curso")
                    .AsNoTracking().Take(numRegistros).ToList();

                foreach (var aluno in alunos)
                {
                    var instituicao = aluno.Instituicao;
                    var cursos = aluno.Curso;
                }

                RegisterEndTime();
            }
            return GetExecutionTime();
        }


        public TimeSpan Dois_Projecao_Sem_Projecao(int numRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                contexto.InstituicoesEnsino
                    .Include("Contrato")
                    .Include("Cursos").AsNoTracking().Take(numRegistros).ToList();

                RegisterEndTime();
            }
            return GetExecutionTime();
        }

        public TimeSpan Dois_Projecao_Com_Projecao(int numRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                var listaIntituicoes = new List<InstituicaoEnsino>();

                var instituicoes = contexto.InstituicoesEnsino
                    .Include("Contrato").Include("Cursos").AsNoTracking().Take(numRegistros)
                    .Select(p => new
                    {
                        InstituicaoId = p.Id,
                        NomeInstituicao = p.Nome,
                        Cursos = p.Cursos.Select(c => new { CursoId = c.Id, NomeCurso = c.Nome }),
                        NumeroContratoInstituicao = p.Contrato.NumeroContrato
                    }).ToList();

                instituicoes.ForEach(resultadoQuery =>
                    listaIntituicoes.Add(new InstituicaoEnsino
                    {
                        Id = resultadoQuery.InstituicaoId,
                        Nome = resultadoQuery.NomeInstituicao,
                        Contrato = new Contrato
                        {
                            NumeroContrato = resultadoQuery.NumeroContratoInstituicao
                        },
                        Cursos = resultadoQuery.Cursos.Select(c => new Curso
                        {
                            Id = c.CursoId,
                            Nome = c.NomeCurso
                        }).ToList()
                    })
                );
                RegisterEndTime();
            }
            return GetExecutionTime();
        }


        public TimeSpan Tres_UsoDetectChangesHabilitado(int numRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                var instituicao = contexto.InstituicoesEnsino.Include("Cursos").First();

                for (int i = 0; i < numRegistros; i++)
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
            return GetExecutionTime();
        }

        public TimeSpan Tres_UsoDetectChangesDesabilitado(int numRegistros)
        {
            using (var contexto = new Contexto())
            {
                contexto.Configuration.AutoDetectChangesEnabled = false;

                RegisterStartTime();

                var instituicao = contexto.InstituicoesEnsino.Include("Cursos").First();

                for (int i = 0; i < numRegistros; i++)
                {
                    var aluno = new Aluno
                    {
                        Instituicao = instituicao,
                        Curso = instituicao.Cursos.First(),
                        Matricula = (i + 1).ToString().PadLeft(6, '0')
                    };
                    contexto.Alunos.Add(aluno);
                }

                contexto.Configuration.AutoDetectChangesEnabled = true;
                contexto.SaveChanges();

                RegisterEndTime();
            }
            return GetExecutionTime();
        }


        public TimeSpan Quatro_Objetos_Desnecessarios_Contexto(int numRegistros)
        {
            using(var context = new Contexto())
            {
                RegisterStartTime();

                var alunos = context.Alunos.Include("Curso")
                    .Take(numRegistros).ToList();

                RegisterEndTime();
            }
            return GetExecutionTime();
        }

        public TimeSpan Quatro_Objetos_Desnecessarios_Contexto_Com_AsNoTracking(int numRegistros)
        {
            using (var context = new Contexto())
            {
                RegisterStartTime();

                var alunos = context.Alunos.Include("Curso")
                    .AsNoTracking()
                    .Take(numRegistros).ToList();

                RegisterEndTime();
            }
            return GetExecutionTime();
        }


        public TimeSpan InsertComBulkInsert(int numRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                var instituicao = contexto.InstituicoesEnsino.Include("Cursos").First();
                var alunosNovos = new List<Aluno>();

                for (int i = 0; i < numRegistros; i++)
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
            return GetExecutionTime();
        }

        public TimeSpan InsertSemBulkInsert(int numRegistros)
        {
            using (var contexto = new Contexto())
            {
                RegisterStartTime();

                var instituicao = contexto.InstituicoesEnsino.Include("Cursos").First();

                for (int i = 0; i < numRegistros; i++)
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
            return GetExecutionTime();
        }


        public void InicializaEFMigrationsNaoAfetarTempo()
        {
            using (var context = new Contexto())
            {
                var aluno = context.Alunos.First();
            }
        }

        public TimeSpan GetTime()
        {
            return new TimeSpan(0, 0, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
        }

        public void RegisterStartTime()
        {
            StartTime = GetTime();
        }

        public void RegisterEndTime()
        {
            EndTime = GetTime();
        }

        public TimeSpan GetExecutionTime()
        {
            return EndTime.Subtract(StartTime);
        }
    }
}
